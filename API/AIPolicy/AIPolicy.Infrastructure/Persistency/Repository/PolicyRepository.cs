using AIPolicy.Core.Entity;
using AIPolicy.Core.Interface.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

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
                            SELECT id, version, name, last_change AS LastChange 
                            FROM public.""Policy""";
    private const string PolicyGetByIdQuery = @"
                            SELECT id, version, name, last_change AS LastChange 
                            FROM public.""Policy"" 
                            WHERE id = @Id";
    private const string PolicyGetCompleteQuery = @"
                            SELECT 
                                p.id, p.version, p.name, p.last_change AS LastChange,
                                t.id, t.id_policy AS IdPolicy, t.version, t.name, t.active, t.run, t.attack_valid AS AttackValid, t.last_change AS LastChange,
                                c.id, c.id_trigger AS IdTrigger, c.type, c.value, 
                                c.condition_left_id AS ConditionLeftId, c.subnode_l AS SubNodeL,
                                c.condition_right_id AS ConditionRightId, c.subnode_r AS SubNodeR, c.last_change AS LastChange
                            FROM public.""Policy"" p
                            LEFT JOIN public.""Trigger"" t ON p.id = t.id_policy
                            LEFT JOIN public.""Condition"" c ON t.id = c.id_trigger
                            WHERE p.id = @Id";
    private const string UpdatePolicyQuery = @"
                            UPDATE public.""Policy""
                            SET version = @Version, name = @Name, last_change = @LastChange
                            WHERE id = @Id";
    private const string DeletePolicyQuery = @"
                           DELETE FROM public.""Policy"" WHERE id = @Id";
    #endregion

    #region Constructor
    public PolicyRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
    }
    #endregion

    #region Methods
    private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    public async Task<int> AddAsync(Policy policy)
    {
        using var connection = CreateConnection();
        return await connection.ExecuteScalarAsync<int>(PolicyInsertQuery, policy);
    }
    public async Task DeleteAsync(int id)
    {
        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(DeletePolicyQuery, new { Id = id });
        if (rowsAffected == 0)
            throw new KeyNotFoundException($"Policy with ID {id} not found.");
    }
    public async Task<IEnumerable<Policy>> GetAllAsync()
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Policy>(PolicyGetAllQuery);
    }
    public async Task<Policy> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();
        var policy = await connection.QuerySingleOrDefaultAsync<Policy>(PolicyGetByIdQuery, new { Id = id });

        return policy ?? throw new KeyNotFoundException($"Policy with ID {id} not found.");
    }
    public async Task<Policy> GetCompletePolicyAsync(int id)
    {
        using var connection = CreateConnection();
        var policies = new Dictionary<int, Policy>();
        var triggers = new Dictionary<int, Trigger>();
        var conditions = new Dictionary<int, Condition>();

        await connection.QueryAsync<Policy, Trigger, Condition, Policy>(
            PolicyGetCompleteQuery,
            (policy, trigger, condition) =>
            {
                if (!policies.TryGetValue(policy.Id, out var existingPolicy))
                {
                    existingPolicy = policy;
                    existingPolicy.Triggers = new List<Trigger>();
                    policies.Add(policy.Id, existingPolicy);
                }

                if (trigger != null && !triggers.ContainsKey(trigger.Id))
                {
                    existingPolicy.Triggers.Add(trigger);
                    triggers.Add(trigger.Id, trigger);
                }

                if (condition != null && !conditions.ContainsKey(condition.Id))
                {
                    conditions.Add(condition.Id, condition);
                    if (trigger?.Id == condition.IdTrigger)
                    {
                        triggers[condition.IdTrigger].RootCondition ??= BuildConditionTree(condition, conditions);
                    }
                }

                return existingPolicy;
            },
            new { Id = id },
            splitOn: "id,id,id");

        return policies.Values.FirstOrDefault() ?? throw new KeyNotFoundException($"Policy with ID {id} not found.");
    }
    public async Task UpdateAsync(Policy entity)
    {
        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(UpdatePolicyQuery, entity);
        if (rowsAffected == 0)
            throw new KeyNotFoundException($"Policy with ID {entity.Id} not found.");
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// Recursively builds a binary condition tree starting from a root condition node.
    /// Each node can link to left and right conditions based on their IDs.
    /// </summary>
    /// <param name="root">The root node from which to start building the tree.</param>
    /// <param name="conditions">A dictionary mapping condition IDs to their corresponding Condition objects.</param>
    /// <returns>The fully constructed condition tree with all linked conditions.</returns>
    private Condition BuildConditionTree(Condition root, Dictionary<int, Condition> conditions)
    {
        if (root.ConditionLeftId != 0 && conditions.TryGetValue(root.ConditionLeftId, out var left))
        {
            root.ConditionLeft = BuildConditionTree(left, conditions);
        }

        if (root.ConditionRightId != 0 && conditions.TryGetValue(root.ConditionRightId, out var right))
        {
            root.ConditionRight = BuildConditionTree(right, conditions);
        }

        return root;
    }
    #endregion
}
