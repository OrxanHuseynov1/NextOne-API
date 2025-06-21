namespace BusinessLayer.DTOs.Transfer;

public class TransferPostDTO
{
    public Guid FromDepoId { get; set; }
    public Guid ToDepoId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid CompanyId { get; set; }

}
