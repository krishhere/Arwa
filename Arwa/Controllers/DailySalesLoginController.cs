using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Arwa.Models;

namespace Arwa.Controllers;

public class DailySalesLoginController : Controller
{
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("ProductUser");
        if (!string.IsNullOrEmpty(user))
        {
            return RedirectToAction("Index", "DailySales");
        }
        return View();
    }
    [HttpPost]
    public IActionResult Index(string username, string password)
    {
        if (username == "admin" && password == "admin")
        {
            HttpContext.Session.SetString("ProductUser", username);
            return RedirectToAction("Index", "DailySales");
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