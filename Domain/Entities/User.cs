using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : AuditableEntity
{
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public RoleType Role { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = default!;
}
