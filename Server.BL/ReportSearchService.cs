using Models;

namespace Server.BL
{
    public class ReportSearchService: ServiceBase
    {
        public async Task<IEnumerable<Report>> GenerateEmployeeReportAsync(int employeeId) =>
            await RepositoryManager.ReportSearchRepository.GenerateEmployeeReportAsync(employeeId);

        public async Task<IEnumerable<Report>> GenerateDepartmentReportAsync(int departmentId) =>
            await RepositoryManager.ReportSearchRepository.GenerateDepartmentReportAsync(departmentId);
        

        public async Task<EmployeeDto> GenerateLeaveStatisticsAsync(DateTime start, DateTime end) =>
            await RepositoryManager.ReportSearchRepository.GenerateLeaveStatisticsAsync(start, end);
       

        public async Task<EmployeeDto> SearchEmployeesAsync(string lastName, string firstName) =>
            await RepositoryManager.ReportSearchRepository.SearchEmployeesAsync(lastName, firstName);
       
    }
}
