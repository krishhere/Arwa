using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Arwa.Models;

namespace Arwa.Controllers;

public class SalesLoginController : Controller
{
    private readonly DbData _dbData;

    public SalesLoginController(DbData dbData)
    {
        _dbData = dbData;
    }
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("ProdManagerName");
        if (!string.IsNullOrEmpty(user))
        {
            return RedirectToAction("Index", "Sales");
        }
        return View();
    }
    [HttpPost]
    public IActionResult Index(string username, string password)
    {
        string id = _dbData.IsValidProdManager(username, password);
        if (id != string.Empty)
        {
            HttpContext.Session.SetString("ProdManagerName", username);
            HttpContext.Session.SetString("ProdManagerId", id);
            return RedirectToAction("Index", "Sales");
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