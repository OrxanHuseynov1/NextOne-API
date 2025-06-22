using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ReturnReadRepository : ReadRepository<Return>, IReturnReadRepository
{
    public ReturnReadRepository(AppDbContext context) : base(context)
    {
    }
}