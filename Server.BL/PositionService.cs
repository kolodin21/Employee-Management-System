using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Server.BL
{
    public class PositionService: ServiceBase
    {
        public async Task AddPositionAsync(string name) =>
            await repositoryManager.PositionRepository.AddPositionAsync(name);
        

        public async Task<IEnumerable<Position>> GetPositionsAsync() =>
            await repositoryManager.PositionRepository.GetPositionsAsync();
       

        public async Task UpdatePositionAsync(int id, string name) =>
            await repositoryManager.PositionRepository.UpdatePositionAsync(id, name);
        

        public async Task DeletePositionAsync(int positionId) =>
            await repositoryManager.PositionRepository.DeletePositionAsync(positionId);
        
    }
}
