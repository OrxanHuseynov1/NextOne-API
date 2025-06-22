using Domain.Entities;

namespace Domain.Common;

public abstract class AuditableCompanyEntity : AuditableEntity
{
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
}
