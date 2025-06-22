using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class CompanyReadRepository : ReadRepository<Company>, ICompanyReadRepository
{
    public CompanyReadRepository(AppDbContext context) : base(context)
    {
    }
}