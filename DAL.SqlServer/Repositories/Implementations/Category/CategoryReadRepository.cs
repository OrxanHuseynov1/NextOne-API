using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
{
    public CategoryReadRepository(AppDbContext context) : base(context)
    {
    }
}
