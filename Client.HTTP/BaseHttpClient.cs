using System.Net.Http.Json;
using Client.HTTP.Configuration;
using Newtonsoft.Json;
using NLog;

namespace Client.HTTP
{
    public abstract class BaseHttpClient
    {
        protected static readonly HttpClient Client = new();
        protected static string? Host { get; set; } = HttpConfig.Host;


        public async Task<bool> HttpRequestBoolAsync(Func<Task<HttpResponseMessage>> clientRequest, Logger logger,string messageSuccess, string messageError)
        {
            try
            {
                var response = await clientRequest();

                if (response.IsSuccessStatusCode)
                {
                    logger.Info(messageSuccess);
                    return true;
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                logger.Warn($"Неуспешный статус ответа: {response.StatusCode}. Ответ: {errorContent}");
                return false;

            }
            catch (Exception ex)
            {
                logger.Warn($"{messageError}: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<T>> HttpRequestResultAsync<T>(
            Func<Task<HttpResponseMessage>> clientRequest, Logger logger, string messageSuccess, string messageError)
        {
            try
            {
                var response = await clientRequest();

                if (response.IsSuccessStatusCode)
                {
                    logger.Info(messageSuccess);
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
                    return result ?? [];
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                logger.Warn($"Неуспешный статус ответа: {response.StatusCode}. Ответ: {errorContent}");
                return [];
            }
            catch (JsonException jsonEx)
            {
                logger.Warn($"Ошибка десериализации JSON: {jsonEx.Message}");
                return [];
            }
            catch (Exception ex)
            {
                logger.Warn($"{messageError}: {ex.Message}");
                return [];
            }
        }

        public async Task<T?> HttpRequestResultSingleAsync<T>(
            Func<Task<HttpResponseMessage>> clientRequest, Logger logger, string messageSuccess, string messageError)
        {
            try
            {
                var response = await clientRequest();

                if (response.IsSuccessStatusCode)
                {
                    logger.Info(messageSuccess);
                    return await response.Content.ReadFromJsonAsync<T>();
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                logger.Warn($"Неуспешный статус ответа: {response.StatusCode}. Ответ: {errorContent}");
                return default;

            }
            catch (JsonException jsonEx)
            {
                logger.Warn($"Ошибка десериализации JSON: {jsonEx.Message}");
                return default;
            }
            catch (Exception ex)
            {
                logger.Warn($"{message}: {ex.Message}");
                return default;
            }
        }

    }
}
