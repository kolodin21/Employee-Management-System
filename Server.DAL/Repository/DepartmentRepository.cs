using Models;

namespace Server.DAL.Repository;

public class DepartmentRepository
{
    
    public async Task<int> AddDepartmentAsync(string name)
    {
        var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT * FROM function_add_department(@dep_name)";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@dep_name", name);
        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        var departmentId = 0;
        while (await reader.ReadAsync())
        {
            departmentId = reader.GetInt(0);
        }
        await connection.CloseAsync();
        return departmentId;
    }
    
    public async Task<bool> UpdateDepartmentAsync(int id, string name)
    {
        var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT * FROM function_update_department(@dep_id,@dep_name)";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@dep_id", id);
        command.Parameters.AddWithValue("@dep_name", name);
        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        var result = false;
        while (await reader.ReadAsync())
        {
            result = reader.GetBool(0);
        }
        await connection.CloseAsync();
        return result;
    }
    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT * FROM table_departments";
        await using var command = new NpgsqlCommand(sql, connection);
        await connection.OpenAsync();
        var result = await command.ExecuteReaderAsync();
        var departments = new List<Department>();
        while (await result.ReadAsync())
        {
            var department = new Department()
            {
                Id = result.GetInt32(0),
                Name = result.GetString(1),
            };
            departments.Add(department);
        }
        await connection.CloseAsync();
        return departments;
    }
    
}