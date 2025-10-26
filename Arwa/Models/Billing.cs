public class Billing
{
    public int BillingId { get; set; }
    public int WaterCanId { get; set; }
    public decimal AmountBilled { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal PriceQuotedToVendor { get; set; }
    public DateTime? BillingDate { get; set; }
    public DateTime CreatedAt { get; set; }

    public WaterCan WaterCan { get; set; }
}