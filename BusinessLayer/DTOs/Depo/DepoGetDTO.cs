using Domain.Entities;

namespace BusinessLayer.DTOs.Depo;

public class DepoGetDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Domain.Entities.ProductInDepo> ProductInDepos { get; set; } = [];
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public bool IsDeleted { get; set; } = false;
}
