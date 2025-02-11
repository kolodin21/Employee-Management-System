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

    }
}
