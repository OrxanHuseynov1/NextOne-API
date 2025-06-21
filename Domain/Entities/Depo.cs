using Domain.Common;

namespace Domain.Entities;

public class Depo : AuditableCompanyEntity
{
    public required string Name { get; set; }
    public ICollection<ProductInDepo> ProductInDepos { get; set; } = [];    
}
