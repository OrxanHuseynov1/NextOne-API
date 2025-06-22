using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class SaleItemReadRepository : ReadRepository<SaleItem>, ISaleItemReadRepository
{
    public SaleItemReadRepository(AppDbContext context) : base(context)
    {
    }
}