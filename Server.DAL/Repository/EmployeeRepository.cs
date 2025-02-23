using Models;
using Npgsql;

namespace Server.DAL.Repository;

public class EmployeeRepository : BaseRepository
{
    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);

        const string sql = "SELECT function_add_employee(@name,@surname,@patronymic,@department_id,@position_id,@hire_date)";

        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@name", employee.Name);
        command.Parameters.AddWithValue("@surname", employee.Surname);
        command.Parameters.AddWithValue("@patronymic", employee.Patronymic ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@department_id", employee.DepartmentId);
        command.Parameters.AddWithValue("@position_id", employee.PositionId);
        command.Parameters.AddWithValue("@hire_date", employee.HireDate);

        await connection.OpenAsync();

        return (bool)(await command.ExecuteScalarAsync() ?? false);
    }


    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        var employees = new List<EmployeeDto>();

        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT * FROM view_employees WHERE dismissal_date is null";

        await connection.OpenAsync();
        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var employee = new EmployeeDto
            {
                EmployeeId = reader.GetInt32(0),
                Id = reader.GetInt32(1),
                Name = reader.GetString(2),
                Surname = reader.GetString(3),
                Patronymic = reader.GetString(4),
                DepartmentId = reader.GetInt32(5),
                Department = reader.GetString(6),
                PositionId = reader.GetInt32(7),
                Position = reader.GetString(8),
                HireDate = reader.GetDateTime(9), // Теперь это DateTime
            };

            employees.Add(employee);
        }

        return employees;
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT * FROM view_employees WHERE id = @id";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", id);

        await connection.OpenAsync();

        var result = await command.ExecuteReaderAsync();
        
        while (await result.ReadAsync())
        {
            var employee = new EmployeeDto()
            {
                Id = result.GetInt32(1),
                EmployeeId = result.GetInt32(0),
                Name = result.GetString(2),
                Surname = result.GetString(3),
                Patronymic = result.GetString(4),
                DepartmentId = result.GetInt32(5),
                Department = result.GetString(6),
                PositionId = result.GetInt32(7),
                Position = result.GetString(8),
                HireDate = result.GetDateTime(9),
                DateOfDismissal = result.GetDateTime(10)
            };
            return employee;
        }
        return null;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int departmentId)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT * FROM view_employees WHERE department_id = @departmentId";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@departmentId", departmentId);

        await connection.OpenAsync();

        var result = await command.ExecuteReaderAsync();

        var employees = new List<EmployeeDto>();

        while (await result.ReadAsync())
        {
            var employee = new EmployeeDto()
            {
                EmployeeId = result.GetInt32(0),
                Id = result.GetInt32(1),
                Name = result.GetString(2),
                Surname = result.GetString(3),
                Patronymic = result.GetString(4),
                DepartmentId = result.GetInt32(5),
                Department = result.GetString(6),
                PositionId = result.GetInt32(7),
                Position = result.GetString(8),
                HireDate =result.GetDateTime(9),
                DateOfDismissal = result.GetDateTime(10)
            };
            employees.Add(employee);
        }
        return employees;
    }

    public async Task<bool> UpdateEmployeeAsync(EmployeeDto employee)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);

        const string sql = "SELECT function_update_employee(@employee_id, @name,@sur_name,@patronymic,@department_id,@position_id,@date_hire)";

        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@employee_id", employee.EmployeeId);
        command.Parameters.AddWithValue("@name", employee.Name);
        command.Parameters.AddWithValue("@sur_name", employee.Surname);
        command.Parameters.AddWithValue("@patronymic", employee.Patronymic ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@department_id", employee.DepartmentId);
        command.Parameters.AddWithValue("@position_id", employee.PositionId);
        command.Parameters.AddWithValue("@date_hire", employee.HireDate);

        await connection.OpenAsync();

        return (bool)(await command.ExecuteScalarAsync() ?? false);
    }
    
    public async Task<bool> DismissEmployeeAsync(int id,DateTime dateOfDismissal)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "SELECT function_delete_employee(@employee_id,@date_of_dismissal)";

        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@employee_id", id);
        command.Parameters.AddWithValue("@date_of_dismissal", dateOfDismissal);

        await connection.OpenAsync();

        return (bool)(await command.ExecuteScalarAsync() ?? false);
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        const string sql = "DELETE FROM table_employees WHERE id = @id";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", id);

        await connection.OpenAsync();

        return await command.ExecuteNonQueryAsync() > 0;
    }

}