using BusinessLayer.DTOs.User;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace BusinessLayer.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IMapper _mapper;

    public UserService(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper)
    {
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateUserAsync(UserPostDTO UserPostDTO)
    {
        User user = _mapper.Map<User>(UserPostDTO);

        await _userWriteRepository.CreateAsync(user);
        var result = await _userWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to create user.");
        }
    }

    public async Task DeleteUserAsync(Guid id)
    {
        if (!await _userReadRepository.IsExist(id)) throw new Exception("User not found.");
        User user = await _userReadRepository.GetByIdAsync(id) ?? throw new Exception("User not found.");

        _userWriteRepository.Delete(user);

        var result = await _userWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to delete user.");
        }
    }

    public async Task SoftDeleteUserAsync(Guid id)
    {
        if (!await _userReadRepository.IsExist(id)) throw new Exception("User not found.");
        User user = await _userReadRepository.GetOneByCondition(u => u.Id == id && !u.IsDeleted, false)
                             ?? throw new Exception("User not found.");
        user.IsDeleted = true;
        _userWriteRepository.Update(user);

        var result = await _userWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to soft delete user.");
        }
    }

    public async Task RestoreUserAsync(Guid id)
    {
        if (!await _userReadRepository.IsExist(id)) throw new Exception("User not found.");
        User user = await _userReadRepository.GetOneByCondition(u => u.Id == id && u.IsDeleted, false)
                             ?? throw new Exception("User not found.");
        user.IsDeleted = false;
        _userWriteRepository.Update(user);

        var result = await _userWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to restore user.");
        }
    }

    public async Task UpdateUserAsync(UserPutDTO UserPutDTO)
    {
        if (!await _userReadRepository.IsExist(UserPutDTO.Id)) throw new Exception("User not found.");

        User userToUpdate = await _userReadRepository.GetByIdAsync(UserPutDTO.Id)
                                      ?? throw new Exception("User not found.");

        _mapper.Map(UserPutDTO, userToUpdate);
        _userWriteRepository.Update(userToUpdate);

        var result = await _userWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to update user.");
        }
    }

    public async Task<ICollection<UserGetDTO>> GetAllSoftDeletedUser()
    {
        ICollection<User> users = await _userReadRepository.GetAllByCondition(u => u.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<UserGetDTO>>(users);
    }

    public async Task<ICollection<UserGetDTO>> GetAllActiveUserAsync()
    {
        ICollection<User> users = await _userReadRepository.GetAllByCondition(u => !u.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<UserGetDTO>>(users);
    }

    public async Task<UserGetDTO> GetByIdUserAsync(Guid id)
    {
        if (!await _userReadRepository.IsExist(id)) throw new Exception("User not found.");
        User user = await _userReadRepository.GetByIdAsync(id) ?? throw new Exception("User not found.");
        return _mapper.Map<UserGetDTO>(user);
    }
}