using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class LossReadRepository : ReadRepository<Loss>, ILossReadRepository
{
    public LossReadRepository(AppDbContext context) : base(context)
    {
    }
}