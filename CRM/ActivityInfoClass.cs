using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    public class ActivityInfoClass
    {
        public void ActivityInfo(string Title,string Activities)
        {
            ActivityInfoForm frm = new ActivityInfoForm();
            frm.labelX2.Text = Title;
            frm.labelX3.Text = Activities;
            frm.ShowDialog();
            
        }
    }
}
