using AIPolicy.Core.Entity;
using AIPolicy.Core.Interface.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AIPolicy.Infrastructure.Persistency.Repository;

public class PolicyRepository : IPolicyRepository
{
    private readonly string _connectionString;

    #region SQL Queries
    private const string PolicyInsertQuery = @"
                            INSERT INTO public.""Policy"" (id, version, name, last_change)
                            VALUES (@Id, @Version, @Name, @LastChange)
                            RETURNING id";
    private const string TriggerInsertQuery = @"
                            INSERT INTO public.""Trigger"" (id_policy, version, name, active, run, attack_valid, last_change)
                            VALUES (@IdPolicy, @Version, @Name, @Active, @Run, @AttackValid, @LastChange)
                            RETURNING id";
    private const string ConditionUpdateQuery = "UPDATE public.\"Condition\" SET id_trigger = @IdTrigger, type = @Type, value = @Value, condition_left_id = @ConditionLeftId, subnode_l = @SubNodeL, condition_right_id = @ConditionRightId, subnode_r = @SubNodeR, last_change = @LastChange WHERE id = @Id";
    private const string ConditionInsertQuery = "INSERT INTO public.\"Condition\" (id_trigger, type, value, condition_left_id, subnode_l, condition_right_id, subnode_r, last_change) VALUES (@IdTrigger, @Type, @Value, @ConditionLeftId, @SubNodeL, @ConditionRightId, @SubNodeR, @LastChange) RETURNING id";
    private const string PolicyGetAllQuery = @"
                            SELECT p.*, t.*, c.*
                            FROM public.""Policy"" p
                            LEFT JOIN public.""Trigger"" t ON t.id_policy = p.id
                            LEFT JOIN public.""Condition"" c ON c.id_trigger = t.id
                            WHERE p.id = @Id";
    #endregion

    public PolicyRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<Policy> AddAsync(Policy policy)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            policy.Id = await connection.ExecuteScalarAsync<int>(PolicyInsertQuery, policy, transaction);

            if (policy.Triggers.Any())
            {
                foreach (var trigger in policy.Triggers)
                {
                    trigger.IdPolicy = policy.Id;
                    trigger.Id = await connection.ExecuteScalarAsync<int>(TriggerInsertQuery, trigger, transaction);

                    if (trigger.RootCondition != null)
                    {
                        await InsertConditionAsync(connection, transaction, trigger);
                    }
                }
            }

            await transaction.CommitAsync();
            return await GetByIdAsync(policy.Id);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Policy>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Policy?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var policies = new Dictionary<int, Policy>();
        var triggers = new Dictionary<int, Trigger>();
        var conditions = new Dictionary<int, Condition>();

        await connection.QueryAsync<Policy, Trigger, Condition, Policy>(
            PolicyGetAllQuery,
            (p, t, c) =>
            {
                // Policy
                if (!policies.TryGetValue(p.Id, out var policy))
                {
                    policy = p;
                    policies.Add(p.Id, policy);
                }

                // Trigger
                if (t != null && !triggers.ContainsKey(t.Id))
                {
                    t.RootCondition = null; // Inicializa como null
                    policy.Triggers.Add(t);
                    triggers.Add(t.Id, t);
                }

                // Condition
                if (c != null && !conditions.ContainsKey(c.Id))
                {
                    conditions.Add(c.Id, c);
                    if (t != null && t.Id == c.IdTrigger)
                    {
                        t.RootCondition = c; // Atribui a condição raiz
                    }
                }

                return policy;
            },
            new { Id = id },
            splitOn: "id,id,id");

        return policies.Values.FirstOrDefault();
    }

    public Task UpdateAsync(Policy policy)
    {
        throw new NotImplementedException();
    }

    private async Task InsertConditionAsync(NpgsqlConnection connection, NpgsqlTransaction transaction, Trigger trigger)
    {
        var condition = trigger.RootCondition!;
        var conditionSql = condition.Id > 0 ? ConditionUpdateQuery : ConditionInsertQuery;

        condition.IdTrigger = trigger.Id;
        condition.Id = await connection.ExecuteScalarAsync<int>(conditionSql, condition, transaction);
    }




    public Task<Policy> GetCompletePolicyAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<int> IRepository<Policy>.AddAsync(Policy entity)
    {
        throw new NotImplementedException();
    }
}
