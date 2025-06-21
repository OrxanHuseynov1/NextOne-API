using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<DebtRecord> DebtRecords { get; set; }
    public DbSet<Depo> Depos { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Loss> Losses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductInDepo> ProductInDepos { get; set; }
    public DbSet<Return> Returns { get; set; }
    public DbSet<ReturnItem> ReturnItems { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
