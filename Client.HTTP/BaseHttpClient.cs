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


        public async Task<bool> HttpRequestBoolAsync(Func<Task<HttpResponseMessage>> clientRequest, Logger logger)
        {
            try
            {
                var response = await clientRequest();

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                logger.Warn($"Неуспешный статус ответа: {response.StatusCode}. Ответ: {errorContent}. " +
                            $"Вызвано из метода: {new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name}");
                return false;

            }
            catch (Exception ex)
            {
                logger.Warn($"{ex.Message}. " +
                            $"Вызвано из метода: {new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name}");
                return false;   
            }
        }

        public async Task<IEnumerable<T>> HttpRequestResultAsync<T>(
            Func<Task<HttpResponseMessage>> clientRequest, Logger logger)
        {
            try
            {
                var response = await clientRequest();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
                    return result ?? [];
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                logger.Warn($"Неуспешный статус ответа: {response.StatusCode}. Ответ: {errorContent}. " +
                            $"Вызвано из метода: {new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name}");
                return [];
            }
            catch (JsonException jsonEx)
            {
                logger.Warn($"Ошибка десериализации JSON: {jsonEx.Message}" + 
                            $"Вызвано из метода: {new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name}");
                return [];
            }
            catch (Exception ex)
            {
                logger.Warn($"{ex.Message}. " +
                            $"Вызвано из метода: {new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name}");
                return [];
            }
        }

        protected async Task<T?> HttpRequestResultSingleAsync<T>(
            Func<Task<HttpResponseMessage>> clientRequest, Logger logger)
        {
            try
            {
                var response = await clientRequest();

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                logger.Warn($"Неуспешный статус ответа: {response.StatusCode}. Ответ: {errorContent}. " +
                            $"Вызвано из метода: {new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name}");
                return default;

            }
            catch (JsonException jsonEx)
            {
                logger.Warn($"Ошибка десериализации JSON: {jsonEx.Message}" +
                            $"Вызвано из метода: {new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name}");
                return default;
            }
            catch (Exception ex)
            {
                logger.Warn($"{ex.Message}. " +
                            $"Вызвано из метода: {new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name}");
                return default;
            }
        }
    }
}
