using Microsoft.Extensions.DependencyInjection;
using Models;
using NLog;
using Server.BL;

#region MyRegion

//var builder = WebApplication.CreateBuilder(args);

//var app = builder.Build();

//app.UseHttpsRedirection();

//var Logger = LogManager.GetCurrentClassLogger();


//var employeeService = new EmployeeService();
//var departmentService = new DepartmentService();
//var positionService = new PositionService();
//var leaveService = new LeaveService();
//var reportSearchService = new ReportSearchService();

//#region EmployeeRepository
//app.MapPost("/employees/new", async (Employee emp) => await employeeService.AddEmployeeAsync(emp));

//app.MapGet("/employee/{id:int}", async (int id) => await employeeService.GetEmployeeByIdAsync(id));

//app.MapGet("/department/{id:int}/employees", async (int id) => await employeeService.GetEmployeesByDepartmentAsync(id));
//app.MapPut("/employees", async (Employee emp) => await employeeService.UpdateEmployeeAsync(emp));
//app.MapDelete("/employee/{id:int}", async (int id) => await employeeService.DeleteEmployeeAsync(id));
//#endregion


//#region PositionRepository
//app.MapPost("/position/new", async (Position position) => await positionService.AddPositionAsync(position.Name));

//app.MapGet("/positions", async () => await positionService.GetPositionsAsync());
//app.MapPut("/positions/{id:int}", async (int id, string newName) => await positionService.UpdatePositionAsync(id,newName));
//app.MapDelete("/positions/{id:int}", async (int id) => await positionService.DeletePositionAsync(id));
//#endregion

//#region LeaveRepository
//app.MapPost("/leave/new", async (Leave leave) => await leaveService.AddLeaveAsync(leave));

//app.MapPut("/leaves/{leaveId:int}", async (int leaveId) => await leaveService.CancelLeaveAsync(leaveId));
//app.MapGet("/leaves/{id:int}", async (int id) => await leaveService.GetLeavesByEmployeeAsync(id));
//app.MapGet("/leaves/{start:datetime}/{end:datetime}", async (DateTime start, DateTime end) => await leaveService.GetLeavesByDateRangeAsync(start,end));
//app.MapGet("/leaves/remainder/{employeeId:int}", async (int employeeId) => await leaveService.GetLeaveBalanceAsync(employeeId));
//#endregion

//#region ReportSearchRepository
//app.MapGet("/report/employee/{employeeId:int}", async (int employeeId) => await reportSearchService.GenerateEmployeeReportAsync(employeeId));
//app.MapGet("/report/department/{departmentId:int}", async (int departmentId) => await reportSearchService.GenerateDepartmentReportAsync(departmentId));
//app.MapGet("/reports", async (DateTime start, DateTime end) => await reportSearchService.GenerateLeaveStatisticsAsync(start,end));
//app.MapGet("reports/search", async (string lastName, string firstName) => await reportSearchService.SearchEmployeesAsync(lastName,firstName ?? ""));
//#endregion

//app.Run();
#endregion


var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервисы в builder.Services
builder.Services.AddSingleton<DepartmentService>();
builder.Services.AddSingleton<EmployeeService>();
builder.Services.AddSingleton<LeaveService>();
builder.Services.AddSingleton<PositionService>();
builder.Services.AddSingleton<ReportSearchService>();
builder.Services.AddSingleton<ManagerService>();

var app = builder.Build();

// Получаем сервис через `app.Services`
var serviceManager = app.Services.GetRequiredService<ManagerService>();

//// Настройка CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});


//app.UseHttpsRedirection();
//app.UseCors("AllowAll");

//// Глобальная обработка исключений
//app.UseExceptionHandler("/error");

var Logger = LogManager.GetCurrentClassLogger()
    ;


async Task<IResult> HttpRequestAsync<T>(Func<Task<T>> managerService, string message)
{
    try
    {
        var result = await managerService();
        return Results.Ok(result);  // Возвращаем результат запроса
    }
    catch (Exception ex)
    {
        Logger.Error(ex, message);
        return Results.Problem(message);  // Возвращаем ошибку с сообщением
    }
}

#region EmployeeService

app.MapPost("/employees/new", async (Employee emp, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.EmployeeService.AddEmployeeAsync(emp),
        "Error adding employee")
);


app.MapGet("/employees", async (ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.EmployeeService.GetEmployeesAsync(),
        "Error getting employees"));


//app.MapGet("/employees", async () => await serviceManager.EmployeeService.GetEmployeesAsync());


