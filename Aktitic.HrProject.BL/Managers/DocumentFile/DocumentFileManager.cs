using System.Security.Cryptography;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Aktitic.HrProject.BL;

public class DocumentFileManager(
    IUnitOfWork unitOfWork,
    IWebHostEnvironment webHostEnvironment) : IDocumentFileManager
{
    public async Task<int> Add(DocumentFileAddDto documentAddDto,int documentId)
    {
       
        var document = unitOfWork.Documents.GetById(documentId);
        var projectId = document?.ProjectId;  
    
            // Determine the file path based on whether ProjectId is provided
            if (documentAddDto.Files.Any())
            {
                var docFiles = new List<DocumentFile>();
                foreach (var docFile in documentAddDto.Files)
                {
                    var projectFolder = projectId.ToString().IsNullOrEmpty() ? "general" : projectId.ToString();
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads/files", projectFolder);

                // Ensure the directory exists
                Directory.CreateDirectory(filePath);

                // Construct the full file path
                var fullFilePath = Path.Combine(filePath, docFile.FileName); 
                fullFilePath = fullFilePath.Replace("\\", "/");
                // Save the file to the specified path
                var file = new DocumentFile();
                await using (var stream =
                             new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await docFile.CopyToAsync(stream);
                    var sha256 = SHA256.Create();
                    if (document != null)
                    {
                        file.FileHash = await sha256.ComputeHashAsync(stream);
                       
                    }                    
                }

                // Store the relative file path in the file entity

                
                file.Path = Path.Combine("uploads/files", projectFolder,
                        docFile.FileName);
                        file.Name = docFile.FileName;
                    file.Size = docFile.Length;
                    file.Type = docFile.ContentType;
                    file.DocumentId = documentId;
                

                docFiles.Add(file);
                } 
                await unitOfWork.DocumentFiles.AddDocumentFileAsync(docFiles);
            }
            unitOfWork.Documents.ComputeHash(documentId);
            return await unitOfWork.SaveChangesAsync();
    }

    public async Task<int> Update(DocumentFileUpdateDto documentUpdateDto, int id)
        {
            var file = unitOfWork.DocumentFiles.GetById(id);
        
            if (file == null) return 0;
            var newPath = file.Path[..file.Path.LastIndexOf('/')];

            // Ensure the directory exists
            Directory.CreateDirectory(newPath);
        
            // Construct the full file path
            var fullFilePath = Path.Combine(newPath, documentUpdateDto.File.FileName);
        
            // Save the file to the specified path
            await using (var stream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await documentUpdateDto.File.CopyToAsync(stream);
                var sha256 = SHA256.Create();
                
                file.FileHash = await sha256.ComputeHashAsync(stream);
            }
        
            // Store the relative file path in the file entity


            file.Path = Path.Combine("uploads/files", newPath.Split("/").Last(),
                documentUpdateDto.File.FileName);
            file.Name = documentUpdateDto.File.FileName;
            file.Size = documentUpdateDto.File.Length;
            file.Type = documentUpdateDto.File.ContentType;
            
            
            file.Description = documentUpdateDto.Description;
            file.DocumentId = documentUpdateDto.DocumentId;
            
            unitOfWork.DocumentFiles.Update(file); 
            // await unitOfWork.SaveChangesAsync();
            unitOfWork.Documents.ComputeHash(file.DocumentId);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var file = await unitOfWork.DocumentFiles.DeleteDocumentAsync(id);
            return await unitOfWork.SaveChangesAsync();
        }
    

        public DocumentFileReadDto? DownloadFile(int id)
        {
            // download logic 
            var file = unitOfWork.DocumentFiles.GetById(id);
            if (file == null) return (null);
            var path = Path.Combine(webHostEnvironment.WebRootPath,file.Path); 
            var fileDto = new DocumentFileReadDto()
            {
                Id = file.Id,
                Path = path,
                Name = file.Name,
                Size = file.Size,
                Type = file.Type,
                FileNumber = file.FileNumber,
                DocumentId = file.DocumentId,
            };
           
            return fileDto;
        }

        public DocumentFileReadDto? Get(int id)
        {
            var file = unitOfWork.DocumentFiles.GetById(id);
            if (file == null) return null;
            var fileDto = new DocumentFileReadDto()
            {
                Id = file.Id,
                Path = file.Path,
                Name = file.Name,
                Size = file.Size,
                Type = file.Type,
                FileNumber = file.FileNumber,
                DocumentId = file.DocumentId,
                CreatedAt = file.CreatedAt,
                UpdatedAt = file.UpdatedAt,
                CreatedBy = file.CreatedBy,
                UpdatedBy = file.UpdatedBy
            };
            return fileDto;
        }

        public async Task<List<DocumentFileReadDto>?> GetDocumentFiles(int documentId, int page = 1, int pageSize = 10)
        {
            var files = await unitOfWork.DocumentFiles.GetAllDocumentFilesAsync(documentId, page, pageSize);
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
                CreatedAt = file.CreatedAt,
                UpdatedAt = file.UpdatedAt,
                CreatedBy = file.CreatedBy,
                UpdatedBy = file.UpdatedBy
            }).ToList();
            return result;
        }
    
}
