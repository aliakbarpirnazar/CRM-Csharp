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
    public class CustomerDAL
    {
        DB db = new DB();
        public string Create(Customer c)
        {
            try
            {
                db.Customers.Add(c);
                db.SaveChanges();
                return "ثبت اطلاعات مشتری با موفقیت انجام شد";
            }
            catch(Exception e)
            {
                return "ثبت اطلاعات با مشکل مواجه شد\n" + e.Message ;
            }
        }
        public bool Read(Customer c){
            var q = db.Customers.Where(i => c.Phone == i.Phone );
            if (q.Count() == 0)
            {
                return true;
            }
            else
                return false;


        }
        public DataTable Read()
        {
            string cmd = "SELECT id AS [آیدی], Name AS [نام مشتری], Phone AS [شماره تماس], RegDate AS [تاریخ ثبت] FROM dbo.Customers WHERE(DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = DBCRM ;Integrated Security = True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandBuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s , int index)
        {
            SqlCommand cmd = new SqlCommand();
            if (index == 0)
            {
                cmd.CommandText="dbo.SearchCustomer";
            }
            else if (index==1)
            {
                cmd.CommandText = "dbo.SearchCustomerName";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchCustomerPhone";
            }
            SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = DBCRM ;Integrated Security = True");
            
            cmd.Parameters.AddWithValue("@search",s);
            cmd.Connection = con;
            cmd.CommandType= CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandBuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public Customer ReadC(string s)
        {
            return db.Customers.Where(i => i.Phone == s).SingleOrDefault();
        }

        public Customer Read(int id)
        {
            return db.Customers.Where(i=>i.id ==id).FirstOrDefault();
        }
        public List<string> ReadPhone()
        {
            return db.Customers.Where(i => i.DeleteStatus == false).Select(i => i.Phone).ToList();
        }
        public List<string> ReadName()
        {
            return db.Customers.Where(i => i.DeleteStatus == false).Select(i => i.Name).ToList();
        }
        private SqlConnection SqlConnection(string v)
        {
            throw new NotImplementedException();
        }

        public string Update(Customer c,int id)
        {
            var q=db.Customers.Where(i=>i.id ==id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Name = c.Name;
                    q.Phone = c.Phone;
                    db.SaveChanges();
                    return "ویرایش اطلاعات موفق بود";
                }
                else
                    return "کاربر مورد نظر یافت نشد";
            }
            catch(Exception e)
            {
                return "ویرایش اطلاعات با مشکل مواجه بود \n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.Customers.Where(i => i.id == id).FirstOrDefault();
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
