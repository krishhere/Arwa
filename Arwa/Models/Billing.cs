using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Billing")]
public class Billing
{
    [Key]
    public int BillingId { get; set; }

    public int VendorId { get; set; }
    public int WaterCanId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal AmountBilled { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal AmountPaid { get; set; }

    public DateTime BillingDate { get; set; }
}
