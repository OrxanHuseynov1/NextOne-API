using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class UserReadRepository : ReadRepository<User>, IUserReadRepository
{
    public UserReadRepository(AppDbContext context) : base(context)
    {
    }
}