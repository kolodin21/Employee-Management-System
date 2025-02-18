using Models;
using NLog;
using System.Net.Http.Json;

namespace Client.HTTP
{
    public class LeaveHttpClient : BaseHttpClient
    {

        // Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static Uri AddLeaveUri() => new Uri($"{Host}/leave/new");
        public static Uri CancelLeaveUri(int leaveId) => new Uri($"{Host}/leaves/{leaveId}");
        public static Uri GetLeavesByEmployeeUri(int id) => new Uri($"{Host}/leaves/{id}");
        public static Uri GetLeavesByDateRangeUri(DateTime start, DateTime end) => new Uri($"{Host}/leaves/{start:yyyy-MM-dd}/{end:yyyy-MM-dd}");
        public static Uri GetLeaveBalanceUri(int employeeId) => new Uri($"{Host}/leaves/remainder/{employeeId}");


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
