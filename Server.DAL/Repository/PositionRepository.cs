using Models;
using Npgsql;

namespace Server.DAL.Repository;

public class PositionRepository : BaseRepository
{
    public async Task<bool> AddPositionAsync(string name)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);

        const string sql = "SELECT function_add_position(@position_name)";

        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@position_name", name);

        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return false;

        int positionId = reader.GetInt32(0);

        return positionId > 0;
    }

    public async Task<IEnumerable<Position>> GetPositionsAsync()
    {
        return null;
    }
    
    public async Task<bool> UpdatePositionAsync(int id, string name)
    {
        return false;
    }
    
    public async Task<bool> DeletePositionAsync(int positionId)
    {
        return false;
    }
}