using Models;

namespace Server.BL
{
    public class PositionService: ServiceBase
    {
        public async Task<bool> AddPositionAsync(string name) =>
            await repositoryManager.PositionRepository.AddPositionAsync(name);
        

        public async Task<IEnumerable<Position>> GetPositionsAsync() =>
            await repositoryManager.PositionRepository.GetPositionsAsync();
       

        public async Task<bool> UpdatePositionAsync(int id, string name) =>
            await repositoryManager.PositionRepository.UpdatePositionAsync(id, name);
        

        public async Task<bool> DeletePositionAsync(int positionId) =>
            await repositoryManager.PositionRepository.DeletePositionAsync(positionId);
        
    }
}
