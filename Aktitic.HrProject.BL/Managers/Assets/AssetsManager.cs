using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class AssetsManager(
    IMapper mapper,
    IUnitOfWork unitOfWork) : IAssetsManager
{
    public Task<int> Add(AssetsAddDto assetsAddDto)
    {
        var assets = new Asset
        {
            AssetName = assetsAddDto.AssetName,
            AssetId = assetsAddDto.AssetId,
            UserId = assetsAddDto.UserId,
            Status = assetsAddDto.Status,
            PurchaseFrom = assetsAddDto.PurchaseFrom,
            PurchaseTo = assetsAddDto.PurchaseTo,
            Manufacturer = assetsAddDto.Manufacturer,
            Model = assetsAddDto.Model,
            SerialNumber = assetsAddDto.SerialNumber,
            Supplier = assetsAddDto.Supplier,
            Condition = assetsAddDto.Condition,
            Value = assetsAddDto.Value,
            Description = assetsAddDto.Description,
            Warranty = assetsAddDto.Warranty,
        };
        
        unitOfWork.Assets.Add(assets);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(AssetsUpdateDto assetsUpdateDto, int id)
    {
        var assets = unitOfWork.Assets.GetById(id);
        
        if (assets == null) return Task.FromResult(0);

        if (assetsUpdateDto.AssetName.IsNullOrEmpty()) assets.AssetName = assetsUpdateDto.AssetName;
        if (assetsUpdateDto.AssetId.IsNullOrEmpty()) assets.AssetId = assetsUpdateDto.AssetId;
        if (assetsUpdateDto.UserId != 0) assets.UserId = assetsUpdateDto.UserId;
        if (assetsUpdateDto.Status.IsNullOrEmpty()) assets.Status = assetsUpdateDto.Status;
        if (assetsUpdateDto.PurchaseFrom != assets.PurchaseFrom ) 
            assets.PurchaseFrom = assetsUpdateDto.PurchaseFrom;
        if (assetsUpdateDto.PurchaseTo != assets.PurchaseTo ) assets.PurchaseTo = assetsUpdateDto.PurchaseTo;
        if (assetsUpdateDto.Manufacturer.IsNullOrEmpty() ) assets.Manufacturer= assetsUpdateDto.Manufacturer;
        if (assetsUpdateDto.Model.IsNullOrEmpty()) assets.Model = assetsUpdateDto.Model;
        if (assetsUpdateDto.SerialNumber.IsNullOrEmpty() ) assets.SerialNumber = assetsUpdateDto.SerialNumber;
        if (assetsUpdateDto.Supplier.IsNullOrEmpty() ) assets.SerialNumber = assetsUpdateDto.SerialNumber;
        if (assetsUpdateDto.Condition.IsNullOrEmpty() ) assets.SerialNumber = assetsUpdateDto.SerialNumber;
        if (assetsUpdateDto.Value.IsNullOrEmpty() ) assets.SerialNumber = assetsUpdateDto.SerialNumber;
        if (assetsUpdateDto.Description.IsNullOrEmpty() ) assets.SerialNumber = assetsUpdateDto.SerialNumber;
        if (assetsUpdateDto.Warranty != assets.Warranty ) assets.SerialNumber = assetsUpdateDto.SerialNumber;

        
        
        unitOfWork.Assets.Update(assets);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var assets = unitOfWork.Assets.GetById(id);
        if (assets==null) return Task.FromResult(0);
        assets.IsDeleted = true;
        unitOfWork.Assets.Update(assets);
        return unitOfWork.SaveChangesAsync();
    }

    public async Task<AssetsReadDto> Get(int id)
    {
        var assets = await unitOfWork.Assets.GetAssetById(id);
        if (assets == null) return null;
        
        return new AssetsReadDto()
        {
            Id = assets.Id,
            AssetName = assets.AssetName,
            AssetId = assets.AssetId,
            UserId = assets.UserId,
            Status = assets.Status,
            PurchaseFrom = assets.PurchaseFrom,
            PurchaseTo = assets.PurchaseTo,
            Manufacturer = assets.Manufacturer,
            Model = assets.Model,
            SerialNumber = assets.SerialNumber,
            Supplier = assets.Supplier,
            Condition = assets.Condition,
            Value = assets.Value,
            Description = assets.Description,
            Warranty = assets.Warranty,
            User = mapper.Map<ApplicationUser,ApplicationUserDto>(assets.User),
            CreatedAt = assets.CreatedAt,
            CreatedBy = assets.CreatedBy,
            UpdatedAt = assets.UpdatedAt,
            UpdatedBy = assets.UpdatedBy,    
        };
    }

    public async Task<List<AssetsReadDto>> GetAll()
    {
        var assets = await unitOfWork.Assets.GetAll();
        return assets.Select(assets => new AssetsReadDto()
        {
            Id = assets.Id,
            AssetName = assets.AssetName,
            AssetId = assets.AssetId,
            UserId = assets.UserId,
            Status = assets.Status,
            PurchaseFrom = assets.PurchaseFrom,
            PurchaseTo = assets.PurchaseTo,
            Manufacturer = assets.Manufacturer,
            Model = assets.Model,
            SerialNumber = assets.SerialNumber,
            Supplier = assets.Supplier,
            Condition = assets.Condition,
            Value = assets.Value,
            Description = assets.Description,
            Warranty = assets.Warranty,
            CreatedAt = assets.CreatedAt,
            CreatedBy = assets.CreatedBy,
            UpdatedAt = assets.UpdatedAt,
            UpdatedBy = assets.UpdatedBy,    
            
        }).ToList();
    }

   

    public async Task<FilteredAssetsDto> GetFilteredAssetsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var assetsList = await unitOfWork.Assets.GetAllAssets();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = assetsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = assetsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var assetsDto = new List<AssetsDto>();
            foreach (var assets in paginatedResults)
            {
                assetsDto.Add(new AssetsDto()
                {
                    Id = assets.Id,
                    AssetName = assets.AssetName,
                    AssetId = assets.AssetId,
                    UserId = assets.UserId,
                    Status = assets.Status,
                    PurchaseFrom = assets.PurchaseFrom,
                    PurchaseTo = assets.PurchaseTo,
                    Manufacturer = assets.Manufacturer,
                    Model = assets.Model,
                    SerialNumber = assets.SerialNumber,
                    Supplier = assets.Supplier,
                    Condition = assets.Condition,
                    Value = assets.Value,
                    Description = assets.Description,
                    Warranty = assets.Warranty,
                    User = mapper.Map<ApplicationUser,ApplicationUserDto>(assets.User),
                    CreatedAt = assets.CreatedAt,
                    CreatedBy = assets.CreatedBy,
                    UpdatedAt = assets.UpdatedAt,
                    UpdatedBy = assets.UpdatedBy,    
                });
            }
            FilteredAssetsDto filteredBudgetsExpensesDto = new()
            {
                AssetsDto = assetsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (assetsList != null)
        {
            IEnumerable<Asset> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(assetsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(assetsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var assetss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(assetss);
            
            var mappedAssets = new List<AssetsDto>();

            foreach (var assets in paginatedResults)
            {
                
                mappedAssets.Add(new AssetsDto()
                {
                    Id = assets.Id,
                    AssetName = assets.AssetName,
                    AssetId = assets.AssetId,
                    UserId = assets.UserId,
                    Status = assets.Status,
                    PurchaseFrom = assets.PurchaseFrom,
                    PurchaseTo = assets.PurchaseTo,
                    Manufacturer = assets.Manufacturer,
                    Model = assets.Model,
                    SerialNumber = assets.SerialNumber,
                    Supplier = assets.Supplier,
                    Condition = assets.Condition,
                    Value = assets.Value,
                    Description = assets.Description,
                    Warranty = assets.Warranty,
                    User = mapper.Map<ApplicationUser,ApplicationUserDto>(assets.User),
                    CreatedAt = assets.CreatedAt,
                    CreatedBy = assets.CreatedBy,
                    UpdatedAt = assets.UpdatedAt,
                    UpdatedBy = assets.UpdatedBy,    
                });
            }
            FilteredAssetsDto filteredAssetsDto = new()
            {
                AssetsDto = mappedAssets,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredAssetsDto;
        }

        return new FilteredAssetsDto();
    }
    private IEnumerable<Asset> ApplyFilter(IEnumerable<Asset> assets, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => assets.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => assets.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => assets.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => assets.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var assetsValue) => ApplyNumericFilter(assets, column, assetsValue, operatorType),
            _ => assets
        };
    }

    private IEnumerable<Asset> ApplyNumericFilter(IEnumerable<Asset> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue < value),
        _ => policys
    };
}


    public Task<List<AssetsDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Asset> enumerable = unitOfWork.Assets.GetAllAssets().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var assets = enumerable.Select(assets => new AssetsDto()
            {
                Id = assets.Id,
                AssetName = assets.AssetName,
                AssetId = assets.AssetId,
                UserId = assets.UserId,
                Status = assets.Status,
                PurchaseFrom = assets.PurchaseFrom,
                PurchaseTo = assets.PurchaseTo,
                Manufacturer = assets.Manufacturer,
                Model = assets.Model,
                SerialNumber = assets.SerialNumber,
                Supplier = assets.Supplier,
                Condition = assets.Condition,
                Value = assets.Value,
                Description = assets.Description,
                Warranty = assets.Warranty,
                User = mapper.Map<ApplicationUser,ApplicationUserDto>(assets.User),
                CreatedAt = assets.CreatedAt,
                CreatedBy = assets.CreatedBy,
                UpdatedAt = assets.UpdatedAt,
                UpdatedBy = assets.UpdatedBy,    
            });
            return Task.FromResult(assets.ToList());
        }

        var  queryable = unitOfWork.Assets.GlobalSearch(searchKey);
        var assetss = queryable.Select(assets => new AssetsDto()
        {
            Id = assets.Id,
            AssetName = assets.AssetName,
            AssetId = assets.AssetId,
            UserId = assets.UserId,
            Status = assets.Status,
            PurchaseFrom = assets.PurchaseFrom,
            PurchaseTo = assets.PurchaseTo,
            Manufacturer = assets.Manufacturer,
            Model = assets.Model,
            SerialNumber = assets.SerialNumber,
            Supplier = assets.Supplier,
            Condition = assets.Condition,
            Value = assets.Value,
            Description = assets.Description,
            Warranty = assets.Warranty,
            User = mapper.Map<ApplicationUser,ApplicationUserDto>(assets.User),
            CreatedAt = assets.CreatedAt,
            CreatedBy = assets.CreatedBy,
            UpdatedAt = assets.UpdatedAt,
            UpdatedBy = assets.UpdatedBy,    
        });
        return Task.FromResult(assetss.ToList());
    }

}
