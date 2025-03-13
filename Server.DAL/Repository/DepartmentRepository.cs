using Models;
using Npgsql;

namespace Server.DAL.Repository;

public class DepartmentRepository : BaseRepository
{

    public async Task<bool> AddDepartmentAsync(string name)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);

        const string sql = "SELECT function_add_department(@dep_name)";

        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@dep_name", name);

        await connection.OpenAsync();
        
        return (bool)(await command.ExecuteScalarAsync() ?? false);
    }


    public async Task<bool> UpdateDepartmentAsync(int id, string name)
    {
        var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT function_update_department(@dep_id,@dep_name)";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@dep_id", id);
        command.Parameters.AddWithValue("@dep_name", name);

        await connection.OpenAsync();
        return (bool?)await command.ExecuteScalarAsync() ?? false;
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT * FROM table_departments";

        await using var command = new NpgsqlCommand(sql, connection);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        var departments = new List<Department>();

        while (await reader.ReadAsync())
        {
            departments.Add(new Department
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return departments;
    }
}