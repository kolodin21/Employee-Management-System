using Models;
using NLog;

namespace Server.BL
{
    public class PositionService: ServiceBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> AddPositionAsync(Position position) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.PositionRepository.AddPositionAsync(position.Name),
                Logger,
                $"Успешно добавлена должность {position.Name}",
                $"Ошибка добавления должности {position.Name}");

        public async Task<IEnumerable<Position>> GetPositionsAsync() =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.PositionRepository.GetPositionsAsync(),
                Logger,
                "Успешно получены должности",
                "Ошибка получения должностей",
                CacheKey.AllPositions);

        public async Task<bool> UpdatePositionAsync(int id, string newName) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.PositionRepository.UpdatePositionAsync(id, newName),
                Logger,
                $"Успешно обновлена должность с id {id} на {newName}",
                $"Ошибка обновления должности с id {id} на {newName}");

        public async Task<bool> DeletePositionAsync(int id) =>
            await ExecuteWithLoggingAsync(
                () => RepositoryManager.PositionRepository.DeletePositionAsync(id),
                Logger,
                $"Успешно удалена должность с id {id}",
                $"Ошибка удаления должности с id {id}");

    }
}
