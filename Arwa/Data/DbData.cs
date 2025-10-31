using System.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

public class DbData : DbContext
{
    private readonly ApplicationDbContext _context;
    public DbData(ApplicationDbContext context)
    {
        _context = context;
    }
    public DataTable GetDailySalesData(string day)
    {
        DateTime today = DateTime.Today;
        string formattedDate = today.ToString($"yyyy-MM-{day}");
        var sql = @"SELECT pm.Name AS `Prod. Manager`, v.Name AS `Vendor Name`, v.Location AS `Vendor Location`, v.Phone AS `Vendor Phone`, wc.Cases025Ltr AS `0.25L`, wc.Cases05Ltr AS `0.5L`, wc.Cases1Ltr AS `1L`, wc.Cases2Ltr AS `2L`, wc.Cases5Ltr AS `5L`, wc.Cases20Ltr AS `20L`, wc.AmountBilled AS `Amount Billed`, wc.AmountPaid AS `Amount Paid`, (wc.AmountBilled - wc.AmountPaid) AS `Debt`, TIME(wc.BillingDate) AS BillingTime FROM WaterCanOrders wc INNER JOIN ProdManagers pm ON wc.ProdManagerId = pm.ProdManagerId INNER JOIN Vendors v ON wc.VendorId = v.VendorId WHERE DATE(wc.BillingDate) = @BillingDate";
        var dt = new DataTable();
        using (var conn = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString))
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    var param = cmd.CreateParameter();
                    param.ParameterName = "@BillingDate";
                    param.Value = formattedDate;
                    cmd.Parameters.Add(param);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        return dt;
    }
    public DataTable GetMonthlySalesData(DateTime targetDate)
    {
        var dt = new DataTable();
        string sql = @"SELECT SUM(AmountBilled) AS 'Total Billed', SUM(AmountPaid) AS 'Total Paid' 
        FROM WaterCanOrders WHERE YEAR(BillingDate) = @Year AND MONTH(BillingDate) = @Month";
        using (var conn = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString))
        {
            try
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Year", targetDate.Year);
                    cmd.Parameters.AddWithValue("@Month", targetDate.Month);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        return dt;
    }
    public DataTable GetVendorNames()
    {
        var dt = new DataTable();
        string sql = @"SELECT DISTINCT VendorId, Name FROM Vendors";
        using (var conn = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString))
        {
            try
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        return dt;
    }
    public DataTable GetSalesDataBasedOnVendor(string day, string vendor)
    {
        DateTime today = DateTime.Today;
        string formattedDate = today.ToString($"yyyy-MM-{day}");
        var sql = @"SELECT pm.Name AS `Prod. Manager`, v.Name AS `Vendor Name`, v.Location AS `Vendor Location`, v.Phone AS `Vendor Phone`, wc.Cases025Ltr AS `0.25L`, wc.Cases05Ltr AS `0.5L`, wc.Cases1Ltr AS `1L`, wc.Cases2Ltr AS `2L`, wc.Cases5Ltr AS `5L`, wc.Cases20Ltr AS `20L`, wc.AmountBilled AS `Amount Billed`, wc.AmountPaid AS `Amount Paid`, (wc.AmountBilled - wc.AmountPaid) AS `Debt`, TIME(wc.BillingDate) AS BillingTime FROM WaterCanOrders wc INNER JOIN ProdManagers pm ON wc.ProdManagerId = pm.ProdManagerId INNER JOIN Vendors v ON wc.VendorId = v.VendorId WHERE DATE(wc.BillingDate) = @BillingDate and v.Name = @VendorName";
        var dt = new DataTable();
        using (var conn = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString))
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    var billingDateParam = cmd.CreateParameter();
                    billingDateParam.ParameterName = "@BillingDate";
                    billingDateParam.Value = formattedDate;
                    cmd.Parameters.Add(billingDateParam);

                    var vendorParam = cmd.CreateParameter();
                    vendorParam.ParameterName = "@VendorName";
                    vendorParam.Value = vendor;
                    cmd.Parameters.Add(vendorParam);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        return dt;
    }
    public string IsValidProdManager(string email, string password)
    {
        string ProdManagerId = string.Empty;
        var sql = @"SELECT ProdManagerId FROM ProdManagers where upper(name) = upper(@name) AND password = @Password AND ROLE='PM'";
        using (var conn = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString))
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    var emailParam = cmd.CreateParameter();
                    emailParam.ParameterName = "@name";
                    emailParam.Value = email;
                    cmd.Parameters.Add(emailParam);
                    var passwordParam = cmd.CreateParameter();
                    passwordParam.ParameterName = "@Password";
                    passwordParam.Value = password;
                    cmd.Parameters.Add(passwordParam);
                    var result = cmd.ExecuteScalar();
                    ProdManagerId = (result != null && result != DBNull.Value) ? result.ToString() : string.Empty;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ProdManagerId;
        }
    }
    public DataTable GetSalesPersons()
    {
        var dt = new DataTable();
        string sql = @"SELECT DISTINCT SalesPersonId, Name FROM SalesPersons";
        using (var conn = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString))
        {
            try
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        return dt;
    }
    public async Task AddVendorAndOrder(Vendor vendor, WaterCanOrders order)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            order.VendorId = vendor.VendorId;
            order.BillingDate = DateTime.Now;
            _context.WaterCanOrders.Add(order);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public DataTable GetProdManagerOrders(string prodManagerId)
    {
        var dt = new DataTable();
        string sql = @"SELECT w.WaterCanOrderId,p.Name AS `Prod. Manager`, v.Name AS `Vendor Name`, v.Location AS `Vendor Location`, v.Phone AS `Vendor Phone`, w.Cases025Ltr AS `0.25L`, w.Cases05Ltr AS `0.5L`, w.Cases1Ltr AS `1L`, w.Cases2Ltr AS `2L`, w.Cases5Ltr AS `5L`, w.Cases20Ltr AS `20L`, w.AmountBilled AS `Amount Billed`, w.AmountPaid AS `Amount Paid`, (w.AmountBilled - w.AmountPaid) AS `Debt`, w.BillingDate AS `BillingTime`, CASE w.OrderStatus WHEN 1 THEN 'Pending' WHEN 2 THEN 'Collected' WHEN 3 THEN 'Return' ELSE 'Unknown' END AS 'OrderStatus' FROM WaterCanOrders w INNER JOIN ProdManagers p ON w.ProdManagerId = p.ProdManagerId INNER JOIN Vendors v ON w.VendorId = v.VendorId INNER JOIN SalesPersons s ON s.SalesPersonId = w.SalesPersonId WHERE p.ProdManagerId = @ProdManagerId order by BillingTime asc";
        using (var conn = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString))
        {
            try
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    var param = cmd.CreateParameter();
                    param.ParameterName = "@ProdManagerId";
                    param.Value = prodManagerId;
                    cmd.Parameters.Add(param);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        return dt;
    }

}