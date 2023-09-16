using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Activity
    {
        public Activity()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public DateTime RegDate { get; set; }
        public Customer Customers { get; set; }
        public bool DeleteStatus { get; set; }
        public User User { get; set; }
        public ActivityCategory Category { get; set; }
    }
}
