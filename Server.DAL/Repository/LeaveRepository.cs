namespace Server.DAL.Repository;

public class LeaveRepository
{
    public async Task AddLeaveAsync(Leave leave)
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
    
    public async Task CancelLeaveAsync(int leaveId)
    {
        
    }
    
}