using Models;
using NLog;

namespace Client.HTTP
{
    public class ReportHttpClient : BaseHttpClient
    {
        // Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static string GenerateEmployeeReportUri(int employeeId) => $"{Host}/report/employee/{employeeId}";
        public static string GenerateDepartmentReportUri(int departmentId) => $"{Host}/report/department/{departmentId}";
        public static string GenerateLeaveStatisticsUri(DateTime start, DateTime end) => $"{Host}/reports?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}";
        public static string SearchEmployeesUri(string lastName, string firstName) => $"{Host}/reports/search?lastName={lastName}&firstName={firstName}";


        public async Task<IEnumerable<Report>> GenerateEmployeeReportAsync(int employeeId) =>
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateEmployeeReportUri(employeeId)), Logger, $"Ошибка генерации отчета для сотрудника с id {employeeId}");

        public async Task<IEnumerable<Report>> GenerateDepartmentReportAsync(int departmentId) =>
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateDepartmentReportUri(departmentId)), Logger, $"Ошибка генерации отчета для департамента с id {departmentId}");

        public async Task<IEnumerable<Report>> GenerateLeaveStatisticsAsync(DateTime start, DateTime end) =>
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateLeaveStatisticsUri(start, end)), Logger, $"Ошибка генерации статистики отпусков за период с {start} по {end}");

        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string lastName, string firstName) =>
            await HttpRequestResultAsync<EmployeeDto>(async () => await Client.GetAsync(SearchEmployeesUri(lastName, firstName)), Logger, $"Ошибка поиска сотрудников с фамилией {lastName} и именем {firstName}");

    }
}
