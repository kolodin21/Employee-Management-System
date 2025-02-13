using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Server.BL
{
    public class EmployeeService : ServiceBase
    {

        public async Task<bool> AddEmployeeAsync(Employee employee) =>
            await repositoryManager.EmployeeRepository.AddEmployeeAsync(employee);


        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync() =>
            await repositoryManager.EmployeeRepository.GetEmployeesAsync();


        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id) =>
            await repositoryManager.EmployeeRepository.GetEmployeeByIdAsync(id);


        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int departmentId) =>
            await repositoryManager.EmployeeRepository.GetEmployeesByDepartmentAsync(departmentId);
       

        public async Task UpdateEmployeeAsync(Employee employee) =>
            await repositoryManager.EmployeeRepository.UpdateEmployeeAsync(employee);
       

        public async Task DeleteEmployeeAsync(int employeeId) =>
            await repositoryManager.EmployeeRepository.DeleteEmployeeAsync(employeeId);
    }
}
