using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class CompanyWriteRepository : WriteRepository<Company>, ICompanyWriteRepository
{
    public CompanyWriteRepository(AppDbContext context) : base(context)
    {
    }
}