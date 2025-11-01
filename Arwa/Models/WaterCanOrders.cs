using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("WaterCanOrders")]
public class WaterCanOrders
{
    [Key]
    public int WaterCanOrderId { get; set; }

    public int ProdManagerId { get; set; }
    [ForeignKey("Vendor")]
    public int VendorId { get; set; }
    public int SalesPersonId { get; set; }
    public int Cases025Ltr { get; set; } = 0;
    public int Cases05Ltr { get; set; } = 0;
    public int Cases1Ltr { get; set; } = 0;
    public int Cases2Ltr { get; set; } = 0;
    public int Cases5Ltr { get; set; } = 0;
    public int Cases20Ltr { get; set; } = 0;
    [Column(TypeName = "decimal(10,2)")]
    public decimal AmountBilled { get; set; } = 0.00m;
    [Column(TypeName = "decimal(10,2)")]
    public decimal AmountPaid { get; set; } = 0.00m;
    public DateTime BillingDate { get; set; }
    public int OrderStatus { get; set; }
}