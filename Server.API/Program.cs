using NLog;
using Server.BL

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();

var Logger = LogManager.GetCurrentClassLogger();

Logger.Info("Приложение запущено");

EmployeeService employeeService = new EmployeeService();

#region EmployeeRepository
app.MapGet("/employees", async () => await employeeService.GetEmployeesAsync());
app.MapGet("/employee/{id}", async (int id) => await GetEmployeeByIdAsync(int id));
app.MapGet("/department/{id}/employees", async (int departmentId) => await GetEmployeesByDepartmentAsync(int departmentId);

app.MapPost("/employees", async (Employees emp) => await AddEmployeeAsync(Employees emp));//добавить модель сотрудника
app.MapPut("/employees", async (Employees emp) => await UpdateEmployeeAsync(DTO emp)); //добавить DTO сотрудника
app.MapDelete("/employee/{id}", async (int id) => await DeleteEmployeeAsync(int id));
#endregion

app.Run();
