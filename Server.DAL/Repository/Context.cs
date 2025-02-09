namespace Server.DAL.Repository;

public class Context : DbContext
{
    private readonly string _connectionString;
    public Context(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}