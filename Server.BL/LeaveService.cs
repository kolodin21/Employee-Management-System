using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Server.BL
{
    public class LeaveService: ServiceBase
    {
        public async Task AddLeaveAsync(Leave leave) =>
            await repositoryManager.LeaveRepository.AddLeaveAsync(leave);
        

        public async Task<IEnumerable<LeaveDto>> GetLeavesByEmployeeAsync(int employeeId) =>
            await repositoryManager.LeaveRepository.GetLeavesByEmployeeAsync(employeeId);
        

        public async Task<IEnumerable<LeaveDto>> GetLeavesByDateRangeAsync(DateTime beginDate, DateTime endDate) =>
            await repositoryManager.LeaveRepository.GetLeavesByDateRangeAsync(beginDate, endDate);
        

        public async Task<IEnumerable<LeaveDto>> GetLeaveBalanceAsync(int employeeId) =>
            await repositoryManager.LeaveRepository.GetLeaveBalanceAsync(employeeId);
        

        public async Task CancelLeaveAsync(int leaveId) =>
            await repositoryManager.LeaveRepository.CancelLeaveAsync(leaveId);
        
    }
}
