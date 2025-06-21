namespace BusinessLayer.DTOs.ReturunItem;

public class ReturunItemPostDTO
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Guid CompanyId { get; set; }
}
