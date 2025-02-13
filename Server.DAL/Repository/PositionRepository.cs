using Models;

namespace Server.DAL.Repository;

public class PositionRepository
{
    public async Task<bool> AddPositionAsync(string name)
    {
        
    }

    public async Task<IEnumerable<Position>> GetPositionsAsync()
    {
        return null;
    }
    
    public async Task<bool> UpdatePositionAsync(int id, string name)
    {
        
    }
    
    public async Task<bool> DeletePositionAsync(int positionId)
    {
        
    }
}