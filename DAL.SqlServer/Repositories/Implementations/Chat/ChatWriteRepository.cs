using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ChatWriteRepository : WriteRepository<Chat>, IChatWriteRepository
{
    public ChatWriteRepository(AppDbContext context) : base(context)
    {
    }
}