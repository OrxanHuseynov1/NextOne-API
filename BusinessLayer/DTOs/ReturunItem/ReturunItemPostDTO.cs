namespace BusinessLayer.DTOs.ReturunItem;

public class ReturunItemPostDTO
{
    public decimal UnitPrice { get; set; }
    public int BoxCount { get; set; }
    public int Quantity { get; set; }
    public int OrderIndex { get; set; }
    public Guid ProductId { get; set; }
    public Guid CompanyId { get; set; }
}
