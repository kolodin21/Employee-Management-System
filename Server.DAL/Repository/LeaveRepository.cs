using Models;

namespace Server.DAL.Repository;

public class LeaveRepository
{
    public async Task<bool> AddLeaveAsync(Leave leave)
    {
        
    }

    public async Task<IEnumerable<LeaveDto>> GetLeavesByEmployeeAsync(int employeeId)
    {
        return null;
    }
    
    public async Task<IEnumerable<LeaveDto>> GetLeavesByDateRangeAsync(DateTime beginDate, DateTime endDate)
    {
        return null;
    }

    public async Task<IEnumerable<LeaveDto>> GetLeaveBalanceAsync(int employeeId)
    {
        return null;
    }
    
    public async Task<bool> CancelLeaveAsync(int leaveId)
    {
        
    }
    
}