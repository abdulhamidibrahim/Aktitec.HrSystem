
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class FileManager(
    IUnitOfWork unitOfWork,
    IWebHostEnvironment webHostEnvironment) : IFileManager
{
    public async Task<int> Add(FileAddDto fileAddDto)
    {
        var file = new File()
        {
            FileName = fileAddDto.FileName,
            FileSize = fileAddDto.FileSize,
            UserId = fileAddDto.UserId,
            Status = fileAddDto.Status,
            VersionNumber = fileAddDto.VersionNumber,
            ProjectId = fileAddDto.ProjectId,
            CreatedAt = DateTime.Now,
            CreatedBy = UserUtility.GetUserName(),
        };
        
        // add file users
        if(fileAddDto.Status == Status.Shared)
        {
            file.FileUsers = fileAddDto.FileUsers
                .Select(x => new FileUsers()
                {
                    FileUserId = x.UserId, 
                    FileId = file.Id, 
                    CreatedAt = DateTime.Now, 
                    CreatedBy = UserUtility.GetUserName()
                })
                .ToList();
        }
        
        // upload file  
        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads/files",fileAddDto.ProjectId.ToString());
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        var path = Path.Combine(filePath, fileAddDto.FileName);
        await using var stream = new FileStream(path, FileMode.Create);
        await fileAddDto.File.CopyToAsync(stream);
        file.FilePath = path;
        
        unitOfWork.File.Add(file);
        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<int> Update(FileUpdateDto fileUpdateDto, int id)
    {
        var file = unitOfWork.File.GetById(id);

        if (file == null) return 0;

        // Update properties if provided in the DTO
        if (fileUpdateDto.FileName != null)
        {
            file.FileName = fileUpdateDto.FileName;
        }
        if (!fileUpdateDto.FileSize.IsNullOrEmpty())
        {
            file.FileSize = fileUpdateDto.FileSize;
        }
        if (fileUpdateDto.UserId != 0)
        {
            file.UserId = fileUpdateDto.UserId;
        }
        if (fileUpdateDto.Status != null)
        {
            file.Status = fileUpdateDto.Status;
        }
        if (!fileUpdateDto.VersionNumber.IsNullOrEmpty())
        {
            file.VersionNumber = fileUpdateDto.VersionNumber;
        }
        if (fileUpdateDto.ProjectId != 0)
        {
            file.ProjectId = fileUpdateDto.ProjectId;
        }
        
        if(fileUpdateDto.Status == Status.Shared)
        {
            file.FileUsers = fileUpdateDto.FileUsers
                .Select(x => new FileUsers()
                {
                    FileUserId = x.UserId, 
                    FileId = file.Id, 
                    CreatedAt = DateTime.Now, 
                    CreatedBy = UserUtility.GetUserName()
                })
                .ToList();
        }

        // Handle file upload if a new file is provided
        if (fileUpdateDto.File != null)
        {
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads/files", file.ProjectId.ToString());
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var path = Path.Combine(filePath, fileUpdateDto.FileName);
            await using var stream = new FileStream(path, FileMode.Create);
            await fileUpdateDto.File.CopyToAsync(stream);
            file.FilePath = path;
        }

        file.UpdatedAt = DateTime.Now;
        file.UpdatedBy = UserUtility.GetUserName();
        
        unitOfWork.File.Update(file);
        return await unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var file = unitOfWork.File.GetById(id);
        if (file==null) return Task.FromResult(0);
        file.IsDeleted = true;
        file.DeletedAt = DateTime.Now;
        file.DeletedBy = UserUtility.GetUserName();
        unitOfWork.File.Update(file);
        return unitOfWork.SaveChangesAsync();
    }

    public async Task<List<FileReadDto>>? GetByProjectId(int id)
    {
        var files = await  unitOfWork.File.GetByProjectId(id);
        if (files == null) return null;
        var result= files.Select(file => new FileReadDto()
        {
            Id = file.Id,
            FileName = file.FileName,
            FileSize = file.FileSize,
            File = file.FilePath,
            
            Status = file.Status,
            
            VersionNumber = file.VersionNumber,
            
            ProjectId = file.ProjectId,
            
            UserId = file.UserId, 
            
        }).ToList();
        
        {
            result.ForEach(x => x.FileUsers.AddRange(
                unitOfWork.FileUsers.GetAll().Result
                    .Where(y => y.FileId == x.Id && y.File.Status == Status.Shared)
                    .Select(y => new FileUsersDto()
                    {
                        UserId = y.FileUserId,
                        Status = y.Status
                    })
                    .ToList()));
        }
        return result;
    }
    
    public async Task<List<FileReadDto>> GetAll()
    {
        var files =await unitOfWork.File.GetAll();
        var result= files.Select(file => new FileReadDto()
        {
            Id = file.Id,
            FileName = file.FileName,
            FileSize = file.FileSize,
            File = file.FilePath,
            
            Status = file.Status,
            
            VersionNumber = file.VersionNumber,
            
            ProjectId = file.ProjectId,
            
            UserId = file.UserId, 
            
        }).ToList();
        
        {
            result.ForEach(x => x.FileUsers.AddRange(
                unitOfWork.FileUsers.GetAll().Result
                    .Where(y => y.FileId == x.Id)
                    .Select(y => new FileUsersDto()
                    {
                        UserId = y.FileUserId,
                        Status = y.Status
                    })
                    .ToList()));
        }
        return result;
    }
    
    
         public async Task<FilteredFilesDto> GetFilteredFilesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var contracts = await unitOfWork.File.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = contracts.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var contractList = contracts.ToList();

            var paginatedResults = contractList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = paginatedResults
                .Select(file => new FileReadDto()
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    FileSize = file.FileSize,
                    File = file.FilePath,
            
                    Status = file.Status,
            
                    VersionNumber = file.VersionNumber,
            
                    ProjectId = file.ProjectId,
            
                    UserId = file.UserId, 
            
                }).ToList();
        
            {
                map.ForEach(x => x.FileUsers.AddRange(
                    unitOfWork.FileUsers.GetAll().Result
                        .Where(y => y.FileId == x.Id && y.File.Status == Status.Shared)
                        .Select(y => new FileUsersDto()
                        {
                            UserId = y.FileUserId,
                            Status = y.Status
                        })
                        .ToList()));
            }
            FilteredFilesDto result = new()
            {
                FileDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (contracts != null)
        {
            IEnumerable<File> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(contracts, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(contracts, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedFile = paginatedResults
                .Select(file => new FileReadDto()
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    FileSize = file.FileSize,
                    File = file.FilePath,
            
                    Status = file.Status,
            
                    VersionNumber = file.VersionNumber,
            
                    ProjectId = file.ProjectId,
            
                    UserId = file.UserId, 
            
                }).ToList();
        
            {
                mappedFile.ForEach(x => x.FileUsers.AddRange(
                    unitOfWork.FileUsers.GetAll().Result
                        .Where(y => y.FileId == x.Id && y.File.Status == Status.Shared)
                        .Select(y => new FileUsersDto()
                        {
                            UserId = y.FileUserId,
                            Status = y.Status
                        })
                        .ToList()));
            }
            FilteredFilesDto filteredFileDto = new()
            {
                FileDto = mappedFile,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredFileDto;
        }

        return new FilteredFilesDto();
    }
    private IEnumerable<File> ApplyFilter(IEnumerable<File> contracts, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => contracts.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => contracts.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => contracts.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => contracts.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var contractValue) => ApplyNumericFilter(contracts, column, contractValue, operatorType),
            _ => contracts
        };
    }

    private IEnumerable<File> ApplyNumericFilter(IEnumerable<File> contracts, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue == value),
        "neq" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue != value),
        "gte" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue >= value),
        "gt" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue > value),
        "lte" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue <= value),
        "lt" => contracts.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var contractValue) && contractValue < value),
        _ => contracts
    };
}




    public async Task<List<FileReadDto>> GlobalSearch(string searchKey, string? column)
    {

        if (column != null)
        {
            IEnumerable<File> contractDto;
            contractDto = unitOfWork.File.GetAll().Result.Where(e =>
                e.GetPropertyValue(column).ToLower().Contains(searchKey, StringComparison.OrdinalIgnoreCase));
            var contract = contractDto.Select(file => new FileReadDto()
            {
                Id = file.Id,
                FileName = file.FileName,
                FileSize = file.FileSize,
                File = file.FilePath,
            
                Status = file.Status,
            
                VersionNumber = file.VersionNumber,
            
                ProjectId = file.ProjectId,
            
                UserId = file.UserId, 
            
            }).ToList();
        
            {
                contract.ForEach(x => x.FileUsers.AddRange(
                    unitOfWork.FileUsers.GetAll().Result
                        .Where(y => y.FileId == x.Id && y.File.Status == Status.Shared)
                        .Select(y => new FileUsersDto()
                        {
                            UserId = y.FileUserId,
                            Status = y.Status
                        })
                        .ToList()));
            }

            return (contract.ToList());
        }

        var  contractDtos = await unitOfWork.File.GlobalSearch(searchKey);
        var contracts = contractDtos.Select(file => new FileReadDto()
        {
            Id = file.Id,
            FileName = file.FileName,
            FileSize = file.FileSize,
            File = file.FilePath,
            
            Status = file.Status,
            
            VersionNumber = file.VersionNumber,
            
            ProjectId = file.ProjectId,
            
            UserId = file.UserId, 
            
        }).ToList();
        
        {
            contracts.ForEach(x => x.FileUsers.AddRange(
                unitOfWork.FileUsers.GetAll().Result
                    .Where(y => y.FileId == x.Id && y.File.Status == Status.Shared)
                    .Select(y => new FileUsersDto()
                    {
                        UserId = y.FileUserId,
                        Status = y.Status
                    })
                    .ToList()));
        }
        return (contracts.ToList());
    }
}
