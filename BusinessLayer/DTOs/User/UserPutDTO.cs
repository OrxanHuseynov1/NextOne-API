using Domain.Enums;

namespace BusinessLayer.DTOs.User;

public class UserPutDTO
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public RoleType Role { get; set; }
}
