using BusinessLayer.DTOs.Expense;

namespace BusinessLayer.Services.Abstractions;

public interface IExpenseService
{
    Task CreateExpenseAsync(ExpensePostDTO ExpensePostDTO);
    Task DeleteExpenseAsync(Guid id);
    Task SoftDeleteExpenseAsync(Guid id);
    Task RestoreExpenseAsync(Guid id);
    Task<ICollection<ExpenseGetDTO>> GetAllSoftDeletedExpense();
    Task<ICollection<ExpenseGetDTO>> GetAllActiveExpenseAsync();
    Task<ExpenseGetDTO> GetByIdExpenseAsync(Guid id);
}
