using AIPolicy.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPolicy.Core.Interface.Repository;

public interface IPolicyRepository
{
    Task<Policy> AddAsync(Policy policy);
    Task<Policy?> GetByIdAsync(int id);
    Task<IEnumerable<Policy>> GetAllAsync();
    Task UpdateAsync(Policy policy);
    Task DeleteAsync(int id);
}
