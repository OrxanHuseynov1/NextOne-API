using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ChatReadRepository : ReadRepository<Chat>, IChatReadRepository
{
    public ChatReadRepository(AppDbContext context) : base(context)
    {
    }
}