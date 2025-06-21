using Domain.Enums;

namespace BusinessLayer.DTOs.Transfer;

public class TransferGetDTO
{
    public Guid Id { get; set; }

    public Guid FromDepoId { get; set; }
    public required Domain.Entities.Depo FromDepo { get; set; }
    public Guid ToDepoId { get; set; }
    public required Domain.Entities.Depo ToDepo { get; set; }
    public Guid ProductId { get; set; }
    public required Domain.Entities.Product Product { get; set; }
    public int Quantity { get; set; }

    public Status TransferStatus { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public required string CreatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
}
