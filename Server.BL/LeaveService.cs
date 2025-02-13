using Models;

namespace Server.BL
{
    public class LeaveService: ServiceBase
    {
        public async Task<bool> AddLeaveAsync(Leave leave) =>
            await repositoryManager.LeaveRepository.AddLeaveAsync(leave);
        

        public async Task<IEnumerable<LeaveDto>> GetLeavesByEmployeeAsync(int employeeId) =>
            await repositoryManager.LeaveRepository.GetLeavesByEmployeeAsync(employeeId);
        

        public async Task<IEnumerable<LeaveDto>> GetLeavesByDateRangeAsync(DateTime beginDate, DateTime endDate) =>
            await repositoryManager.LeaveRepository.GetLeavesByDateRangeAsync(beginDate, endDate);
        

        public async Task<IEnumerable<LeaveDto>> GetLeaveBalanceAsync(int employeeId) =>
            await repositoryManager.LeaveRepository.GetLeaveBalanceAsync(employeeId);
        

        public async Task<bool> CancelLeaveAsync(int leaveId) =>
            await repositoryManager.LeaveRepository.CancelLeaveAsync(leaveId);
        
    }
}
