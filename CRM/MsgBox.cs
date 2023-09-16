using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public class MsgBox
    {
        public DialogResult MyShowDialog(string Title,string FaInfo,string EngInfo,bool Buttons,bool Type)
        {
            MyMsgBox m = new MyMsgBox();
            m.labelX1.Text = EngInfo;
            m.labelX2.Text = Title;
            m.labelX3.Text = FaInfo;
            if (Buttons)
            {
                m.buttonX1.Text = "خیر";
                m.buttonX2.Text = "بله";
            }
            else
            {
                m.buttonX2.Visible = false;
                m.buttonX1.Text = "خروج";
            }
            if (Type)
            {
                m.BackColor = Color.FromArgb(242, 69, 29);
               m.pictureBox1.Image =Properties.Resources.Warning;
            }

            m.ShowDialog();
            return m.DialogResult;
        }
    }
}
