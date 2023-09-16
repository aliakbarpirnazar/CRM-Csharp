using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class InvoiceBLL
    {
        InvoiceDAL dal = new InvoiceDAL();
        public string Create(Invoice i, Customer c, List<Product> p)
        {
            return dal.Create(i, c, p);
                }
        public string ReadInvoiceNum()
        {
            return dal.ReadInvoiceNum();
        }
        public string PrintInvoice(int id)
        {
            return dal.PrintInvoice(id);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public Invoice Read(int id)
        {
            return dal.Read(id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public string IschechedOut(int id)
        {
            return dal.IschechedOut(id);
        }
        }
}
