using BusinessLayer.DTOs.User;

namespace BusinessLayer.Services.Abstractions;

public interface IUserService
{
    Task CreateUserAsync(UserPostDTO UserPostDTO);
    Task DeleteUserAsync(Guid id);
    Task SoftDeleteUserAsync(Guid id);
    Task RestoreUserAsync(Guid id);
    Task UpdateUserAsync(UserPutDTO UserPutDTO);
    Task<ICollection<UserGetDTO>> GetAllSoftDeletedUser();
    Task<ICollection<UserGetDTO>> GetAllActiveUserAsync();
    Task<UserGetDTO> GetByIdUserAsync(Guid id);
}
