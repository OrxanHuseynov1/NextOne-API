using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Transfer : AuditableCompanyEntity
{
    public Guid FromDepoId { get; set; }
    public Depo FromDepo { get; set; }
    public Guid ToDepoId { get; set; }
    public Depo ToDepo { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Status TransferStatus { get; set; } = Status.Pending;
    public int Quantity { get; set; }
    public string? ApprovedByUserName { get; set; }
    public DateTime? ApprovedAt { get; set; }
}
