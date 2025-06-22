using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ReturnItemReadRepository : ReadRepository<ReturnItem>, IReturnItemReadRepository
{
    public ReturnItemReadRepository(AppDbContext context) : base(context)
    {
    }
}