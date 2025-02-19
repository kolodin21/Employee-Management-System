using NLog;
using System.Net.Http.Json;
using Models;

namespace Client.HTTP
{
    public class EmployeeHttpClient : BaseHttpClient
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static Uri DeleteEmployeeUri(int id) => new Uri($"{Host}/employee/{id}");
        public static Uri AddEmployeeUri() => new Uri($"{Host}/employees/new");
        public static Uri GetEmployeesUri() => new Uri($"{Host}/employees");
        public static Uri GetEmployeeByIdUri(int id) => new Uri($"{Host}/employee/{id}");
        public static Uri GetEmployeesByDepartmentUri(int id) => new Uri($"{Host}/department/{id}/employees");
        public static Uri UpdateEmployeeUri() => new Uri($"{Host}/employees");


        public async Task<bool> AddEmployeeAsync(Employee employee) =>
            await HttpRequestBoolAsync(async () => await Client.PostAsJsonAsync(AddEmployeeUri(), employee), Logger, "Ошибка добавления сотрудника");

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync() =>
            await HttpRequestResultAsync<EmployeeDto>(async () => await Client.GetAsync(GetEmployeesUri()), Logger, "Ошибка получения сотрудников");

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id) =>
            await HttpRequestResultSingleAsync<EmployeeDto>(async () => await Client.GetAsync(GetEmployeeByIdUri(id)), Logger, $"Ошибка получения сотрудника с id {id}");

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(int id) =>
            await HttpRequestResultAsync<EmployeeDto>(async () => await Client.GetAsync(GetEmployeesByDepartmentUri(id)), Logger, $"Ошибка получения сотрудников департамента с id {id}");

        public async Task<bool> UpdateEmployeeAsync(Employee employee) =>
            await HttpRequestBoolAsync(async () => await Client.PutAsJsonAsync(UpdateEmployeeUri(), employee), Logger, "Ошибка обновления сотрудника");

        public async Task<bool> DeleteEmployeeAsync(int id) =>
            await HttpRequestBoolAsync(async () => await Client.DeleteAsync(DeleteEmployeeUri(id)), Logger, $"Ошибка удаления сотрудника с id {id}");

    }
}
