using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ProductInDepoWriteRepository : WriteRepository<ProductInDepo>, IProductInDepoWriteRepository
{
    public ProductInDepoWriteRepository(AppDbContext context) : base(context)
    {
    }
}