using Domain.Common;

namespace Domain.Entities;

public class Chat : AuditableCompanyEntity
{
    public string SenderName { get; set; }
    public string Message { get; set; } 
}
