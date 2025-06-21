using Domain.Common;

namespace Domain.Entities;

public class Chat : AuditableCompanyEntity
{
    public required string SenderName { get; set; }
    public required string Message { get; set; } 
}
