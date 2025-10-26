public class ProdManager
{
    public int ProdManagerId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
    public DateTime? CreatedAt { get; set; }

    public ICollection<WaterCan> WaterCans { get; set; }
}