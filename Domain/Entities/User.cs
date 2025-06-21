using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : AuditableCompanyEntity
{
    public required string FullName { get; set; }
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public RoleType Role { get; set; }
}
