using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class User
    {
        public User()
        {
            DeleteStatus = false;
        }

        public int id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Pic { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime RegDate { get; set; }
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<Reminder> Remeinders { get; set; } = new List<Reminder>();
        public UserGroup UserGroup { get; set; }
    }
}
