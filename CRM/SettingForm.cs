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
    public partial class SettingForm : Form
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
        public SettingForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;
        ActivityCategoryBLL bll = new ActivityCategoryBLL();
        MsgBox msgBox = new MsgBox();
        void ShowDataGridview()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;

        }
        void ClearTextboxs()
        {
            textBoxX1.Text = "";
            
        }
        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            label1_Click(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ActivityCategory c = new ActivityCategory();
            c.CategoryName = textBoxX1.Text;
            
            if (label1.Text == "ویرایش دسته بندی")
            {
                msgBox.MyShowDialog("اطلاعیه", bll.Update(c, id), "", false, false);
               
                label1.Text = "ثبت دسته بندی جدید";
            }
            else if (label1.Text == "ثبت دسته بندی جدید")
            {
                msgBox.MyShowDialog("اطلاعیه", bll.Create(c), "", false, false);
                
            }


            ShowDataGridview();
            ClearTextboxs();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivityCategory c = bll.Read(id);
            textBoxX1.Text = c.CategoryName;
            
            label1.Text = "ویرایش دسته بندی";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = msgBox.MyShowDialog("اخطار", "آیا مطمئن هستید میخواهید دسته بندی حذف کنید؟", "", true, true);

            if (dr == DialogResult.Yes)
            {
                // MessageBox.Show(bll.Delete(id));
                msgBox.MyShowDialog("اطلاعیه", bll.Delete(id), "", false, false);
                ShowDataGridview();
            }
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            ShowDataGridview();
        }

        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }
    }
}
