using Domain.Enums;

namespace BusinessLayer.DTOs.Auth;

public class LoginResponseDTO
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }

    public string UserName { get; set; }
    public string FullName { get; set; }
    public RoleType Role { get; set; }
    public Guid CompanyId { get; set; }
}
