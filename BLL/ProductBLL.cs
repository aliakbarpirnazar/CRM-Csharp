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
    public class ProductBLL
    {
        ProductDAL dal = new ProductDAL();
        public string Create(Product c)
        {
            return dal.Create(c);
            //if (dal.Read(c))
            //{
            //    return dal.Create(c);
            //}
            //else
            //{
            //    return "کاربر قبلا ثبت نام کرده است";
            //}
        }
        public DataTable Read(string s, int index)
        {
            return dal.Read(s, index);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public Product Read(int id)
        {
            return dal.Read(id);
        }
        public List<string> ReadNames()
        {
            return dal.ReadNames();
        }
        public Product ReadN(string p)
        {
            return dal.ReadN(p);
        }
        public string Update(Product c, int id)
        {
            return dal.Update(c, id);

        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
    }
}
