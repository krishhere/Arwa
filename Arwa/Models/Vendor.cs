using System.ComponentModel.DataAnnotations;

public class Vendor
{
    [Key]
    public int VendorId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(250)]
    public string Location { get; set; }

    [Required]
    [StringLength(10)]
    public string Phone { get; set; }

    [StringLength(100)]
    public string Email { get; set; }
}