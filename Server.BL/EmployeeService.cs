using Models;

namespace Server.BL
{
    public class EmployeeService : ServiceBase
    {

        public async Task<bool> AddEmployeeAsync(Employee employee) =>
            await RepositoryManager.EmployeeRepository.AddEmployeeAsync(employee);


        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync() =>
            await RepositoryManager.EmployeeRepository.GetEmployeesAsync();


        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id) =>
            await RepositoryManager.EmployeeRepository.GetEmployeeByIdAsync(id);


        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int departmentId) =>
            await RepositoryManager.EmployeeRepository.GetEmployeesByDepartmentAsync(departmentId);
       

        public async Task<bool> UpdateEmployeeAsync(Employee employee) =>
            await RepositoryManager.EmployeeRepository.UpdateEmployeeAsync(employee);
       

        public async Task<bool> DeleteEmployeeAsync(int employeeId) =>
            await RepositoryManager.EmployeeRepository.DeleteEmployeeAsync(employeeId);
    }
}
