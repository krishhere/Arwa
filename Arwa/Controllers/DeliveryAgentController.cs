using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Arwa.Models;

namespace Arwa.Controllers;

public class DeliveryAgentController : Controller
{
    private readonly DbData _dbData;
    public DeliveryAgentController(DbData dbData)
    {
        _dbData = dbData;
    }
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("DeliveryAgentUser");
        if (string.IsNullOrEmpty(user))
        {
            return RedirectToAction("Index", "DeliveryAgentLogin");
        }
        var deliveryAgentUserId = HttpContext.Session.GetString("DeliveryAgentUserId");
        ViewBag.DeliveryAgentUser = _dbData.GetDeliveryAgentUser(deliveryAgentUserId);
        return View();
    }
    [HttpPost]
    public IActionResult UpdatePayment(int orderId, decimal amountPaid)
    {
        string result = _dbData.UpdatePayment(orderId, amountPaid);
        if (!result.Contains("success"))
        {
            return Json(new { success = false, message = result });
        }
        return Json(new { success = true, message = result });
    }
    [HttpGet]
    public IActionResult GetDebtByOrderId(int orderId)
    {
        var debt = _dbData.GetDebtByOrderId(orderId);
        return Json(debt);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}