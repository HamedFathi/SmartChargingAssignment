using HamedStack.TheRepository.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCA.Domain.Entities;

namespace SCA.Infrastructure.Repositories;

public class GroupRepository : Repository<Group>
{
    public GroupRepository(DbContextBase dbContext, TimeProvider timeProvider) : base(dbContext, timeProvider)
    {
    }

    public override ValueTask<Group?> GetByIdsAsync(object[] ids,
        CancellationToken cancellationToken = new())
    {
        var output = DbSet.Include(np => np.ChargeStations).ThenInclude(cs => cs.Connectors)
            .SingleOrDefaultAsync(g => g.Id == Guid.Parse(ids[0].ToString()!),
                cancellationToken: cancellationToken);
        return new ValueTask<Group?>(output);
    }

    public override ValueTask<Group?> GetByIdAsync<TKey>(TKey id,
        CancellationToken cancellationToken = new())
    {
        return GetByIdsAsync(new object[] { id }, cancellationToken);
    }

    public override Task<List<Group>> ToListAsync(CancellationToken cancellationToken = new())
    {
        return DbSet.Include(np => np.ChargeStations).ThenInclude(cs => cs.Connectors)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}