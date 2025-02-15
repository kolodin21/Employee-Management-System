using NLog;
using System.Net.Http;

namespace Client.HTTP
{
    public class EmployeeHttpClient : BaseHttpClient
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static string DeleteEmployeeUri(int id) => $"{Host}/employee/{id}";

        public async Task<bool> DeleteEmployeeAsync(int id) =>
            await HttpRequest(async () => await Client.DeleteAsync(DeleteEmployeeUri(id)), Logger, "Ошибка удаления сотрудника");
    }
}
