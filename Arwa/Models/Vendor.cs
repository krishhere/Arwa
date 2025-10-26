public class Vendor
{
    public int VendorId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime? CreatedAt { get; set; }

    public ICollection<WaterCan> WaterCans { get; set; }
}