using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class TransferReadRepository : ReadRepository<Transfer>, ITransferReadRepository
{
    public TransferReadRepository(AppDbContext context) : base(context)
    {
    }
}