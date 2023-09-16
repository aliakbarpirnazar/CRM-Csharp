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
    public class ReminderDAL
    {
        DB db = new DB();
        public string Create(Reminder r,User u)
        {
            try
            {
                r.Users = db.Users.Find(u.id);
                db.reminders.Add(r);
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
            string cmd = @"SELECT  dbo.Reminders.id AS آیدی, dbo.Reminders.Title AS [موضوع یادآور], dbo.Reminders.ReminderInfo AS [توضیحات یادآور], dbo.Reminders.RegDate AS [تارخ ثبت یادآور], dbo.Reminders.RemindDate AS [تاریخ یادآوری], 
dbo.Reminders.IsDone AS [ثبت شد], dbo.Users.UserName AS [نام کاربر]
FROM            dbo.Reminders INNER JOIN
dbo.Users ON dbo.Reminders.Users_id = dbo.Users.id
WHERE        (dbo.Reminders.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = DBCRM ;Integrated Security = True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandBuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

        }
        public DataTable Read(string s)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SearchReminder";
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
        
        public string Update(Reminder r,int id)
        {
            var q = db.reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.RemindDate = r.RemindDate;
                    q.ReminderInfo= r.ReminderInfo;
                    q.Title = r.Title;
                    db.SaveChanges();
                    return "ویرایش اطلاعات موفق بود";
                }
                else
                    return "یادآور مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "ویرایش اطلاعات با مشکل مواجه بود \n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "یادآور موردنظر حذف شد";
                }
                else
                    return "یادآور مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "حذف اطلاعات با مشکل مواجه بود \n" + e.Message;
            }
        }
        public string Done(int id)
        {
            var q = db.reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsDone = true;
                    db.SaveChanges();
                    return "یادآور موردنظر حذف شد";
                }
                else
                    return "یادآور مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "حذف اطلاعات با مشکل مواجه بود \n" + e.Message;
            }

        }
    }
}
