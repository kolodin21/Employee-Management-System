using NLog;
using Server.BL

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();

var Logger = LogManager.GetCurrentClassLogger();

Logger.Info("Приложение запущено");

var employeeService = new EmployeeService();
var departmentService = new DepartmentService();
var positionService = new PositionService();
var leaveService = new LeaveService();
var reportSearchService = new ReportSearchService();

#region EmployeeRepository
app.MapGet("/employees", async () => await employeeService.GetEmployeesAsync());
app.MapGet("/employee/{id}", async (int id) => await employeeService.GetEmployeeByIdAsync(id));
app.MapGet("/department/{id}/employees", async (int departmentId) => await employeeService.GetEmployeesByDepartmentAsync(departmentId);

app.MapPost("/employees/new", async (Employee emp) => await employeeService.AddEmployeeAsync(emp));
app.MapPut("/employees", async (Employee emp) => await employeeService.UpdateEmployeeAsync(emp));
app.MapDelete("/employee/{id}", async (int id) => await employeeService.DeleteEmployeeAsync(id));
#endregion

#region DepartmentRepository
app.MapGet("/departments", async () => await departmentService.GetDepartmentsAsync());

app.MapPost("/departments/new", async (string name) => await departmentService.AddDepartmentAsync(name));
app.MapPut("/departments/{id}", async (int id, string newName) => await departmentService.UpdateDepartmentAsync(id,newName));
app.MapDelete("/departments/{id}", async (int id) => await departmentService.DeleteDepartmentAsync(id));
#endregion

#region PositionRepository
app.MapGet("/positions", async () => await positionService.GetPositionsAsync());

app.MapPut("/positions/{id}", async (int id, string newName) => await positionService.UpdatePositionAsync(id,newName));
app.MapPost("/position/new", async (string name) => await positionService.AddPositionAsync(name));
app.Delete("/positions/{id}", async (int id) => await positionService.DeletePositionAsync(id));
#endregion

#region LeaveRepository
app.MapPost("/leave/new", async () => await leaveService.AddLeaveAsync());
app.MapPut("/leaves/{leaveId}", async (int leaveId) => await leaveService.CancelLeaveAsync(leaveId));
app.MapGet("/leaves/{id}", async (int id) => await leaveService.GetLeavesByEmployeeAsync(id));
app.MapGet("/leaves", async (DateTime start, DateTime end) => await leaveService.GetLeavesByDateRangeAsync(start,end));
app.MapGet("/leaves/remainder/{employeeId}", async (int employeeId) => await leaveService.GetLeaveBalanceAsync(employeeId));
#endregion

#region ReportSearchRepository
app.MapGet("/report/employee/{employeeId}", async (int employeeId) => await reportSearchService.GenerateEmployeeReportAsync(employeeId));
app.MapGet("/report/department/{departmentId}", async (int departmentId) => await reportSearchService.GenerateDepartmentReportAsync(departmentId));
app.MapGet("/reports", async (DateTime start, DateTime end) => await reportSearchService.GenerateLeaveStatisticsAsync(start,end));
app.MapGet("reports/search", async (string lastName, string firstName) => await reportSearchService.SearchEmployeesAsync(lastName,firstName));
#endregion

app.Run();
