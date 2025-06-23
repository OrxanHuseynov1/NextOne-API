using BusinessLayer.DTOs.Returun;
using BusinessLayer.Services.Abstractions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DAL.SqlServer.Context;
using Domain.Entities;

public class ReturnService(AppDbContext context, IMapper mapper) : IReturnService
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task CreateReturnAsync(ReturunPostDTO ReturnPostDTO)
    {
        var returun = _mapper.Map<Return>(ReturnPostDTO);
        returun.CreatedAt = DateTime.UtcNow;
        returun.IsDeleted = false;

        await _context.Returns.AddAsync(returun);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReturnAsync(Guid id)
    {
        var returun = await _context.Returns.FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new Exception("Return operation not found.");

        _context.Returns.Remove(returun);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteReturnAsync(Guid id)
    {
        var returun = await _context.Returns.FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new Exception("Return operation not found.");

        returun.IsDeleted = true;
        returun.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task RestoreReturnAsync(Guid id)
    {
        var returun = await _context.Returns.FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new Exception("Return operation not found.");

        returun.IsDeleted = false;
        returun.DeletedAt = null;

        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<ReturunGetDTO>> GetAllSoftDeletedReturn()
    {
        var returns = await _context.Returns
            .Where(r => r.IsDeleted)
            .ToListAsync();
        return _mapper.Map<ICollection<ReturunGetDTO>>(returns);
    }

    public async Task<ICollection<ReturunGetDTO>> GetAllActiveReturnAsync()
    {
        var returns = await _context.Returns
            .Where(r => !r.IsDeleted)
            .ToListAsync();
        return _mapper.Map<ICollection<ReturunGetDTO>>(returns);
    }

    public async Task<ReturunGetDTO> GetByIdReturnAsync(Guid id)
    {
        var returun = await _context.Returns.FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new Exception("Return operation not found.");
        return _mapper.Map<ReturunGetDTO>(returun);
    }
}