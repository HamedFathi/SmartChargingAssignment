using HamedStack.TheRepository.EntityFrameworkCore;
using SCA.Domain.Entities;

namespace SCA.Infrastructure.Repositories;

public class ConnectorRepository : Repository<Connector>
{
    private readonly DbContextBase _dbContext;

    public ConnectorRepository(DbContextBase dbContext, TimeProvider timeProvider) : base(dbContext, timeProvider)
    {
        _dbContext = dbContext;
    }

    public int GetMaxId()
    {
        var maxId = DbSet.Max(x => x.Id);
        return maxId;
    }
}