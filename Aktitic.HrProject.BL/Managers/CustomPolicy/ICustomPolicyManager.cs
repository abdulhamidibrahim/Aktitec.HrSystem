using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface ICustomPolicyManager
{
    public Task<int> Add(CustomPolicyAddDto customPolicyAddDto);
    public Task<int> Update(CustomPolicyUpdateDto customPolicyUpdateDto, int id);
    public Task<int> Delete(int id);
    public CustomPolicyReadDto? Get(int id);
    public List<CustomPolicyReadDto>? GetByType(string type);
    public Task<List<CustomPolicyReadDto>> GetAll();
    
}