using Models;

namespace Server.BL
{
    public class LeaveService: ServiceBase
    {
        public async Task<bool> AddLeaveAsync(Leave leave) =>
            await RepositoryManager.LeaveRepository.AddLeaveAsync(leave);
        

        public async Task<IEnumerable<LeaveDto>> GetLeavesByEmployeeAsync(int employeeId) =>
            await RepositoryManager.LeaveRepository.GetLeavesByEmployeeAsync(employeeId);
        

        public async Task<IEnumerable<LeaveDto>> GetLeavesByDateRangeAsync(DateTime beginDate, DateTime endDate) =>
            await RepositoryManager.LeaveRepository.GetLeavesByDateRangeAsync(beginDate, endDate);
        

        public async Task<IEnumerable<LeaveDto>> GetLeaveBalanceAsync(int employeeId) =>
            await RepositoryManager.LeaveRepository.GetLeaveBalanceAsync(employeeId);
        

        //public async Task<bool> CancelLeaveAsync(int leaveId) =>
        //    await RepositoryManager.LeaveRepository.CancelLeaveAsync(leaveId);
        
    }
}
