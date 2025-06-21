using BusinessLayer.DTOs.ReturunItem;
using Domain.Enums;

namespace BusinessLayer.DTOs.Returun;

public class ReturunPostDTO
{
    public Guid CustomerId { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal? DebtReduction { get; set; }
    public ICollection<ReturunItemPostDTO> ReturnItems { get; set; } = [];
    public Guid CompanyId { get; set; }
}
