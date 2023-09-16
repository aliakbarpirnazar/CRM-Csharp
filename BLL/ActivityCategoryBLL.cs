using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class ActivityCategoryBLL
    {
        ActivityCategoryDAL dal = new ActivityCategoryDAL();

    
        public string Create(ActivityCategory u)
        {
            return dal.Create(u);
        }
        public ActivityCategory ReadAC(string s)
        {
            return dal.ReadAC(s);
        }
        public List<string> ReadCategoryNames()
        {
            return dal.ReadCategoryNames();
        }
        
        public DataTable Read()
        {
            return dal.Read();
        }
        
        public ActivityCategory Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(ActivityCategory c, int id)
        {
           
            return dal.Update(c, id);

        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
    }
}
