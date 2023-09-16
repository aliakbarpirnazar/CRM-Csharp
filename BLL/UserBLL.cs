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
    public class UserBLL
    {
        UserDAL dal = new UserDAL();

        private string EnCode(string Pass)
        {
            byte[] encData_byte = new byte[Pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(Pass);
            string enCodedData = Convert.ToBase64String(encData_byte);
            return enCodedData;
        }

        private string DeCode(string Decodedpass)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(Decodedpass);
            int CharCount = utf8Decode.GetCharCount(todecode_byte,0,todecode_byte.Length);
            char[] decoded_char = new char[CharCount];
            utf8Decode.GetChars(todecode_byte,0,todecode_byte.Length,decoded_char,0);
            string result = new string(decoded_char);
            return result;
        }
        public string Create(User u)
        {
            u.Password= EnCode(u.Password);
            return dal.Create(u);
        }
        public bool IsRegistered()
        {
            return dal.IsRegistered();
        }
        public User ReadU(string s)
        {
            return dal.ReadU(s);
        }
        public List<string> ReadUserNames()
        {
            return dal.ReadUserNames();
        }
        public DataTable Read(string s, int index)
        {
            return dal.Read(s, index);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public User Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(User c, int id)
        {
            c.Password= EnCode(c.Password);
            return dal.Update(c, id);

        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
    }
}
