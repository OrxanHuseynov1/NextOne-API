using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Transfer : AuditableCompanyEntity
{
    public Guid FromDepoId { get; set; }
    public required Depo FromDepo { get; set; }
    public Guid ToDepoId { get; set; }
    public required Depo ToDepo { get; set; }
    public Guid ProductId { get; set; }
    public required Product Product { get; set; }
    public Status TransferStatus { get; set; } = Status.Pending;
    public int Quantity { get; set; }
    public string? ApprovedByUserName { get; set; }
    public DateTime? ApprovedAt { get; set; }
}
