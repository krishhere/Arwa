using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class VendorOrderModel
{
    public Vendor Vendor { get; set; }
    public WaterCanOrders WaterCanOrder { get; set; }
}
