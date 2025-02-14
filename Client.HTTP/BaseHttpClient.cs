using Client.HTTP.Configuration;

namespace Client.HTTP
{
    public class BaseHttpClient
    {
        protected static readonly HttpClient Client = new();
        protected static string Host { get; private set; } = HttpConfig.Host;

    }
}
