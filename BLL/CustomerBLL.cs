using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
namespace BLL
{
    public class CustomerBLL
    {
        CustomerDAL dal = new CustomerDAL();
        public string Create(Customer c)
        {
            if (dal.Read(c))
            {
                return dal.Create(c);
            }
            else
            {
                return "کاربر قبلا ثبت نام کرده است";
            }
        }
        public DataTable Read(string s,int index)
        {
            return dal.Read(s,index);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public List<string> ReadPhone()
            { return dal.ReadPhone(); }
        public List<string> ReadName()
        {
            return dal.ReadName();
        }
        public Customer Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(Customer c, int id)
        {
            return dal.Update(c, id);
           
        }
        public string Delete(int id)
        {
          return  dal.Delete(id);
        }
        public Customer ReadC(string s)
        {
            return dal.ReadC(s);
        }
    }
    
}
