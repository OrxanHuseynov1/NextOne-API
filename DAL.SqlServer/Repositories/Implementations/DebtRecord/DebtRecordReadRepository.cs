using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class DebtRecordReadRepository : ReadRepository<DebtRecord>, IDebtRecordReadRepository
{
    public DebtRecordReadRepository(AppDbContext context) : base(context)
    {
    }
}