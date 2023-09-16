using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;

namespace DAL
{
    public class ProductDAL
    {
        DB db = new DB();
        public string Create(Product c)
        {
            try
            {
                db.Products.Add(c);
                db.SaveChanges();
                return "ثبت اطلاعات کالا با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکل مواجه شد\n" + e.Message;
            }
        }
        public bool Read(Product c)
        {
            var q = db.Products.Where(i => c.Name == i.Name);
            if (q.Count() == 0)     
            {
                return true;
            }
            else
                return false;


        }
        public DataTable Read()
        {
            string cmd = @"SELECT id AS آیدی, Name AS [نام کالا], Price AS [قیمت کالا], Stock AS [موجودی کالا] FROM dbo.Products WHERE(DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = DBCRM ;Integrated Security = True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandBuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s, int index)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.SearchCustomer";
            SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = DBCRM ;Integrated Security = True");
            cmd.Parameters.AddWithValue("@search", s);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandBuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public Product Read(int id)
        {
            return db.Products.Where(i => i.id == id).FirstOrDefault();
        }
        public List<string> ReadNames()
        {
            return db.Products.Where(i => i.DeleteStatus == false).Where(i=>i.Stock>0).Select(i=> i.Name).ToList();
        }
        public Product ReadN(string p)
        {
            return db.Products.Where(i => i.Name == p).SingleOrDefault();
        }
        private SqlConnection SqlConnection(string v)
        {
            throw new NotImplementedException();
        }

        public string Update(Product c, int id)
        {
            var q = db.Products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Name = c.Name;
                    q.Price = c.Price;
                    q.Stock = c.Stock;
                    db.SaveChanges();
                    return "ویرایش اطلاعات موفق بود";
                }
                else
                    return "کاربر مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "ویرایش اطلاعات با مشکل مواجه بود \n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.Products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "کاربر موردنظر حذف شد";
                }
                else
                    return "کاربر مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "حذف اطلاعات با مشکل مواجه بود \n" + e.Message;
            }
        }
    }
}
