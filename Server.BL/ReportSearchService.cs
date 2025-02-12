using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Server.BL
{
    public class ReportSearchService: ServiceBase
    {
        public async Task<IEnumerable<Report>> GenerateEmployeeReportAsync(int employeeId) =>
            await repositoryManager.ReportSearchRepository.GenerateEmployeeReportAsync(employeeId);

        public async Task<IEnumerable<Report>> GenerateDepartmentReportAsync(int departmentId) =>
            await repositoryManager.ReportSearchRepository.GenerateDepartmentReportAsync(departmentId);
        

        public async Task<EmployeeDto> GenerateLeaveStatisticsAsync(DateTime start, DateTime end) =>
            await repositoryManager.ReportSearchRepository.GenerateLeaveStatisticsAsync(start, end);
       

        public async Task<EmployeeDto> SearchEmployeesAsync(string lastName, string firstName) =>
            await repositoryManager.ReportSearchRepository.SearchEmployeesAsync(lastName, firstName);
       
    }
}
