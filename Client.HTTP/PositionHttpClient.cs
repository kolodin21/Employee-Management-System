using Models;
using NLog;
using System.Net.Http.Json;

namespace Client.HTTP
{
    public class PositionHttpClient : BaseHttpClient
    {
        // Логгер
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static string AddPositionUri() => $"{Host}/position/new";
        public static string GetPositionsUri() => $"{Host}/positions";
        public static string UpdatePositionUri(int id) => $"{Host}/positions/{id}";
        public static string DeletePositionUri(int id) => $"{Host}/positions/{id}";


        public async Task<bool> AddPositionAsync(Position position) =>
            await HttpRequestBoolAsync(async () => await Client.PostAsJsonAsync(AddPositionUri(), position), Logger, "Ошибка добавления позиции");

        public async Task<IEnumerable<Position>> GetPositionsAsync() =>
            await HttpRequestResultAsync<Position>(async () => await Client.GetAsync(GetPositionsUri()), Logger, "Ошибка получения позиций");

        public async Task<bool> UpdatePositionAsync(int id, string newName) =>
            await HttpRequestBoolAsync(async () => await Client.PutAsJsonAsync(UpdatePositionUri(id), new { Name = newName }), Logger, "Ошибка обновления позиции");

        public async Task<bool> DeletePositionAsync(int id) =>
            await HttpRequestBoolAsync(async () => await Client.DeleteAsync(DeletePositionUri(id)), Logger, "Ошибка удаления позиции");

    }
}
