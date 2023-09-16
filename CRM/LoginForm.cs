using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;

namespace CRM
{
    public partial class LoginForm : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
          (
              int nLeftRect,     // x-coordinate of upper-left corner
              int nTopRect,      // y-coordinate of upper-left corner
              int nRightRect,    // x-coordinate of lower-right corner
              int nBottomRect,   // y-coordinate of lower-right corner
              int nWidthEllipse, // width of ellipse
              int nHeightEllipse // height of ellipse
          );
        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        Timer t1 = new Timer();
        Timer t2 = new Timer();
        UserBLL UBLL = new UserBLL();
        List<string> username = new List<string>();
        RegisterAdmin r = new RegisterAdmin();
        LoginUC uc = new LoginUC();
        bool _IsRegistered;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            label2.Visible = true;
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start();
            this.Controls.Add(r);
            this.Controls["RegisterAdmin"].Location = new Point(290, 650); 
            this.Controls.Add(uc);
            this.Controls["LoginUC"].Location = new Point(290, 650);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBarX1.Value >= 180)
            {
                t1.Stop();
                progressBarX1.Visible = false;
                label2.Visible=false;
                label1.Visible = true;
                t2.Enabled = true;
                t2.Interval = 1;
                t2.Tick+=Timer2_Tick;
                t2.Start();

            }
            else if(progressBarX1.Value==45)
            {
                _IsRegistered = UBLL.IsRegistered();
                progressBarX1.Value++;
            }
            else
            {
                progressBarX1.Value++;
            }
        }
        int y = 650;
        int y2 = 650;
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (progressBarX1.Value >= 180)
            {
                y = y - 15;
                y2 = y2 - 30;
                label1.Location = new Point(468, y);
                if (_IsRegistered)
                {
                    this.Controls["LoginUC"].Location = new Point(290, y2);
                }
                else
                {
                  this.Controls["RegisterAdmin"].Location = new Point(290, y2);
                }
                
            }
            else
            {
                t2.Stop();
                panel1.Visible = true;
            }
        }

    }
}
