using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class DepoReadRepository : ReadRepository<Depo>, IDepoReadRepository
{
    public DepoReadRepository(AppDbContext context) : base(context)
    {
    }
}