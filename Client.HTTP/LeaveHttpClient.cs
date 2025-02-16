using Models;
using NLog;
using System.Net.Http.Json;

namespace Client.HTTP
{
    public class LeaveHttpClient : BaseHttpClient
    {

        // Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static string AddLeaveUri() => $"{Host}/leave/new";
        public static string CancelLeaveUri(int leaveId) => $"{Host}/leaves/{leaveId}";
        public static string GetLeavesByEmployeeUri(int id) => $"{Host}/leaves/{id}";
        public static string GetLeavesByDateRangeUri(DateTime start, DateTime end) => $"{Host}/leaves/{start:yyyy-MM-dd}/{end:yyyy-MM-dd}";
        public static string GetLeaveBalanceUri(int employeeId) => $"{Host}/leaves/remainder/{employeeId}";


        public async Task<bool> AddLeaveAsync(Leave leave) =>
            await HttpRequestBoolAsync(async () => await Client.PostAsJsonAsync(AddLeaveUri(), leave), Logger, "Ошибка добавления отпуска");

        public async Task<bool> CancelLeaveAsync(int leaveId) =>
            await HttpRequestBoolAsync(async () => await Client.PutAsync(CancelLeaveUri(leaveId), null), Logger, "Ошибка отмены отпуска");

        public async Task<IEnumerable<Leave>> GetLeavesByEmployeeAsync(int id) =>
            await HttpRequestResultAsync<Leave>(async () => await Client.GetAsync(GetLeavesByEmployeeUri(id)), Logger, "Ошибка получения отпусков сотрудника");

        public async Task<IEnumerable<Leave>> GetLeavesByDateRangeAsync(DateTime start, DateTime end) =>
            await HttpRequestResultAsync<Leave>(async () => await Client.GetAsync(GetLeavesByDateRangeUri(start, end)), Logger, "Ошибка получения отпусков за период");

        public async Task<IEnumerable<Leave>> GetLeaveBalanceAsync(int employeeId) =>
            await HttpRequestResultAsync<Leave>(async () => await Client.GetAsync(GetLeaveBalanceUri(employeeId)), Logger, "Ошибка получения остатка отпуска");

    }
}
