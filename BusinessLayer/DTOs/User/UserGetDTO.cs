using Domain.Enums;
namespace BusinessLayer.DTOs.User;

public class UserGetDTO
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public RoleType Role { get; set; }
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public bool IsDeleted { get; set; } = false;
}
