using Microsoft.Extensions.Configuration;

namespace Client.HTTP.Configuration
{
    public static class HttpConfig
    {
         private static readonly IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(@"Configuration/appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static string? Host => Configuration["HttpSettings:Host"];
    }
}
