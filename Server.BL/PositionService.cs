using Models;

namespace Server.BL
{
    public class PositionService: ServiceBase
    {
        public async Task<bool> AddPositionAsync(string name) =>
            await RepositoryManager.PositionRepository.AddPositionAsync(name);
        

        public async Task<IEnumerable<Position>> GetPositionsAsync() =>
            await RepositoryManager.PositionRepository.GetPositionsAsync();
       

        public async Task<bool> UpdatePositionAsync(int id, string name) =>
            await RepositoryManager.PositionRepository.UpdatePositionAsync(id, name);
        

        public async Task<bool> DeletePositionAsync(int positionId) =>
            await RepositoryManager.PositionRepository.DeletePositionAsync(positionId);
        
    }
}
