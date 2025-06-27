namespace BusinessLayer.DTOs.Category;

public class CategoryPostDTO
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
}
