using NLog;
using Server.BL

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();

var Logger = LogManager.GetCurrentClassLogger();

Logger.Info("Приложение запущено");

var employeeService = new EmployeeService();
var departmentService = new DepartmentService();
var positionRepository = new PositionRepository();
var leaveRepository = new LeaveRepository();
var reportSearchRepository = new ReportSearchRepository();

#region EmployeeRepository
app.MapGet("/employees", async () => await employeeService.GetEmployeesAsync());
app.MapGet("/employee/{id}", async (int id) => await employeeService.GetEmployeeByIdAsync(int id));
app.MapGet("/department/{id}/employees", async (int departmentId) => await employeeService.GetEmployeesByDepartmentAsync(int departmentId);

app.MapPost("/employees/new", async (Employees emp) => await employeeService.AddEmployeeAsync(Employees emp));//добавить модель сотрудника
app.MapPut("/employees", async (Employees emp) => await employeeService.UpdateEmployeeAsync(DTO emp)); //добавить DTO сотрудника
app.MapDelete("/employee/{id}", async (int id) => await employeeService.DeleteEmployeeAsync(int id));
#endregion

#region DepartmentRepository
app.MapGet("/departments", async () => await departmentService.GetDepartmentsAsync());

app.MapPost("/departments/new", async (string name) => await departmentService.AddDepartmentAsync(string name));
app.MapPut("/departments/{id}", async (int id, string newName) => await departmentService.UpdateDepartmentAsync(int id, string newName));
app.MapDelete("/departments/{id}", async (int id) => await departmentService.DeleteDepartmentAsync(int id));
#endregion

#region PositionRepository
app.MapGet("/positions", async () => await positionRepository.GetPositionsAsync());

app.MapPut("/positions/{id}", async (int id, string newName) => await positionRepository.UpdatePositionAsync(int id, string newName));
app.MapPost("/position/new", async (string name) => await positionRepository.AddPositionAsync(string name));
app.Delete("/positions/{id}", async (int id) => await positionRepository.DeletePositionAsync(int id));
#endregion

#region LeaveRepository
app.MapPost("/leave/new", async () => await leaveRepository.AddLeaveAsync());// fix me
app.MapPut("/leaves/{leaveId}", async (int leaveId) => await leaveRepository.CancelLeaveAsync(int leaveId));
app.MapGet("/leaves/{id}", async (int id) => await leaveRepository.GetLeavesByEmployeeAsync(int id));
app.MapGet("/leaves", async (DateTime start, DateTime end) => await leaveRepository.GetLeavesByDateRangeAsync(DateTime start, DateTime end));
app.MapGet("/leaves/remainder/{employeeId}", async (int employeeId) => await leaveRepository.GetLeaveBalanceAsync(int employeeId));
#endregion

#region ReportSearchRepository
app.MapGet("/report/employee/{employeeId}", async (int employeeId) => await reportSearchRepository.GenerateEmployeeReportAsync(int employeeId));
app.MapGet("/report/department/{departmentId}", async (int departmentId) => await reportSearchRepository.GenerateDepartmentReportAsync(int departmentId));
app.MapGet("/reports", async (DateTime start, DateTime end) => await reportSearchRepository.GenerateLeaveStatisticsAsync(DateTime start, DateTime end));
app.MapGet("reports/search", async (string lastName, string firstName) => await reportSearchRepository.SearchEmployeesAsync(string lastName, string firstName));
#endregion

app.Run();
