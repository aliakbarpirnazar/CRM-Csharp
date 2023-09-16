using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ActivityDAL
    {

        DB db = new DB();
        public string Create(Activity a, User u,Customer c,ActivityCategory ac)
        {
            try
            {
                a.User=db.Users.Find(u.id);
                a.Customers = db.Customers.Find(c.id);
                a.Category=db.activityCategories.Find(ac.id);
                db.activities.Add(a);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";

            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکل مواجه شد! \n" + e.Message;
            }
        }
        public DataTable Read()
        {
            string cmd = @"SELECT dbo.Activities.id AS آیدی, dbo.Activities.Title AS [موضوع فعالیت], dbo.ActivityCategories.CategoryName AS [دسته بندی], dbo.Customers.Name AS [نام مشتری], dbo.Users.Name AS [نام کاربر], dbo.Activities.RegDate AS [تاریخ ثبت] FROM dbo.Activities INNER JOIN dbo.ActivityCategories ON dbo.Activities.Category_id = dbo.ActivityCategories.id INNER JOIN dbo.Customers ON dbo.Activities.Customers_id = dbo.Customers.id INNER JOIN dbo.Users ON dbo.Activities.User_id = dbo.Users.id";
            SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = DBCRM ;Integrated Security = True");
            var sqladapter = new SqlDataAdapter(cmd,con);
            var commandBuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

        }
        public Activity Read(int id)
        {

            return db.activities.Where(i => i.id == id).FirstOrDefault();
        }   
        public string Delete(int id)
        {
            var q = db.activities.Where(i => i.id == id).FirstOrDefault();
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
       
    }
}
