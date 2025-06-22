using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : AuditableCompanyEntity
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public RoleType Role { get; set; }
}
