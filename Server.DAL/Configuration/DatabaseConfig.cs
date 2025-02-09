using Microsoft.Extensions.Configuration;

namespace Server.DAL.Configuration;

public class DatabaseConfig
{
    private static readonly IConfiguration Configuration;

    static DatabaseConfig()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(@"Configuration\appsetting.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string ConnectionString => Configuration["ConnectionString:TestConnection"]
                                             ?? throw new InvalidOperationException(
                                                 "ConnectionStrings not found in configuration.");

    
}