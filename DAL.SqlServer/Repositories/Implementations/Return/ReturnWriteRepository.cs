using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ReturnWriteRepository : WriteRepository<Return>, IReturnWriteRepository
{
    public ReturnWriteRepository(AppDbContext context) : base(context)
    {
    }
}