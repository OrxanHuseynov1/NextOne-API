using AutoMapper;
using BusinessLayer.DTOs.Expense;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseReadRepository _expenseReadRepository;
    private readonly IExpenseWriteRepository _expenseWriteRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseReadRepository expenseReadRepository, IExpenseWriteRepository expenseWriteRepository, IMapper mapper)
    {
        _expenseReadRepository = expenseReadRepository;
        _expenseWriteRepository = expenseWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateExpenseAsync(ExpensePostDTO ExpensePostDTO)
    {
        Expense expense = _mapper.Map<Expense>(ExpensePostDTO);

        await _expenseWriteRepository.CreateAsync(expense);
        var result = await _expenseWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Expense could not be created.");
        }
    }

    public async Task DeleteExpenseAsync(Guid id)
    {
        if (!await _expenseReadRepository.IsExist(id)) throw new Exception("Expense not found.");
        Expense expense = await _expenseReadRepository.GetByIdAsync(id) ?? throw new Exception("Expense not found.");

        _expenseWriteRepository.Delete(expense);

        var result = await _expenseWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Expense could not be deleted.");
        }
    }

    public async Task SoftDeleteExpenseAsync(Guid id)
    {
        if (!await _expenseReadRepository.IsExist(id)) throw new Exception("Expense not found.");
        Expense expense = await _expenseReadRepository.GetOneByCondition(c => c.Id == id && !c.IsDeleted, false)
                            ?? throw new Exception("Expense not found.");
        expense.IsDeleted = true;
        _expenseWriteRepository.Update(expense);

        var result = await _expenseWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Expense could not be soft deleted.");
        }
    }

    public async Task RestoreExpenseAsync(Guid id)
    {
        if (!await _expenseReadRepository.IsExist(id)) throw new Exception("Expense not found.");
        Expense expense = await _expenseReadRepository.GetOneByCondition(c => c.Id == id && c.IsDeleted, false)
                            ?? throw new Exception("Expense not found.");
        expense.IsDeleted = false;
        _expenseWriteRepository.Update(expense);

        var result = await _expenseWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Expense could not be restored.");
        }
    }

    public async Task<ICollection<ExpenseGetDTO>> GetAllSoftDeletedExpense()
    {
        ICollection<Expense> expenses = await _expenseReadRepository.GetAllByCondition(c => c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ExpenseGetDTO>>(expenses);
    }

    public async Task<ICollection<ExpenseGetDTO>> GetAllActiveExpenseAsync()
    {
        ICollection<Expense> expenses = await _expenseReadRepository.GetAllByCondition(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ExpenseGetDTO>>(expenses);
    }

    public async Task<ExpenseGetDTO> GetByIdExpenseAsync(Guid id)
    {
        if (!await _expenseReadRepository.IsExist(id)) throw new Exception("Expense not found.");
        Expense expense = await _expenseReadRepository.GetByIdAsync(id) ?? throw new Exception("Expense not found.");
        return _mapper.Map<ExpenseGetDTO>(expense);
    }
}