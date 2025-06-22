using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class DepoWriteRepository : WriteRepository<Depo>, IDepoWriteRepository
{
    public DepoWriteRepository(AppDbContext context) : base(context)
    {
    }
}