app.MapGet("/employee/{id:int}", async (ManagerService managerService, int id) =>
    await HttpRequestAsync(
        () => managerService.EmployeeService.GetEmployeeByIdAsync(id),
        $"Error getting employee with id {id}"));


app.MapGet("/department/{id:int}/employees", async (ManagerService managerService, int id) =>
 await HttpRequestAsync(
     () => managerService.EmployeeService.GetEmployeesByDepartmentAsync(id),
     $"Error getting employees by department with id {id}"));


app.MapPut("/employees", async (ManagerService managerService, Employee emp) =>
    await HttpRequestAsync(
        () => managerService.EmployeeService.UpdateEmployeeAsync(emp),
        "Error updating employee"));


//app.MapDelete("/employee/{id:int}", async (ManagerService managerService, int id) =>
// await HttpRequestAsync(
//     () => managerService.EmployeeService.DeleteEmployeeAsync(id),
//     $"Error deleting employee with id {id}"));

#endregion

#region DepartmentService
//app.MapGet("/departments", async () => await departmentService.GetDepartmentsAsync());
//Todo Добавить реализацию метода GetDepartmentsAsync
//app.MapGet("/departments", async (ManagerService managerService) =>
//    await HttpRequestAsync(
//        () => managerService.DepartmentService.GetDepartmentsAsync(),
//        "Error getting departments"));

app.MapPost("/departments/new", async (Department department, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.DepartmentService.AddDepartmentAsync(department.Name),
        "Error adding department"));


app.MapPut("/departments/{id:int}", async (int id, string newName, ManagerService managerService) =>
    await HttpRequestAsync(() => managerService.DepartmentService.UpdateDepartmentAsync(id, newName),
        "Error updating departments"));

//app.MapDelete("/departments/{id:int}", async (int id, ManagerService managerService) =>
//    await HttpRequestAsync(
//        () => managerService.DepartmentService.DeleteDepartmentAsync(id),
//$"Error deleting department with id {id}"));

#endregion

#region PositionService

app.MapPost("/position/new", async (Position position, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.PositionService.AddPositionAsync(position.Name),
        "Error adding position")
);

app.MapGet("/positions", async (ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.PositionService.GetPositionsAsync(),
        "Error getting positions")
);

app.MapPut("/positions/{id:int}", async (int id, string newName, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.PositionService.UpdatePositionAsync(id, newName),
        $"Error updating position with id {id}")
);

app.MapDelete("/positions/{id:int}", async (int id, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.PositionService.DeletePositionAsync(id),
        $"Error deleting position with id {id}")
);

#endregion

#region LeaveService

app.MapPost("/leave/new", async (Leave leave, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.LeaveService.AddLeaveAsync(leave),
        "Error adding leave")
);

//app.MapPut("/leaves/{leaveId:int}", async (int leaveId, ManagerService managerService) =>
//    await HttpRequestAsync(
//        () => managerService.LeaveService.CancelLeaveAsync(leaveId),
//        $"Error canceling leave with id {leaveId}")
//);

app.MapGet("/leaves/{id:int}", async (int id, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.LeaveService.GetLeavesByEmployeeAsync(id),
        $"Error getting leaves for employee with id {id}")
);

app.MapGet("/leaves/{start:datetime}/{end:datetime}", async (DateTime start, DateTime end, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.LeaveService.GetLeavesByDateRangeAsync(start, end),
        $"Error getting leaves between {start} and {end}")
);

app.MapGet("/leaves/remainder/{employeeId:int}", async (int employeeId, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.LeaveService.GetLeaveBalanceAsync(employeeId),
        $"Error getting leave balance for employee with id {employeeId}")
);

#endregion

#region ReportSearchService

app.MapGet("/report/employee/{employeeId:int}", async (int employeeId, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.ReportSearchService.GenerateEmployeeReportAsync(employeeId),
        $"Error generating report for employee with id {employeeId}")
);

app.MapGet("/report/department/{departmentId:int}", async (int departmentId, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.ReportSearchService.GenerateDepartmentReportAsync(departmentId),
        $"Error generating report for department with id {departmentId}")
);

app.MapGet("/reports", async (DateTime start, DateTime end, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.ReportSearchService.GenerateLeaveStatisticsAsync(start, end),
        $"Error generating leave statistics between {start} and {end}")
);

app.MapGet("/reports/search", async (string lastName, string firstName, ManagerService managerService) =>
    await HttpRequestAsync(
        () => managerService.ReportSearchService.SearchEmployeesAsync(lastName, firstName ?? ""),
        $"Error searching employees with last name {lastName} and first name {firstName}")
);
#endregion


app.Run();

Logger.Info("Сервер запущен");