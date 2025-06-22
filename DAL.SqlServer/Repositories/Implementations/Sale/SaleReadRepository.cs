using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class SaleReadRepository : ReadRepository<Sale>, ISaleReadRepository
{
    public SaleReadRepository(AppDbContext context) : base(context)
    {
    }
}