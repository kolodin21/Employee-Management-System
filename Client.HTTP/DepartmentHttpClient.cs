using Models;
using NLog;
using System.Net.Http.Json;

namespace Client.HTTP
{
    public class DepartmentHttpClient : BaseHttpClient
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static string AddDepartmentUri() => $"{Host}/departments/new";
        public static string UpdateDepartmentUri(int id) => $"{Host}/departments/{id}";
        public static string DeleteDepartmentUri(int id) => $"{Host}/departments/{id}";

        // Пример использования
        public async Task<bool> AddDepartmentAsync(Department department) =>
            await HttpRequest(async () => await Client.PostAsJsonAsync(AddDepartmentUri(), department), Logger, "Ошибка добавления департамента");

        public async Task<bool> UpdateDepartmentAsync(int id, string newName) =>
            await HttpRequest(async () => await Client.PutAsJsonAsync(UpdateDepartmentUri(id), new { Name = newName }), Logger, "Ошибка обновления департамента");

        public async Task<bool> DeleteDepartmentAsync(int id) =>
            await HttpRequest(async () => await Client.DeleteAsync(DeleteDepartmentUri(id)), Logger, "Ошибка удаления департамента");

    }
}
