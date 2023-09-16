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
    public class ActivityCategoryDAL
    {
        DB db = new DB();
        public string Create(ActivityCategory u)
        {
            try
            {
                if (Read(u))
                {
                    db.activityCategories.Add(u);
                    db.SaveChanges();
                    return "ثبت دسته بندی با موفقیت انجام شد";
                }
                else
                {
                    return "دسته بندی تکراری است";
                }
            }
            catch (Exception e)
            {
                return "ثبت دسته بندی با مشکل مواجه است \n" + e.Message;
            }
        }
        public bool Read(ActivityCategory u)
        {
            var q = db.activityCategories.Where(i => i.CategoryName == u.CategoryName);
            if (q.Count() == 0)
            {
                return true;
            }
            else
                return false;
        }
        public ActivityCategory ReadAC(string s)
        {
            return db.activityCategories.Where(i => i.CategoryName == s).SingleOrDefault();
        }
        public List<string> ReadCategoryNames()
        {
            return db.activityCategories.Where(i => i.DeleteStatus == false).Select(i => i.CategoryName).ToList();
        }
        public DataTable Read()
        {
            string cmd = "SELECT id AS [آیدی], CategoryName AS [نام دسته بندی]  FROM dbo.activityCategories WHERE(DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog = DBCRM ;Integrated Security = True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandBuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public ActivityCategory Read(int id)
        {
            return db.activityCategories.Where(i => i.id == id).FirstOrDefault();
        }
        private SqlConnection SqlConnection(string v)
        {
            throw new NotImplementedException();
        }

        public string Update(ActivityCategory c, int id)
        {
            var q = db.activityCategories.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CategoryName = c.CategoryName;
                    db.SaveChanges();
                    return "ویرایش دسته بندی موفق بود";
                }
                else
                    return "دسته بندی مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "ویرایش دسته بندی با مشکل مواجه بود \n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.activityCategories.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "دسته بندی موردنظر حذف شد";
                }
                else
                    return "دسته بندی مورد نظر یافت نشد";
            }
            catch (Exception e)
            {
                return "حذف دسته بندی با مشکل مواجه بود \n" + e.Message;
            }
        }
    }
}
