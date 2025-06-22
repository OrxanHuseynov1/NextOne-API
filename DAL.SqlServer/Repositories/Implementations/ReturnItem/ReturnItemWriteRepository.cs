using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ReturnItemWriteRepository : WriteRepository<ReturnItem>, IReturnItemWriteRepository
{
    public ReturnItemWriteRepository(AppDbContext context) : base(context)
    {
    }
}