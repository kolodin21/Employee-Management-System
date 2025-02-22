using Models;
using NLog;

namespace Server.BL
{
    public class ReportSearchService: ServiceBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public async Task<IEnumerable<Report>> GenerateEmployeeReportAsync(int employeeId) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.ReportSearchRepository.GenerateEmployeeReportAsync(employeeId),
                Logger,
                $"Успешно сгенерирован отчет для сотрудника с id {employeeId}",
                $"Ошибка генерации отчета для сотрудника с id {employeeId}");

        public async Task<IEnumerable<Report>> GenerateDepartmentReportAsync(int departmentId) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.ReportSearchRepository.GenerateDepartmentReportAsync(departmentId),
                Logger,
                $"Успешно сгенерирован отчет для департамента с id {departmentId}",
                $"Ошибка генерации отчета для департамента с id {departmentId}");


        public async Task<IEnumerable<Report>> GenerateLeaveStatisticsAsync(DateTime start, DateTime end) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.ReportSearchRepository.GenerateLeaveStatisticsAsync(start, end),
                Logger,
                $"Успешно сгенерирована статистика отпусков за период с {start:yyyy-MM-dd} по {end:yyyy-MM-dd}",
                $"Ошибка генерации статистики отпусков за период с {start:yyyy-MM-dd} по {end:yyyy-MM-dd}");


        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string lastName, string firstName) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.ReportSearchRepository.SearchEmployeesAsync(lastName, firstName),
                Logger,
                $"Успешно выполнен поиск сотрудников с фамилией {lastName} и именем {firstName}",
                $"Ошибка поиска сотрудников с фамилией {lastName} и именем {firstName}");

    }
}
