using System.Security.Cryptography;
using Aktitic.HrProject.Api.Configuration;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class DocumentManager(
    IUnitOfWork unitOfWork,
    UserUtility userUtility,
    IMapper mapper,
    IWebHostEnvironment webHostEnvironment) : IDocumentManager
{
    public async Task<ApiRespone<string>> Add(DocumentAddDto documentAddDto)
    {
        var file = new Document()
        {
            UserId = int.TryParse(userUtility.GetUserId(),out var userId) ? userId : null,
            Confidential = documentAddDto.Confidential,
            Version = 0,
            ProjectId = documentAddDto.ProjectId,
            CreatedAt = documentAddDto.DateCreated,
            Type = documentAddDto.Type,
            DocumentCode = documentAddDto.DocumentCode,
            // UniqueName = 
            // Revision = documentAddDto.Revision,
            Title = documentAddDto.Title,
            PrintSize = documentAddDto.PrintSize,
            Status = documentAddDto.Status,
            Description = documentAddDto.Description,
            CreatedBy = userUtility.GetUserName(),
        };
        if (unitOfWork.Documents.IsUniqueCode(documentAddDto.DocumentCode))
            return new ApiRespone<string>("Document code already exists")
            {
                Success = false,
            };
        
        if (documentAddDto.Revisors != null)
        {
            var revisorsDto =JsonConvert.DeserializeObject<List<int>>(documentAddDto.Revisors);
            if (revisorsDto != null)
            {
          
                file.Revisors = revisorsDto
                    .Select(x => new Revisor()
                    {
                        EmployeeId = x,
                        DocumentId = file.Id,
                        IsReviewed = false,
                        RevisionDate = null,
                    })
                    .ToList();
            
            }
        }

        if (documentAddDto.FileUsers != null)
        {
            var fileUsers = JsonConvert.DeserializeObject<List<FileUsers>>(documentAddDto.FileUsers);
            
            // add file users
            if (documentAddDto is { Confidential: Confidential.Shared, FileUsers: not null })
            {
                if (fileUsers != null)
                    file.FileUsers = fileUsers
                        .Select(x => new FileUsers()
                        {
                            FileUserId = x.FileUserId,
                            DocumentId = file.Id,
                            Read = x.Read,
                            Write = x.Write,
                            CreatedAt = DateTime.Now,
                            CreatedBy = userUtility.GetUserName()
                        })
                        .ToList();
            }
        }

        // // upload file  
        if (documentAddDto.Files != null && documentAddDto.Files.Any())
        {
            file.DocumentFiles = [];
            
                // Determine the file path based on whether ProjectId is provided
                foreach (var t in documentAddDto.Files)
                {
                    DocumentFile documentFile = new();
                    var projectFolder = documentAddDto.ProjectId.ToString() ?? "general";
                    var filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads/files", projectFolder);
        
                    // Ensure the directory exists
                    Directory.CreateDirectory(filePath);
        
                    // Construct the full file path
                    var fullFilePath = Path.Combine(filePath, t.FileName);
        
                    // Save the file to the specified path
                    await using (var stream = new FileStream(fullFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        await t.CopyToAsync(stream);
                        var sha256 = SHA256.Create();
                       
                        documentFile.FileHash = await sha256.ComputeHashAsync(stream);
                       
                              
                    }
        
                    // Store the relative file path in the file entity

                    // Debug.Assert(file.DocumentFiles != null, "file.DocumentFiles != null");

                    documentFile.Path = Path.Combine("uploads/files", projectFolder,
                        t.FileName);
                    documentFile.Name = t.FileName;
                    documentFile.Size = t.Length;
                    documentFile.Type = t.ContentType;
                         

                    file.DocumentFiles.Add(documentFile);

                    // Add the file entity to the unit of work
                }
        }
        
        unitOfWork.Documents.Add(file);
        unitOfWork.Documents.ComputeHash(file.Id);
        await unitOfWork.SaveChangesAsync();
        
        return new ApiRespone<string>("Document added successfully")
        {
            Success = true
        };
    }

    public async Task<int> Update(DocumentUpdateDto documentUpdateDto, int id)
    {
        var document = unitOfWork.Documents.GetById(id);

        if (document == null) return 0;
        var user =int.TryParse(userUtility.GetUserId(), out var userId) ? userId : (int?)null;
        
 

        if (!unitOfWork.Revisors.RevisionCommited(id))
        { 
            document.UserId = user;
            if (documentUpdateDto.Confidential != null)
            {
                document.Confidential = documentUpdateDto.Confidential;
            }

            if (documentUpdateDto.ProjectId != 0)
            {
                document.ProjectId = documentUpdateDto.ProjectId;
            }

            if (!documentUpdateDto.Title.IsNullOrEmpty())
                document.Title = documentUpdateDto.Title;

            // document.Version += 1;
            if (!documentUpdateDto.PrintSize.IsNullOrEmpty())
                document.PrintSize = documentUpdateDto.PrintSize;
            if (!documentUpdateDto.DocumentCode.IsNullOrEmpty())
                document.DocumentCode = documentUpdateDto.DocumentCode;

            if (!documentUpdateDto.Status.IsNullOrEmpty())
                document.Status = documentUpdateDto.Status;
            if (!documentUpdateDto.Type.IsNullOrEmpty())
                document.Type = documentUpdateDto.Type;
            // if (!documentUpdateDto.Revision.IsNullOrEmpty())
            //     file.Revision = documentUpdateDto.Revision;
            if (!documentUpdateDto.Description.IsNullOrEmpty())
                document.Description = documentUpdateDto.Description;

            if (documentUpdateDto.FileUsers != null)
            {
                var fileUsers = JsonConvert.DeserializeObject<List<FileUsers>>(documentUpdateDto.FileUsers);
                if (documentUpdateDto.Confidential == Confidential.Shared)
                {
                    document.FileUsers = fileUsers?
                        .Select(x => new FileUsers()
                        {
                            FileUserId = x.FileUserId,
                            DocumentId = document.Id,
                            Read = x.Read,
                            Write = x.Write,
                            CreatedAt = DateTime.Now,
                            CreatedBy = userUtility.GetUserName()
                        })
                        .ToList();
                }
            }
            unitOfWork.Documents.Update(document);
        }
        else
        {
            var newDocument = new Document()
            {
                UserId = user,
                Confidential = documentUpdateDto.Confidential,
                Version = document.Version + 1,
                Previous = document.Id,
                ProjectId = documentUpdateDto.ProjectId,
                // CreatedAt = documentUpdateDto.DateCreated,
                Type = documentUpdateDto.Type,
                DocumentCode = documentUpdateDto.DocumentCode,
                // UniqueName = 
                // Revision = documentAddDto.Revision,
                // DocumentFiles = document.DocumentFiles,
                FileUsers = document.FileUsers,
                Title = documentUpdateDto.Title,
                PrintSize = documentUpdateDto.PrintSize,
                Status = documentUpdateDto.Status,
                Description = documentUpdateDto.Description,
                CreatedBy = userUtility.GetUserName(),
            };
            if (document.DocumentFiles != null)
            {
                foreach (var doc in document.DocumentFiles)
                {
                    newDocument.DocumentFiles?.Add(doc);
                }
            }

            return await AddDocumentToChain(newDocument , document.DocumentCode);
        }
        
        return await unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var file = unitOfWork.Documents.GetById(id);
        if (file==null) return Task.FromResult(0);
        file.IsDeleted = true;
        file.DeletedAt = DateTime.Now;
        file.DeletedBy = userUtility.GetUserName();
        unitOfWork.Documents.Update(file);
        return unitOfWork.SaveChangesAsync();
    }

    public async Task<List<DocumentReadDto>?> GetByProjectId(int id)
    {
        var files = await  unitOfWork.Documents.GetByProjectId(id);
        files = files.ToList();
        if (files.Any()) return null;
        var result= files.Select(file => new DocumentReadDto()
        {
            Id = file.Id,
            
            Confidential = file.Confidential,
            UniqueName = string.Concat(file.DocumentCode ,"ـV"+ file.Version.ToString() ,"ـR" + file.Revision.ToString() ),
            Version = "V"+(file.Version.ToString()),
            Type = file.Type,
            Revision = "R" + (file.Revision.ToString()),
            DocumentCode = file.DocumentCode,
            Title = file.Title,
            PrintSize = file.PrintSize,
            Status = file.Status,
            DateCreated = file.CreatedAt,
            Description = file.Description,
            ProjectId = file.ProjectId,
            
            UserId = file.UserId, 
            CreatedBy =file.CreatedBy,
            CreatedAt =file.CreatedAt,
            UpdatedBy =file.UpdatedBy,
            UpdatedAt =file.UpdatedAt,
        }).ToList();
        
      
        return result;
    }

    public async Task<DocumentReadDto?> GetById(int id)
    {
        var file = await unitOfWork.Documents.GetFileById(id);
        if (file is null) return Task.FromResult(null as DocumentReadDto).Result;
                
        var result = new DocumentReadDto()
        {
            Id = file.Id,
            Confidential = file.Confidential,
            UniqueName = string.Concat(file.DocumentCode ,"ـV"+ file.Version.ToString() ,"ـR" + file.Revision.ToString() ),
            Version = "V"+(file.Version.ToString() ??"00"),
            Type = file.Type,
            Revision = "R" + (file.Revision.ToString() ?? "00"),
            Title = file.Title,
            PrintSize = file.PrintSize,
            Status = file.Status,
            DateCreated = file.CreatedAt,
            Description = file.Description,
            DocumentCode = file.DocumentCode,
            ProjectId = file.ProjectId,
            First = file.First,
            Previous = file.Previous,
            Next = file.Next,
            Last = file.Last,
            Revisors = file.Revisors?.Select(r => new RevisorDto()
            {
                Employee = mapper.Map<EmployeeDto>(r.Employee),
                IsReviewed = r.IsReviewed,
                EmployeeId = r.EmployeeId,
                Notes = r.Notes,
                RevisionDate = r.RevisionDate,
            }).ToList(),
            
            UserId = file.UserId, 
            CreatedBy =file.CreatedBy,
            CreatedAt =file.CreatedAt,
            UpdatedBy =file.UpdatedBy,
            UpdatedAt =file.UpdatedAt,
            FileUsers = file.FileUsers?.Select(x => new FileUsers()
            {
                FileUserId = x.FileUserId,
                DocumentId = x.DocumentId,
                Read = x.Read,
                Write = x.Write,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
            }).ToList(),
            DocumentFiles = file.DocumentFiles?.Select(x => new DocumentFileReadDto()
            {
                Id = x.Id,
                Path = x.Path,
                Name = x.Name,
                Size = x.Size,
                Type = x.Type,
                FileNumber = x.FileNumber,
                DocumentId = x.DocumentId,
            }).ToList(),
        };
        
        return result;
    }

    public async Task<List<DocumentReadDto>> GetAll()
    {
        var files =await unitOfWork.Documents.GetAll();
        var result= files.Select(file => new DocumentReadDto()
        {
            Id = file.Id,
         
            
            Confidential = file.Confidential,
            UniqueName = string.Concat(file.DocumentCode ,"ـV"+ file.Version.ToString() ,"ـR" + file.Revision.ToString() ),
            Version = "V"+(file.Version.ToString() ),
            Type = file.Type,
            Revision = "R" + (file.Revision.ToString() ),
            Title = file.Title,
            PrintSize = file.PrintSize,
            Status = file.Status,
            DateCreated = file.CreatedAt,
            Description = file.Description,
            DocumentCode = file.DocumentCode,
            ProjectId = file.ProjectId,
            
            UserId = file.UserId, 
            CreatedBy =file.CreatedBy,
            CreatedAt =file.CreatedAt,
            UpdatedBy =file.UpdatedBy,
            UpdatedAt =file.UpdatedAt,
           
        }).ToList();
        
       
        return result;
    }

    public List<DocumentReadDto>? GetFiles(string? type , string? status, int page, int pageSize)
    {
        var files = unitOfWork.Documents.GetDocuments(type,status, page, pageSize);
        
        files = files.ToList();
        if (!files.Any()) return null;
        var result = files.Select(file => new DocumentReadDto()
        {
            Id = file.Id,
            
            Confidential = file.Confidential,
            UniqueName = string.Concat(file.DocumentCode ,"ـV"+ file.Version.ToString() ,"ـR" + file.Revision.ToString() ),
            Version = "V"+(file.Version.ToString() ),
            Type = file.Type,
            Revision = "R" + (file.Revision.ToString() ),
            Title = file.Title,
            PrintSize = file.PrintSize,
            Status = file.Status,
            DateCreated = file.CreatedAt,
            Description = file.Description,
            DocumentCode = file.DocumentCode,
            ProjectId = file.ProjectId,
            
            UserId = file.UserId, 
            CreatedBy =file.CreatedBy,
            CreatedAt =file.CreatedAt,
            UpdatedBy =file.UpdatedBy,
            UpdatedAt =file.UpdatedAt,
        }).ToList();
        
        return result;
    }


    public async Task<FilteredDocumentsDto> GetFilteredFilesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var files = await unitOfWork.Documents.GetDocumentWithRevision();
        
        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = files.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var contractList = files.ToList();

            var paginatedResults = contractList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = paginatedResults
                .Select(file => new DocumentReadDto()
                {
                    Id = file.Id,
                  
                    Confidential = file.Confidential,
                    UniqueName = string.Concat(file.DocumentCode ,"ـV"+ file.Version.ToString() ,"ـR" + file.Revision.ToString() ),
                    Version = "V"+ file.Version.ToString(),
                    Type = file.Type,
                    Revision = "R" + file.Revision.ToString(),
                    Title = file.Title,
                    PrintSize = file.PrintSize,
                    Status = file.Status,
                    DateCreated = file.CreatedAt,
                    Description = file.Description,
                    DocumentCode = file.DocumentCode,
                    ProjectId = file.ProjectId,
                    Revisors = file.Revisors?.Select(r => new RevisorDto()
                    {
                        // Employee = mapper.Map<EmployeeDto>(r.Employee),
                        IsReviewed = r.IsReviewed,
                        EmployeeId = r.EmployeeId,
                        Notes = r.Notes,
                        RevisionDate = r.RevisionDate,
                    }).ToList(),
                    UserId = file.UserId, 
                    FileUsers = file.FileUsers?.Select(x => new FileUsers()
                    {
                        FileUserId = x.FileUserId,
                        DocumentId = x.DocumentId,
                        Read = x.Read,
                        Write = x.Write,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy,
                    }).ToList(),
                    CreatedBy =file.CreatedBy,
                    CreatedAt =file.CreatedAt,
                    UpdatedBy =file.UpdatedBy,
                    UpdatedAt =file.UpdatedAt,
                }).ToList();
        
           
          
            FilteredDocumentsDto result = new()
            {
                FileDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (files != null)
        {
            IEnumerable<Document> filteredResults =
                // Apply the first filter
                ApplyFilter(files, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(files, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedFile = paginatedResults
                .Select(file => new DocumentReadDto()
                {
                    Id = file.Id,
                    UniqueName = string.Concat(file.DocumentCode ,"V"+ file.Version.ToString() ,"R" + file.Revision.ToString() ),
                    Confidential = file.Confidential,
                    Version = "V"+(file.Version.ToString() ),
                    Type = file.Type,
                    Revision = "R" + (file.Revision.ToString() ),
                    Title = file.Title,
                    PrintSize = file.PrintSize,
                    Status = file.Status,
                    Revisors = file.Revisors?.Select(r => new RevisorDto()
                    {
                        // Employee = mapper.Map<EmployeeDto>(r.Employee),
                        IsReviewed = r.IsReviewed,
                        EmployeeId = r.EmployeeId,
                        Notes = r.Notes,
                        RevisionDate = r.RevisionDate,
                    }).ToList(),
                    FileUsers = file.FileUsers?.Select(x => new FileUsers()
                    {
                        FileUserId = x.FileUserId,
                        DocumentId = x.DocumentId,
                        Read = x.Read,
                        Write = x.Write,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy,
                    }).ToList(),
                    DateCreated = file.CreatedAt,
                    ProjectId = file.ProjectId,
                    UserId = file.UserId, 
                    DocumentCode = file.DocumentCode,
                    Description = file.Description,
                    CreatedBy =file.CreatedBy,
                    CreatedAt =file.CreatedAt,
                    UpdatedBy =file.UpdatedBy,
                    UpdatedAt =file.UpdatedAt,
                    
                }).ToList();
        
           
            FilteredDocumentsDto filteredDocumentDto = new()
            {
                FileDto = mappedFile,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredDocumentDto;
        }

        return new FilteredDocumentsDto();
    }
    private IEnumerable<Document> ApplyFilter(IEnumerable<Document> files, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => files.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => files.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => files.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => files.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var contractValue) => ApplyNumericFilter(files, column, contractValue, operatorType),
            _ => files
        };
    }

    private IEnumerable<Document> ApplyNumericFilter(IEnumerable<Document> files, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => files.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue == value),
        "neq" => files.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue != value),
        "gte" => files.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue >= value),
        "gt" => files.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue > value),
        "lte" => files.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue <= value),
        "lt" => files.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue < value),
        _ => files
    };
}




    public async Task<List<DocumentReadDto>> GlobalSearch(string searchKey, string? column)
    {

        if (column != null)
        {
            IEnumerable<Document> contractDto = unitOfWork.Documents.GetDocumentWithRevision().Result.Where(e =>
                e.GetPropertyValue(column).ToLower().Contains(searchKey, StringComparison.OrdinalIgnoreCase));
            var contract = contractDto.Select(file => new DocumentReadDto()
            {
                Id = file.Id,
               
            
                Confidential = file.Confidential,
                UniqueName = string.Concat(file.DocumentCode ,"V"+ file.Version.ToString() ,"R" + file.Revision.ToString() ),
                Version = "V"+(file.Version.ToString() ),
                Type = file.Type,
                Revision = "R" + (file.Revision.ToString() ),
                Title = file.Title,
                PrintSize = file.PrintSize,
                Status = file.Status,
                DateCreated = file.CreatedAt,
                Description = file.Description,
                DocumentCode = file.DocumentCode,
                ProjectId = file.ProjectId,
                Revisors = file.Revisors?.Select(r => new RevisorDto()
                {
                    // Employee = mapper.Map<EmployeeDto>(r.Employee),
                    IsReviewed = r.IsReviewed,
                    EmployeeId = r.EmployeeId,
                    Notes = r.Notes,
                    RevisionDate = r.RevisionDate,
                }).ToList(),
                FileUsers = file.FileUsers?.Select(x => new FileUsers()
                {
                    FileUserId = x.FileUserId,
                    DocumentId = x.DocumentId,
                    Read = x.Read,
                    Write = x.Write,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                }).ToList(),
                UserId = file.UserId, 
                CreatedBy =file.CreatedBy,
                CreatedAt =file.CreatedAt,
                UpdatedBy =file.UpdatedBy,
                UpdatedAt =file.UpdatedAt,
            }).ToList();
            

            return (contract.ToList());
        }

        var  contractDtos = await unitOfWork.Documents.GlobalSearch(searchKey);
        var files = contractDtos.Select(file => new DocumentReadDto()
        {
            Id = file.Id,
            
            Confidential = file.Confidential,
            UniqueName = string.Concat(file.DocumentCode ,"V"+ file.Version.ToString() ,"R" + file.Revision.ToString() ),
            Version = "V"+(file.Version.ToString() ),
            Type = file.Type,
            Revision = "R" + (file.Revision.ToString() ),
            Title = file.Title,
            PrintSize = file.PrintSize,
            Status = file.Status,
            DateCreated = file.CreatedAt,
            DocumentCode = file.DocumentCode,
            ProjectId = file.ProjectId,
            Description = file.Description,
            UserId = file.UserId,
            Revisors = file.Revisors?.Select(r => new RevisorDto()
            {
                // Employee = mapper.Map<EmployeeDto>(r.Employee),
                IsReviewed = r.IsReviewed,
                EmployeeId = r.EmployeeId,
                Notes = r.Notes,
                RevisionDate = r.RevisionDate,
            }).ToList(),
            FileUsers = file.FileUsers?.Select(x => new FileUsers()
            {
                FileUserId = x.FileUserId,
                DocumentId = x.DocumentId,
                Read = x.Read,
                Write = x.Write,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
            }).ToList(),
            CreatedBy =file.CreatedBy,
            CreatedAt =file.CreatedAt,
            UpdatedBy =file.UpdatedBy,
            UpdatedAt =file.UpdatedAt,
        }).ToList();
        
        
        return (files.ToList());
    }

    public async Task<List<DocumentFileReadDto>?> GetDocumentFiles(int documentId)
    {
        var files = await unitOfWork.DocumentFiles.GetDocumentFiles(documentId);
        if (!files.Any()) return null;
        var result = files.Select(file => new DocumentFileReadDto()
        {
            Id = file.Id,
            Path = file.Path,
            Name = file.Name,
            Size = file.Size,
            Type = file.Type,
            FileNumber = file.FileNumber,
            DocumentId = file.DocumentId,
        }).ToList();
        return result;
    }
    
    
    private async Task<int> AddDocumentToChain(Document newDocument, string documentCode)
    {
        // Step 1: Retrieve the document chain
        var documents = await unitOfWork.Documents.GetDocumentChain(documentCode);
    
        // Check if there are any documents in the chain
        if (documents == null || documents.ToList().Count == 0)
        {
            // If there are no documents, set the new document as first and last
            newDocument.First = newDocument.Id; // Set First
            newDocument.Last = newDocument.Id;  // Set Last
        }
        else
        {
            // Step 2: Identify the last document in the chain
            var lastDocument = documents.Last(); // Assuming documents are sorted by order

            // Step 3: Update links for the new document
            lastDocument.Next = newDocument.Id;  // Set the last document's Next to the new document
            newDocument.Previous = lastDocument.Id; // Set new document's Previous to the last document

            // Step 4: Update the Last property of the chain's head document
            if (documents.ToList().Count == 1)
            {
                // If there's only one document, update First and Last
                documents.First().Last = newDocument.Id; // Set the first document's Last to the new document
                newDocument.First = documents.First().Id; // Set new document's First to the first document
            }
            unitOfWork.Documents.Update(lastDocument);
        }

        // Step 5: Save the new document and the updated last document
        unitOfWork.Documents.Add(newDocument);
       
        return await unitOfWork.SaveChangesAsync(); // Persist changes
    }
}
