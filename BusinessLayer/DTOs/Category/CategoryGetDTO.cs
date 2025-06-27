
namespace BusinessLayer.DTOs.Category;

public class CategoryGetDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Domain.Entities.Product> Products { get; set; } = [];
    public bool IsDeleted { get; set; } = false;
    public Guid CompanyId { get; set; }
    public Domain.Entities.Company Company { get; set; }    
}
