using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class LossWriteRepository : WriteRepository<Loss>, ILossWriteRepository
{
    public LossWriteRepository(AppDbContext context) : base(context)
    {
    }
}