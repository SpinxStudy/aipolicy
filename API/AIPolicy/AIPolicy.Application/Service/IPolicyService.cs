using AIPolicy.Application.InputModel;
using AIPolicy.Application.ViewModel;

namespace AIPolicy.Application.Service;

public interface IPolicyService
{
    Task<PolicyViewModel> GetByIdAsync(int id);
    Task<IEnumerable<PolicyViewModel>> GetAllAsync();
    Task<PolicyViewModel> GetCompletePolicyAsync(int id);
    Task<int> CreateAsync(PolicyInputModel policyInputModel);
    Task UpdateAsync(int id, PolicyInputModel policyInputModel);
    Task DeleteAsync(int id);
}

