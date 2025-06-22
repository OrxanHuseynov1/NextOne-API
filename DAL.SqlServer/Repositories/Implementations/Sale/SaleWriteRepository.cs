using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class SaleWriteRepository : WriteRepository<Sale>, ISaleWriteRepository
{
    public SaleWriteRepository(AppDbContext context) : base(context)
    {
    }
}