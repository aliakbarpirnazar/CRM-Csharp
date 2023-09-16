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
using BE;
using BLL;

namespace CRM
{
    public partial class CustomersForm : Form
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
        public CustomersForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
       CustomerBLL bll = new CustomerBLL();
       MsgBox msgBox = new MsgBox();
        int id;
        void ShowDataGridview()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["آیدی"].Visible=false;

        }
        void ClearTextboxs()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
        }
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            c.Name = textBoxX1.Text;
            c.Phone = textBoxX2.Text;
            c.RegDate = DateTime.Now;
            if (label1.Text== "ویرایش اطلاعات")
            {
                msgBox.MyShowDialog("اطلاعیه",bll.Update(c,id),"",false,false);
               //MessageBox.Show(bll.Update(c,id));
                label1.Text = "ثبت اطلاعات";
            }
            else if(label1.Text== "ثبت اطلاعات")
            {
               msgBox.MyShowDialog("اطلاعیه", bll.Create(c), "", false, false);
                //MessageBox.Show( bll.Create(c));
            }
            
            
            ShowDataGridview();
            ClearTextboxs();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            pictureBox3_Click(sender,e);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        int index;
        private void textBoxX4_TextChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked&& checkBox2.Checked || !checkBox1.Checked && !checkBox2.Checked)
            {
                index = 0;
            }
            else if (checkBox1.Checked)
            {
                index = 1;
            }
            else if (checkBox2.Checked)
            {
                index = 2;
            }
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX4.Text,index);
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
           ShowDataGridview();
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
           
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer c = bll.Read(id);
            textBoxX1.Text = c.Name;
            textBoxX2.Text = c.Phone;
            label1.Text = "ویرایش اطلاعات";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  DialogResult dr = MessageBox.Show("آیا مطمعن هستید میخواهید کاربر را حذف کنید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            DialogResult dr = msgBox.MyShowDialog("اخطار", "درصوردت حذف مشتری تمام اطلاعات مشتری حذف میشود \nآیا مطمئن هستید که میخواهید حذف کنید؟", "", true, true); 
           
            if(dr == DialogResult.Yes)
            {
                // MessageBox.Show(bll.Delete(id));
                msgBox.MyShowDialog("اطلاعیه", bll.Delete(id), "", false, false);
                ShowDataGridview();
            }
            
        }
    }
}
