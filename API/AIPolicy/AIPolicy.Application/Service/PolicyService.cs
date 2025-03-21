using AIPolicy.Application.InputModel;
using AIPolicy.Application.ViewModel;
using AIPolicy.Core.Entity;
using AIPolicy.Core.Interface.Repository;
using AutoMapper;

namespace AIPolicy.Application.Service;

public class PolicyService
{
    private readonly IPolicyRepository _policyRepository;
    private readonly IMapper _mapper;

    public PolicyService(IPolicyRepository policyRepository, IMapper mapper)
    {
        _policyRepository = policyRepository;
        _mapper = mapper;
    }


    public async Task<PolicyViewModel> CreateAsync(PolicyInputModel policyInputModel)
    {
        var policy = _mapper.Map<Policy>(policyInputModel);
        policy.LastChange = DateTime.Now;

        var result = await _policyRepository.AddAsync(policy);
        return _mapper.Map<PolicyViewModel>(result);
    }
    public async Task<PolicyViewModel?> GetByIdAsync(int id)
    {
        var policy = await _policyRepository.GetByIdAsync(id);
        return policy == null ? null : _mapper.Map<PolicyViewModel>(policy);
    }
    public async Task<IEnumerable<PolicyViewModel>> GetAllAsync()
    {
        var policies = await _policyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PolicyViewModel>>(policies);
    }
    public async Task UpdateAsync(int id, PolicyInputModel policyInputModel)
    {
        var policy = await _policyRepository.GetByIdAsync(id);
        if (policy == null)
            throw new Exception($"Policy not found. ID {id}");

        _mapper.Map(policyInputModel, policy);
        policy.LastChange = DateTime.Now;

        await _policyRepository.UpdateAsync(policy);
    }

    public async Task DeleteAsync(int id)
    {
        var policy = await _policyRepository.GetByIdAsync(id);
        if (policy == null)
            throw new Exception($"Policy not found. ID {id}");

        await _policyRepository.DeleteAsync(id);
    }
}
