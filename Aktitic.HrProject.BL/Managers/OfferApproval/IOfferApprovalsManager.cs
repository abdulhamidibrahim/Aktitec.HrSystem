using Aktitic.HrProject.BL;


namespace Aktitic.HrTaskList.BL;

public interface IOfferApprovalsManager
{
    public Task<int> Add(OfferApprovalAddDto offerApprovalsAddDto);
    public Task<int> Update(OfferApprovalUpdateDto offerApprovalsUpdateDto, int id);
    public Task<int> Delete(int id);
    public OfferApprovalReadDto? Get(int id);
    public Task<List<OfferApprovalReadDto>> GetAll();
    public Task<FilteredOfferApprovalsDto> GetFilteredOfferApprovalsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<OfferApprovalDto>> GlobalSearch(string searchKey,string? column);
  
}