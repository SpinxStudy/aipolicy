using AIPolicy.Core.Entity;
using AIPolicy.Core.Interface.Repository;

namespace AIPolicy.Application.Service;

public class TriggerService
{
    private readonly ITriggerRepository _triggerRepository;

    public TriggerService(ITriggerRepository triggerRepository)
    {
        _triggerRepository = triggerRepository;
    }

    public async Task<IEnumerable<Trigger>> GetAllTriggersAsync()
    {
        return await _triggerRepository.GetAllAsync();
    }

    public async Task<Trigger?> GetTriggerByIdAsync(int id)
    {
        return await _triggerRepository.GetByIdAsync(id);
    }

    public async Task<int> CreateTriggerAsync(Trigger trigger)
    {
        return await _triggerRepository.AddAsync(trigger);
    }

    public async Task UpdateTriggerAsync(Trigger trigger)
    {
        var existing = await _triggerRepository.GetByIdAsync(trigger.Id);
        if (existing == null)
            throw new KeyNotFoundException("Trigger not found.");

        await _triggerRepository.UpdateAsync(trigger);
    }

    public async Task DeleteTriggerAsync(int id)
    {
        var existing = await _triggerRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException("Trigger not found.");

        await _triggerRepository.DeleteAsync(id);
    }
}
