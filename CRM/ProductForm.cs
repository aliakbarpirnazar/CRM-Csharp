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
    public partial class ProductForm : Form
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
        public ProductForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        ProductBLL bll = new ProductBLL();
        int id;
        void ShowDataGridview()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;

        }
        void ClearTextboxs()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
            textBoxX5.Text = "";
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Product c = new Product();
            c.Name =textBoxX1.Text;
            c.Price =Convert.ToInt32(textBoxX2.Text);
            c.Stock=Convert.ToInt32(textBoxX5.Text);

            if (label1.Text == "ویرایش اطلاعات")
            {
                MessageBox.Show(bll.Update(c, id));
                label1.Text = "ثبت اطلاعات";
            }
            else if (label1.Text == "ثبت اطلاعات")
            {
                MessageBox.Show(bll.Create(c));
            }
            ShowDataGridview();
            ClearTextboxs();
        }

      

        private void label7_Click(object sender, EventArgs e)
        {
            pictureBox3_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            ShowDataGridview();
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product c = bll.Read(id);
            textBoxX1.Text = c.Name;
            textBoxX2.Text =Convert.ToString(c.Price);
            textBoxX5.Text =Convert.ToString(c.Stock);
            label1.Text = "ویرایش اطلاعات";

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            DialogResult dr= MessageBox.Show("آیا مطمعن هستید میخواهید کاربر را حذف کنید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show(bll.Delete(id));
                ShowDataGridview();
            }

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }
    }
}
