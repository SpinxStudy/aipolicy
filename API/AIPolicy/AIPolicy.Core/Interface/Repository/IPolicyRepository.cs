using AIPolicy.Core.Entity;

namespace AIPolicy.Core.Interface.Repository;

public interface IPolicyRepository : IRepository<Policy>
{
    Task<Policy> GetCompletePolicyAsync(int id);
}
