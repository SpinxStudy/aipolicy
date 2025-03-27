using AIPolicy.Application.InputModel;
using AIPolicy.Application.ViewModel;
using AIPolicy.Core.Entity;
using AIPolicy.Core.Interface.Repository;
using AutoMapper;

namespace AIPolicy.Application.Service;

public class PolicyService : IPolicyService
{
    private readonly IPolicyRepository _policyRepository;
    private readonly IMapper _mapper;

    #region Constructor
    public PolicyService(IPolicyRepository policyRepository, IMapper mapper)
    {
        _policyRepository = policyRepository;
        _mapper = mapper;
    }
    #endregion

    #region Methods
    public async Task<int> CreateAsync(PolicyInputModel policyInputModel)
    {
        var policy = _mapper.Map<Policy>(policyInputModel);
        policy.LastChange = DateTime.UtcNow;
        return await _policyRepository.AddAsync(policy);
    }
    public async Task<PolicyViewModel> GetByIdAsync(int id)
    {
        var policy = await _policyRepository.GetByIdAsync(id);
        return _mapper.Map<PolicyViewModel>(policy);
    }
    public async Task<IEnumerable<PolicyViewModel>> GetAllAsync()
    {
        var policies = await _policyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PolicyViewModel>>(policies);
    }
    public async Task<PolicyViewModel> GetCompletePolicyAsync(int id)
    {
        var policy = await _policyRepository.GetCompletePolicyAsync(id);
        return _mapper.Map<PolicyViewModel>(policy);
    }
    public async Task UpdateAsync(int id, PolicyInputModel policyInputModel)
    {
        var policy = await _policyRepository.GetByIdAsync(id);
        if (policy == null)
        {
            throw new Exception("Policy not found!");
        }

        policy = _mapper.Map(policyInputModel, policy);
        policy.LastChange = DateTime.UtcNow;
        await _policyRepository.UpdateAsync(policy);
    }
    public async Task DeleteAsync(int id)
    {
        await _policyRepository.DeleteAsync(id);
    }
    #endregion
}
