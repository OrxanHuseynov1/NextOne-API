namespace BusinessLayer.DTOs.DebtRecord;

public class DebtRecordPostDTO
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; } 
    public Guid CompanyId { get; set; }
    public Guid? CustomerId { get; set; }
}
