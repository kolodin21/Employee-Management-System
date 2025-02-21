using Models;
using NLog;
using System.Net.Http.Json;

namespace Client.HTTP
{
    public class PositionHttpClient : BaseHttpClient
    {
        // Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static Uri AddPositionUri() => new Uri($"{Host}/position/new");
        public static Uri GetPositionsUri() => new Uri($"{Host}/positions");
        public static Uri UpdatePositionUri(int id) => new Uri($"{Host}/positions/{id}");
        public static Uri DeletePositionUri(int id) => new Uri($"{Host}/positions/{id}");


        public async Task<bool> AddPositionAsync(Position position) =>
            await HttpRequestBoolAsync(async () => await Client.PostAsJsonAsync(AddPositionUri(), position),
                Logger,
                $"Успешно добавлена позиция {position.Name}",
                $"Ошибка добавления позиции {position.Name}");

        public async Task<IEnumerable<Position>> GetPositionsAsync() =>
            await HttpRequestResultAsync<Position>(async () => await Client.GetAsync(GetPositionsUri()),
                Logger,
                "Успешно получены позиции",
                "Ошибка получения позиций");

        public async Task<bool> UpdatePositionAsync(int id, string newName) =>
            await HttpRequestBoolAsync(async () => await Client.PutAsJsonAsync(UpdatePositionUri(id), new { Name = newName }),
                Logger,
                $"Успешно обновлена позиция с id {id} на {newName}",
                $"Ошибка обновления позиции с id {id} на {newName}");

        public async Task<bool> DeletePositionAsync(int id) =>
            await HttpRequestBoolAsync(async () => await Client.DeleteAsync(DeletePositionUri(id)),
                Logger,
                $"Успешно удалена позиция с id {id}",
                $"Ошибка удаления позиции с id {id}");

    }
}
