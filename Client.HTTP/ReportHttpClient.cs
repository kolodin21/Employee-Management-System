using Models;
using NLog;

namespace Client.HTTP
{
    public class ReportHttpClient : BaseHttpClient
    {
        // Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static Uri GenerateEmployeeReportUri(int employeeId) => new Uri($"{Host}/report/employee/{employeeId}");
        public static Uri GenerateDepartmentReportUri(int departmentId) => new Uri($"{Host}/report/department/{departmentId}");
        public static Uri GenerateLeaveStatisticsUri(DateTime start, DateTime end) => new Uri($"{Host}/reports?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}");
        public static Uri SearchEmployeesUri(string lastName, string firstName) => new Uri($"{Host}/reports/search?lastName={lastName}&firstName={firstName}");


        public async Task<IEnumerable<Report>> GenerateEmployeeReportAsync(int employeeId) =>
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateEmployeeReportUri(employeeId)),Logger);

        public async Task<IEnumerable<Report>> GenerateDepartmentReportAsync(int departmentId) =>
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateDepartmentReportUri(departmentId)),Logger);

        public async Task<IEnumerable<Report>> GenerateLeaveStatisticsAsync(DateTime start, DateTime end) =>
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateLeaveStatisticsUri(start, end)),Logger);

        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string lastName, string firstName) =>
            await HttpRequestResultAsync<EmployeeDto>(async () => await Client.GetAsync(SearchEmployeesUri(lastName, firstName)), Logger);

    }
}
