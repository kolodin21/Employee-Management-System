using Models;
using NLog;

namespace Server.BL
{
    public class LeaveService: ServiceBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> AddLeaveAsync(Leave leave) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.LeaveRepository.AddLeaveAsync(leave),
                Logger,
                $"Успешно добавлен отпуск для сотрудника {leave.EmployeeId}",
                "Ошибка добавления отпуска");

        //public async Task<bool> CancelLeaveAsync(int leaveId) =>
        //    await ExecuteWithLoggingAsync(
        //        () => RepositoryManager.LeaveRepository.CancelLeaveAsync(leaveId),
        //        Logger,
        //        $"Успешно отменен отпуск с id {leaveId}",
        //        $"Ошибка отмены отпуска c id {leaveId}");

        public async Task<IEnumerable<LeaveDto>> GetLeavesByEmployeeAsync(int employeeId) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.LeaveRepository.GetLeavesByEmployeeAsync(employeeId),
                Logger,
                $"Успешно получены отпуска для сотрудника с id {employeeId}",
                $"Ошибка получения отпусков сотрудника c id {employeeId}");

        public async Task<IEnumerable<LeaveDto>> GetLeavesByDateRangeAsync(DateTime start, DateTime end) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.LeaveRepository.GetLeavesByDateRangeAsync(start, end),
                Logger,
                $"Успешно получены отпуска за период с {start:yyyy-MM-dd} по {end:yyyy-MM-dd}",
                $"Ошибка получения отпусков за период c {start:yyyy-MM-dd} по {end:yyyy-MM-dd}");

        public async Task<IEnumerable<LeaveDto>> GetLeaveBalanceAsync(int employeeId) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.LeaveRepository.GetLeaveBalanceAsync(employeeId),
                Logger,
                $"Успешно получен остаток отпуска для сотрудника с id {employeeId}",
                $"Ошибка получения остатка отпуска для сотрудника с id {employeeId}");


    }
}
