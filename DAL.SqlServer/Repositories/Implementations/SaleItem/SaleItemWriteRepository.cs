using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class SaleItemWriteRepository : WriteRepository<SaleItem>, ISaleItemWriteRepository
{
    public SaleItemWriteRepository(AppDbContext context) : base(context)
    {
    }
}