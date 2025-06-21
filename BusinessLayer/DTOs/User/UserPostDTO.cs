using Domain.Enums;

namespace BusinessLayer.DTOs.User;

public class UserPostDTO
{
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public RoleType Role { get; set; }
    public Guid CompanyId { get; set; }
}
