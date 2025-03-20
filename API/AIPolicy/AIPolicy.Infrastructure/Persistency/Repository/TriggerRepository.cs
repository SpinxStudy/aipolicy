using AIPolicy.Core.Entity;
using AIPolicy.Core.Interface.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AIPolicy.Infrastructure.Persistency.Repository;

public class TriggerRepository : ITriggerRepository
{
    private readonly string _connectionString;
    public const string InsertQuery = @"
                        INSERT INTO trigger (version, name, active, attack_valid, root_conditions, operations)
                                    VALUES (@Version, @Name, @Active, @AttackValid, @RootConditions, @Operations)
                            RETURNING id;";
    public const string DeleteQuery = @"
                        DELETE FROM trigger WHERE id = @Id";
    public const string GetAllQuery = @"
                        SELECT * FROM trigger";
    public const string GetByIdQuery = @"
                        SELECT * FROM trigger WHERE id = @Id";
    public const string UpdateQuery = @"
                        UPDATE trigger 
                        SET version = @Version, name = @Name, active = @Active, 
                            attack_valid = @AttackValid, root_conditions = @RootConditions, 
                            operations = @Operations 
                        WHERE id = @Id";

    public TriggerRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<int> AddAsync(Trigger trigger)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(InsertQuery, trigger);
    }
    public async Task DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(DeleteQuery, new { Id = id });
    }
    public async Task<IEnumerable<Trigger>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<Trigger>(GetAllQuery);
    }
    public async Task<Trigger?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Trigger>(GetByIdQuery, new { Id = id });
    }
    public async Task UpdateAsync(Trigger trigger)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(UpdateQuery, trigger);
    }
}
