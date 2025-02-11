using Models;

namespace Server.DAL.Repository;

public class ReportSearchRepository
{
    
    public async Task<IEnumerable<Report>> GenerateEmployeeReportAsync(int employeeId)
    {
        return null;
    }
    
    public async Task<IEnumerable<Report>> GenerateDepartmentReportAsync(int departmentId)
    {
        return null;
    }
    
    public async Task<EmployeeDto> GenerateLeaveStatisticsAsync(DateTime start, DateTime end)
    {
        return null;
    }

    public async Task<EmployeeDto> SearchEmployeesAsync(string lastName, string firstName)
    {
        return null;
    }
    
}