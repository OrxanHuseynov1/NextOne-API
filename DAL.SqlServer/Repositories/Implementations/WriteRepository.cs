using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Repositories.Implementations;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public async Task<int> SaveAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result;
    }


}
