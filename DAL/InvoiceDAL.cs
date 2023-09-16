using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class InvoiceDAL
    {
        DB db = new DB();
        public string Create(Invoice i, Customer c, List<Product> p)
        {
            try
            {
                i.Customers = db.Customers.Find(c.id);
                foreach (var item in p)
                {
                    i.Products.Add(db.Products.Find(item.id));
                }
                Random rnd = new Random();
                string s = rnd.Next(1000000000).ToString();
                var q = db.invoices.Where(h => h.InvoiceNumber == s);
                while (q.Count() > 0)
                {
                    s = rnd.Next(1000000000).ToString();
                }
                i.InvoiceNumber = s;
                db.invoices.Add(i);
                db.SaveChanges();
                return "ثبت فاکتور با موفقیت انجام شد";
            }
            catch (Exception e)
            {

                return "ثبت فاکتور با مشکل مواجه شد \n" + e.Message;
            }
        }
        public string ReadInvoiceNum()
        {
            var q = db.invoices.OrderByDescending(i => i.id).FirstOrDefault();
            return q.InvoiceNumber;
        }
        public string PrintInvoice(int id)
        {
            var q = db.invoices.Where(i => i.id == id).FirstOrDefault();
            return q.InvoiceNumber;
        }
        public DataTable Read()
        {
            string cmd = @"SELECT        dbo.Invoices.id AS آیدی, dbo.Invoices.InvoiceNumber AS [شماره فاکتور], dbo.Invoices.RegDate AS [تاریخ ثبت], dbo.Customers.Name AS [نام مشتری], dbo.Invoices.IsCheckedout AS [پرداخت شده], 
                         dbo.Invoices.CheckoutDate AS [تاریخ پرداخت]
FROM            dbo.Invoices INNER JOIN
                         dbo.Customers ON dbo.Invoices.Customers_id = dbo.Customers.id
WHERE        (dbo.Invoices.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = DBCRM ;Integrated Security = True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandBuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
            
        }
        public Invoice Read(int id)
        {

            return db.invoices.Where(i => i.id == id).FirstOrDefault();
            
        }
        public string Delete(int id)
        {
            var q = db.invoices.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "فعالیت موردنظر حذف شد";
                }
                else
                    return "فعالیت مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "حذف فعالیت با مشکل مواجه بود \n" + e.Message;
            }
        }
        public string IschechedOut(int id)
        {
            var q = db.invoices.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsCheckedout = true;
                    q.CheckoutDate = DateTime.Now;
                    db.SaveChanges();
                    return "فعالیت موردنظر تایید شد";
                }
                else
                    return "فعالیت مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "تایید فعالیت با مشکل مواجه بود \n" + e.Message;
            }
        }
    }
}
