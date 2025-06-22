using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;

namespace DAL.SqlServer.Repositories.Implementations;

public class ExpenseWriteRepository : WriteRepository<Expense>, IExpenseWriteRepository
{
    public ExpenseWriteRepository(AppDbContext context) : base(context)
    {
    }
}