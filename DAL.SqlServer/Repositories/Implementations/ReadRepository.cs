using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.SqlServer.Repositories.Implementations;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<ICollection<T>> GetAllAsync(bool isTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
        {
            query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> condition, bool isTracking = true, params string[] includes)
    {
        var query = Table.Where(condition).AsQueryable();
        if (!isTracking)
        {
            query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> condition, int page, int size, bool isTracking = true, params string[] includes)
    {
        var query = Table.Where(condition).Skip(page * size).Take(size);
        if (!isTracking)
        {
            query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    public async Task<T> GetByIdAsync(Guid id, bool isTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();

        if (!isTracking)
        {
            query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        T? entity = await query.FirstOrDefaultAsync(t => t.Id == id);
        return entity!;
    }

    public async Task<T> GetOneByCondition(Expression<Func<T, bool>> condition, bool isTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
        {
            query = query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        T? entity = await query.FirstOrDefaultAsync(condition);
        return entity!;
    }

    public async Task<bool> IsExist(Guid id)
    {
        return await Table.AnyAsync(x => x.Id == id);
    }

}
