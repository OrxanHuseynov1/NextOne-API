using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Loss;

public class LossPostDTO
{
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? Reason { get; set; }
    public Guid ProductId { get; set; }
    public Guid DepoId { get; set; }
    public Guid CompanyId { get; set; }
}
