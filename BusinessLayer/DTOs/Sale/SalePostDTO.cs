using BusinessLayer.DTOs.SaleItem;
using Domain.Enums;

namespace BusinessLayer.DTOs.Sale;

internal class SalePostDTO
{
    public Guid CustomerId { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal? TotalDiscount { get; set; }
    public decimal? DebtLeft { get; set; }
    public Guid CompanyId { get; set; }
    public Status SaleStatus { get; set; } = Status.Approved;
    public ICollection<SaleItemPostDTO> SaleItems { get; set; } = [];
    public string? CreatedBy { get; set; }

}
