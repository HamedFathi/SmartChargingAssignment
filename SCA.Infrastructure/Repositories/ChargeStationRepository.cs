using HamedStack.TheRepository.EntityFrameworkCore;
using SCA.Domain.Entities;

namespace SCA.Infrastructure.Repositories;

public class ChargeStationRepository : Repository<ChargeStation>
{
    public ChargeStationRepository(DbContextBase dbContext, TimeProvider timeProvider) : base(dbContext, timeProvider)
    {
    }
}