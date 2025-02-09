using Models;

namespace Server.DAL.Repository;

public class EmployeeRepository
{

    public async Task AddEmployeeAsync(Employee employee)
    {

    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        return null;
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
    {
        return null;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int departmentId)
    {
        return null;
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {

    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {

    }

}