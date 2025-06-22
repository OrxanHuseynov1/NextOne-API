using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ProductInDepoReadRepository : ReadRepository<ProductInDepo>, IProductInDepoReadRepository
{
    public ProductInDepoReadRepository(AppDbContext context) : base(context)
    {
    }
}