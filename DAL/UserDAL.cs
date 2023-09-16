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
    public class UserDAL
    {
        DB db = new DB();
        public string Create(User u)
        {
            try
            {
                if (Read(u))
                {
                    db.Users.Add(u);
                    db.SaveChanges();
                    return "ثبت اطلاعات کاربر با موفقیت انجام شد";
                } else
                {
                    return "نام کاربری تکاراری است";
                }
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکل مواجه است \n"+e.Message;
            }
        }
        public bool IsRegistered()
        {
            return db.Users.Count() > 0;
        }
        public bool Read(User u)
        {
            var q= db.Users.Where(i=> i.UserName== u.UserName);
            if (q.Count() == 0)
            {
                return true;
            }
            else
                return false;
        }
        public User ReadU(string s)
        {
            return db.Users.Where(i => i.UserName == s).SingleOrDefault();
        }
        public List<string> ReadUserNames()
        {
            return db.Users.Where(i=>i.DeleteStatus==false).Select(i => i.UserName).ToList();
        }
        public DataTable Read()
        {
            string cmd = "SELECT id AS [آیدی], Name AS [نام و نام خانوادگی], UserName AS [نام کاربر], RegDate AS [تاریخ ثبت] FROM dbo.Users WHERE(DeleteStatus = 0)";
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
            cmd.CommandText = "dbo.SearchUser";
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

        public User Read(int id)
        {
            return db.Users.Where(i => i.id == id).FirstOrDefault();
        }
        private SqlConnection SqlConnection(string v)
        {
            throw new NotImplementedException();
        }

        public string Update(User c, int id)
        {
            var q = db.Users.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Name = c.Name;
                    q.UserName = c.UserName;
                    q.Pic = c.Pic;
                    q.Password = c.Password;
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
            var q = db.Users.Where(i => i.id == id).FirstOrDefault();
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
