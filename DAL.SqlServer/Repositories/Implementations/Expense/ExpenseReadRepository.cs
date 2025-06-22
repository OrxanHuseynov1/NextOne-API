using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ExpenseReadRepository : ReadRepository<Expense>, IExpenseReadRepository
{
    public ExpenseReadRepository(AppDbContext context) : base(context)
    {
    }
}