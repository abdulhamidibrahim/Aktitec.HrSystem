using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IPromotionManager
{
    public Task<int> Add(PromotionAddDto promotionAddDto);
    public Task<int> Update(PromotionUpdateDto promotionUpdateDto, int id);
    public Task<int> Delete(int id);
    public PromotionReadDto? Get(int id);
    public Task<List<PromotionReadDto>> GetAll();
    public Task<FilteredPromotionsDto> GetFilteredPromotionAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<PromotionDto>> GlobalSearch(string searchKey,string? column);
  
}