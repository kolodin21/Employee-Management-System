using Models;
using NLog;

namespace Server.BL
{
    public class DepartmentService: ServiceBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> AddDepartmentAsync(string name) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.DepartmentRepository.AddDepartmentAsync(name),
                Logger,
                $"Успешно добавлен департамент {name}",
                "Ошибка добавления департамента");

        public async Task<bool> UpdateDepartmentAsync(int id, string name) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.DepartmentRepository.UpdateDepartmentAsync(id, name),
                Logger,
                $"Успешно обновлен департамент с id {id}",
                "Ошибка обновления департамента");

        public async Task<IEnumerable<Department>> GetDepartmentsAsync() =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.DepartmentRepository.GetAllDepartmentsAsync(),
                Logger,
                "Успешно получены департаменты",
                "Ошибка получения департаментов");

        //Todo добавить для поиска по id 

    }
}
