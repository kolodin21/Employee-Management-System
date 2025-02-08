using NLog;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();

var Logger = LogManager.GetCurrentClassLogger();

Logger.Info("Приложение запущено");

app.Run();

