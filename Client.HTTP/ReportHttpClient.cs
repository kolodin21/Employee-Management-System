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
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateEmployeeReportUri(employeeId)),
                Logger,
                $"Успешно сгенерирован отчет для сотрудника с id {employeeId}",
                $"Ошибка генерации отчета для сотрудника с id {employeeId}");

        public async Task<IEnumerable<Report>> GenerateDepartmentReportAsync(int departmentId) =>
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateDepartmentReportUri(departmentId)),
                Logger,
                $"Успешно сгенерирован отчет для департамента с id {departmentId}",
                $"Ошибка генерации отчета для департамента с id {departmentId}");

        public async Task<IEnumerable<Report>> GenerateLeaveStatisticsAsync(DateTime start, DateTime end) =>
            await HttpRequestResultAsync<Report>(async () => await Client.GetAsync(GenerateLeaveStatisticsUri(start, end)),
                Logger,
                $"Успешно сгенерирована статистика отпусков за период с {start:yyyy-MM-dd} по {end:yyyy-MM-dd}",
                $"Ошибка генерации статистики отпусков за период с {start:yyyy-MM-dd} по {end:yyyy-MM-dd}");

        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string lastName, string firstName) =>
            await HttpRequestResultAsync<EmployeeDto>(async () => await Client.GetAsync(SearchEmployeesUri(lastName, firstName)),
                Logger,
                $"Успешно выполнен поиск сотрудников с фамилией {lastName} и именем {firstName}",
                $"Ошибка поиска сотрудников с фамилией {lastName} и именем {firstName}");

    }
}
