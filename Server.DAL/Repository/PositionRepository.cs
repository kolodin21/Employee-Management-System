using Models;

namespace Server.DAL.Repository;

public class PositionRepository
{
    public async Task<bool> AddPositionAsync(string name)
    {
        return false;
    }

    public async Task<IEnumerable<Position>> GetPositionsAsync()
    {
        return null;
    }
    
    public async Task<bool> UpdatePositionAsync(int id, string name)
    {
        return false;
    }
    
    public async Task<bool> DeletePositionAsync(int positionId)
    {
        return false;
    }
}