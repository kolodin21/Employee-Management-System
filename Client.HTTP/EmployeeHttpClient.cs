using NLog;
using System.Net.Http;

namespace Client.HTTP
{
    public class EmployeeHttpClient : BaseHttpClient
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region URI
        public static string DeleteEmployeeUri(int id) => $"{Host}/employee/{id}";
        public static string AddEmployeeUri() => $"{Host}/employees/new";
        public static string GetEmployeesUri() => $"{Host}/employees";
        public static string GetEmployeeByIdUri(int id) => $"{Host}/employee/{id}";
        public static string GetEmployeesByDepartmentUri(int id) => $"{Host}/department/{id}/employees";
        public static string UpdateEmployeeUri() => $"{Host}/employees";

        #endregion


        public async Task<bool> DeleteEmployeeAsync(int id) =>
            await HttpRequest(async () => await Client.DeleteAsync(DeleteEmployeeUri(id)), Logger, "Ошибка удаления сотрудника");


    }
}
