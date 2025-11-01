using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Arwa.Models;

namespace Arwa.Controllers;

public class DeliveryAgentLoginController : Controller
{
    private readonly DbData _dbData;

    public DeliveryAgentLoginController(DbData dbData)
    {
        _dbData = dbData;
    }
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("DeliveryAgentUser");
        if (!string.IsNullOrEmpty(user))
        {
            return RedirectToAction("Index", "DeliveryAgent");
        }
        return View();
    }
    [HttpPost]
    public IActionResult Index(string username, string password)
    {
        string id = _dbData.IsValidDeliveryAgent(username, password);
        if (id != string.Empty)
        {
            HttpContext.Session.SetString("DeliveryAgentUser", username);
            HttpContext.Session.SetString("DeliveryAgentUserId", id);
            return RedirectToAction("Index", "DeliveryAgent");
        }
        ViewBag.Error = "Invalid username or password.";
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}