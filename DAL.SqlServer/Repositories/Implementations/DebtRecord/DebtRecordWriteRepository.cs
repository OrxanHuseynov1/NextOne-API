using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class DebtRecordWriteRepository : WriteRepository<DebtRecord>, IDebtRecordWriteRepository
{
    public DebtRecordWriteRepository(AppDbContext context) : base(context)
    {
    }
}