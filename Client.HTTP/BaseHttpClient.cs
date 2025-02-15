using Client.HTTP.Configuration;
using NLog;

namespace Client.HTTP
{
    public class BaseHttpClient
    {
        protected static readonly HttpClient Client = new();
        protected static string Host { get; private set; } = HttpConfig.Host;

        public async Task<bool> HttpRequest(Func<Task<HttpResponseMessage>> clientRequest, Logger logger, string message)
        {
            try
            {
                var response = await clientRequest();
                if (response == null)
                {
                    logger.Warn("Ответ от сервера пустой.");
                    return false;
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                logger.Warn($"{message}: {ex.Message}");
                return false;
            }
        }

    }
}
