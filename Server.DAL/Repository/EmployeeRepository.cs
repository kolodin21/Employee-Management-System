using Models;
using Server.DAL.Configuration;

namespace Server.DAL.Repository;

public class EmployeeRepository
{

    DatabaseConfig dbConfig = new DatabaseConfig();
    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT * FROM function_add_employee(@name,@surname,@patronymic,@department_id,@position_id,@hire_date,@date_of_dismissal)";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", employee.Name);
        command.Parameters.AddWithValue("@surname", employee.Surname);
        command.Parameters.AddWithValue("@patronymic", employee.Patronymic);
        command.Parameters.AddWithValue("@department", employee.DepartmentId);
        command.Parameters.AddWithValue("@position", employee.PositionId);
        command.Parameters.AddWithValue("@hire_date", employee.HireDate);
        command.Parameters.AddWithValue("@date_of_dismissal", employee.DateOfDismissal);
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

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT * FROM view_employees";
        await using var command = new NpgsqlCommand(sql, connection);
        await connection.OpenAsync();
        var result = await command.ExecuteReaderAsync();
        var employees = new List<EmployeeDto>();
        while (await result.ReadAsync())
        {
            var employee = new EmployeeDto()
            {
                EmployeeId = result.GetInt32(0),
                PersonId = result.GetInt32(1),
                Name = result.GetString(2),
                Surname = result.GetString(3),
                Patronymic = result.GetString(4),
                DepartmentId = result.GetInt32(5),
                Department = result.GetString(6),
                PositionId = result.GetInt32(7),
                Position = result.GetString(8),
                HireDate = result.DateOnly.FromDateTime(result.GetDateTime(9),
                    DateOfDismissal = result.DateOnly.FromDateTime(result.GetDateTime(10);
            };
            employees.Add(employee);
        }
        await connection.CloseAsync();
        return employees;
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
    {
        var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT * FROM view_employees WHERE id = @id";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        await connection.OpenAsync();
        var result = await command.ExecuteReaderAsync();
        var employees = new List<EmployeeDto>();
        while (await result.ReadAsync())
        {
            var employee = new EmployeeDto()
            {
                EmployeeId = result.GetInt32(0),
                PersonId = result.GetInt32(1),
                Name = result.GetString(2),
                Surname = result.GetString(3),
                Patronymic = result.GetString(4),
                DepartmentId = result.GetInt32(5),
                Department = result.GetString(6),
                PositionId = result.GetInt32(7),
                Position = result.GetString(8),
                HireDate = result.DateOnly.FromDateTime(result.GetDateTime(9),
                    DateOfDismissal = result.DateOnly.FromDateTime(result.GetDateTime(10);
            };
            employees.Add(employee);
        }
        await connection.CloseAsync();
        return employees;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int departmentId)
    {
        var connection = new NpgsqlConnection(dbConfig.ConnectionString);
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
                PersonId = result.GetInt32(1),
                Name = result.GetString(2),
                Surname = result.GetString(3),
                Patronymic = result.GetString(4),
                DepartmentId = result.GetInt32(5),
                Department = result.GetString(6),
                PositionId = result.GetInt32(7),
                Position = result.GetString(8),
                HireDate = result.DateOnly.FromDateTime(result.GetDateTime(9),
                    DateOfDismissal = result.DateOnly.FromDateTime(result.GetDateTime(10);
            };
            employees.Add(employee);
        }
        await connection.CloseAsync();
        return employees;
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        var connection = new NpgsqlConnection(dbConfig.ConnectionString);
        const string sql = "SELECT * FROM function_update_employee(@employee_id, @name,@sur_name,@patronymic,@department_id,@position_id,@hire_date,@date_of_dismissal)";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@employee_id", employee.EmployeeId);
        command.Parameters.AddWithValue("@name", employee.Name);
        command.Parameters.AddWithValue("@sur_name", employee.Surname);
        command.Parameters.AddWithValue("@patronymic", employee.Patronymic);
        command.Parameters.AddWithValue("@department_name", employee.DepartmentId);
        command.Parameters.AddWithValue("@position_name", employee.PositionId);
        command.Parameters.AddWithValue("@hire_date", employee.HireDate);
        command.Parameters.AddWithValue("@date_of_dismissal", employee.DateOfDismissal);
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

}