using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Arwa.Models;
using System.Data;

namespace Arwa.Controllers;

public class DailySalesController : Controller
{
    private readonly DbData _dbData;

    public DailySalesController(DbData dbData)
    {
        _dbData = dbData;
    }
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("ProductUser");
        if (string.IsNullOrEmpty(user))
        {
            return View("~/Views/DailySalesLogin/Index.cshtml");
        }
        DataTable dailySales = _dbData.GetDailySalesData(DateTime.Today.Day.ToString());
        DataTable monthlyTotalSales = _dbData.GetMonthlySalesData(DateTime.Today);
        DataTable vendorNames = _dbData.GetVendorNames();

        var dataSet = new DataSet();

        dailySales.TableName = "DailySales";
        monthlyTotalSales.TableName = "monthlyTotalSales";
        vendorNames.TableName = "vendorNames";

        dataSet.Tables.Add(dailySales);
        dataSet.Tables.Add(monthlyTotalSales);
        dataSet.Tables.Add(vendorNames);

        ViewBag.SalesHistory = dataSet;
        ViewData["Title"] = "Monthly Sales";
        return View();
    }
    [HttpGet]
    public IActionResult GetDataByDate(string day)
    {
        DataTable dt = _dbData.GetDailySalesData(day.ToString());
        return PartialView("~/Views/DailySales/Partials/_SalesHistoryPartial.cshtml", dt);
    }
    [HttpGet]
    public IActionResult GetSalesDataBasedOnVendor(string day, string vendorName)
    {
        DataTable dt = _dbData.GetSalesDataBasedOnVendor(day, vendorName);
        return PartialView("~/Views/DailySales/Partials/_SalesHistoryPartial.cshtml", dt);
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("ProductUser");
        return View("DailySalesLogin");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}