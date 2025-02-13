using Models;
using Server.DAL.Configuration;

namespace Server.DAL.Repository;

public class EmployeeRepository
{

    public async Task AddEmployeeAsync(Employee employee)
    {

    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        var dbConfig = new DatabaseConfig();
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
                Id = result.GetInt32(0),
                Name = result.GetString(1),
                Surname = result.GetString(2),
                Patronymic = result.GetString(3),
                DepartmentId = result.GetInt32(4),
                Department = result.GetString(5),
                PositionId = result.GetInt32(6),
                Position = result.GetString(7),
                HireDate = result.DateOnly.FromDateTime(result.GetDateTime(8),
                    DateOfDismissal = result.DateOnly.FromDateTime(result.GetDateTime(9);
            };

            employees.Add(employee);
        }
        await connection.CloseAsync();
        return employees;
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
    {
        return null;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int departmentId)
    {
        return null;
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {

    }

    public async Task<bool> DeleteEmployeeAsync(int employeeId)
    {

    }

}