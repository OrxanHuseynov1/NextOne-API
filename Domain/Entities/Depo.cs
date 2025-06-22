using Domain.Common;

namespace Domain.Entities;

public class Depo : AuditableCompanyEntity
{
    public string Name { get; set; }
    public ICollection<ProductInDepo> ProductInDepos { get; set; } = [];    
}
