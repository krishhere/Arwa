using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Arwa.Models;

namespace Arwa.Controllers;

public class SalesController : Controller
{
    private readonly DbData _dbData;
    public SalesController(DbData dbData)
    {
        _dbData = dbData;
    }
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("ProdManagerName");
        if (string.IsNullOrEmpty(user))
        {
            return RedirectToAction("Index", "SalesLogin");
        }
        var id = HttpContext.Session.GetString("ProdManagerId");
        ViewBag.SalesPerson = _dbData.GetSalesPersons();
        ViewBag.ProdManagerOrders = _dbData.GetProdManagerOrders(id);
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateVendorOrder([FromBody] VendorOrderModel model)
    {
        await _dbData.AddVendorAndOrder(model.Vendor, model.WaterCanOrder);
        return Ok(new { message = "Vendor and order created successfully!" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}