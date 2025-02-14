using NLog;

namespace Client.HTTP
{
    public class EmployeeHttpClient : BaseHttpClient
    {
        //Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static string DeleteEmployeeUri(int id) => $"{Host}/employee/{id}";

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                var response = await Client.DeleteAsync(DeleteEmployeeUri(id));

                switch (response)
                {
                    case null:
                        Logger.Warn("Ответ от сервера пустой.");
                        return false;
                    default:
                        return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Logger.Warn($"Ошибка при удалении сотрудника: {ex.Message}");
                return false;
            }
        }
    }
}
