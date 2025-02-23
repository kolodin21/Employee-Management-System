using Models;

namespace Server.DAL.Repository;

public class PositionRepository : BaseRepository
{
    public async Task<bool> AddPositionAsync(string name)
    {
        await using var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT function_add_department(@dep_name)";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@dep_name", name);
        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var result = reader.GetBoolean(0);
            return result;
        }
        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Position>> GetPositionsAsync()
    {
        await using var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT * FROM table_positions";
        await using var command = new NpgsqlCommand(sql, connection);
        await connection.OpenAsync();
        var result = await command.ExecuteReaderAsync();
        var positions = new List<Position>();
        while (await result.ReadAsync())
        {
            var position = new Position()
            {
                Id = result.GetInt32(0),
                Name = result.GetString(1),
            };
            positions.Add(position);
        }
        await connection.CloseAsync();
        return positions;
    }
    
    
    public async Task<bool> UpdatePositionAsync(int id, string name)
    {
        await using var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT function_update_position(@pos_id,@pos_name)";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@pos_id", id);
        command.Parameters.AddWithValue("@pos_name", name);
        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        var result = false;
        while (await reader.ReadAsync())
        {
            result = reader.GetBoolean(0);
        }
        await connection.CloseAsync();
        return result;
    }
    
    public async Task<bool> DeletePositionAsync(int positionId)
    {
        await using var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT function_delete_position(@pos_id)";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@pos_id", positionId);
        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        var result = false;
        while (await reader.ReadAsync())
        {
            result = reader.GetBoolean(0);
        }
        await connection.CloseAsync();
        return result;
    }
}