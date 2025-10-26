public class WaterCan
{
    public int WaterCanId { get; set; }
    public int ProdManagerId { get; set; }
    public int VendorId { get; set; }

    public int Cases025Ltr { get; set; }
    public int Cases05Ltr { get; set; }
    public int Cases1Ltr { get; set; }
    public int Cases2Ltr { get; set; }
    public int Cases5Ltr { get; set; }
    public int Cases20Ltr { get; set; }
    public DateTime CreatedAt { get; set; }

    public ProdManager ProdManager { get; set; }
    public Vendor Vendor { get; set; }
    public ICollection<Billing> Billings { get; set; }
}