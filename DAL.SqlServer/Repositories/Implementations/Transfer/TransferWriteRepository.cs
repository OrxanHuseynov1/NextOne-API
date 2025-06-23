using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class TransferWriteRepository : WriteRepository<Transfer>, ITransferWriteRepository
{
    public TransferWriteRepository(AppDbContext context) : base(context)
    {
    }
}