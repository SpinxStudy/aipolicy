using AIPolicy.Core.Entity;

namespace AIPolicy.Core.Interface.Repository
{
    public interface ITriggerRepository
    {
        Task<IEnumerable<Trigger>> GetAllAsync();
        Task<Trigger?> GetByIdAsync(int id);
        Task<int> AddAsync(Trigger trigger);
        Task UpdateAsync(Trigger trigger);
        Task DeleteAsync(int id);
    }
}
