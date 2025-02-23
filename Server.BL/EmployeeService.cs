using Models;
using NLog;

namespace Server.BL
{
    public class EmployeeService : ServiceBase
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            ClearCache(CacheKey.AllEmployees,Logger);
            return await ExecuteWithLoggingAsync(
                () => RepositoryManager.EmployeeRepository.AddEmployeeAsync(employee),
                Logger,
                $"Успешно добавлен сотрудник {employee.Surname} {employee.Name}",
                "Ошибка добавления сотрудника");
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync() =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.EmployeeRepository.GetEmployeesAsync(),
                Logger,
                "Успешно получены сотрудники",
                "Ошибка получения сотрудников",
                CacheKey.AllEmployees);

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.EmployeeRepository.GetEmployeeByIdAsync(id),
                Logger,
                $"Сотрудник с id : {id} найден",
                $"Ошибка получения сотрудника с id {id}");

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int departmentId) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.EmployeeRepository.GetEmployeesByDepartmentAsync(departmentId),
                Logger,
                $"Сотрудники департамента с id : {departmentId} найдены",
                $"Ошибка получения сотрудников департамента с id {departmentId}");

        public async Task<bool> UpdateEmployeeAsync(Employee employee) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.EmployeeRepository.UpdateEmployeeAsync(employee),
                Logger,
                $"Успешно обновлен сотрудник {employee.Id} {employee.Surname}",
                "Ошибка обновления сотрудника");

        public async Task<bool> DeleteEmployeeAsync(int id) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.EmployeeRepository.DeleteEmployeeAsync(id),
                Logger,
                $"Успешно уволен сотрудник с id : {id}",
                $"Ошибка увольнения сотрудника с id : {id}");
    }
}

