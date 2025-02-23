using Models;
using Npgsql;

namespace Server.DAL.Repository;

public class PositionRepository : BaseRepository
{
    public async Task<bool> AddPositionAsync(string name)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT function_add_department(@dep_name)";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@dep_name", name);

        await connection.OpenAsync();

        return (bool)(await command.ExecuteScalarAsync() ?? false);
    }

    public async Task<IEnumerable<Position>> GetPositionsAsync()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT * FROM table_positions";

        await using var command = new NpgsqlCommand(sql, connection);

        await connection.OpenAsync();
        await using var result = await command.ExecuteReaderAsync();

        var positions = new List<Position>();

        while (await result.ReadAsync())
        {
            positions.Add(new Position
            {
                Id = result.GetInt32(0),
                Name = result.GetString(1)
            });
        }
        return positions;
    }
    
    public async Task<bool> UpdatePositionAsync(int id, string name)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT function_update_position(@pos_id,@pos_name)";

        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@pos_id", id);
        command.Parameters.AddWithValue("@pos_name", name);

        await connection.OpenAsync();
        return (bool)(await command.ExecuteScalarAsync() ?? false);
    }
    
    public async Task<bool> DeletePositionAsync(int positionId)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT function_delete_position(@pos_id)";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@pos_id", positionId);

        await connection.OpenAsync();
        return (bool)(await command.ExecuteScalarAsync() ?? false);
    }
}