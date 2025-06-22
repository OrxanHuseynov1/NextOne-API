using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Repositories.Abstractions;
public interface IRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Table { get; }
}
