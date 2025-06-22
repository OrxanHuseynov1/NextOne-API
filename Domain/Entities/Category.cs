using Domain.Common;

namespace Domain.Entities;

public class Category : AuditableCompanyEntity
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public ICollection<Product>? Products { get; set; } = [];
}
