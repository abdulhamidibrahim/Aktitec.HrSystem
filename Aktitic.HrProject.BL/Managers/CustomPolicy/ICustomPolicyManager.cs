using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface ICustomPolicyManager
{
    public Task<int> Add(CustomPolicyAddDto customPolicyAddDto);
    public Task<int> Update(CustomPolicyUpdateDto customPolicyUpdateDto,int id);
    public Task<int> Delete(int id);
    public Task<CustomPolicyReadDto>? Get(int id);
    public Task<List<CustomPolicyReadDto>> GetAll();
}