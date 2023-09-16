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
using System.IO;
using BE;
using BLL;

namespace CRM
{
    public partial class RemindersForm : Form
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
        public RemindersForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        ReminderBLL Rbll = new ReminderBLL();
        UserBLL Ubll = new UserBLL();
        User u = new User();
        int id;
        string username;

        void ShowDataGridview()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Rbll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;

        }
        void ClearTextboxs()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            richTextBoxEx1.Text = "";
            textBoxX4.Text = "";
            textBoxX1.Enabled = true;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            pictureBox3_Click(sender, e);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
            username= Convert.ToString(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["نام کاربر"].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  Reminder r = Rbll.Read(id);
           // textBoxX1.Text = username;
            textBoxX1.Enabled=false;


            label1.Text = "ویرایش اطلاعات";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("آیا مطمعن هستید میخواهید یادآور را حذف کنید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show(Rbll.Delete(id));
                ShowDataGridview();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Reminder r = new Reminder();
            r.Title = textBoxX2.Text;
            r.ReminderInfo = richTextBoxEx1.Text;
            r.RegDate= DateTime.Now;
            r.RemindDate = dateTimeInput1.Value.Date;
            MessageBox.Show(Rbll.Create(r,u));
            ShowDataGridview();
            ClearTextboxs();
        }

        private void RemindersForm_Load(object sender, EventArgs e)
        {
            ShowDataGridview();
            AutoCompleteStringCollection Names = new AutoCompleteStringCollection();
            foreach(var item in Ubll.ReadUserNames())
            {
                Names.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = Names;
           

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxX1.Enabled = false;
            u = Ubll.ReadU(textBoxX1.Text);
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void انجامشدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("آیا انجام شد؟", "اطلاعیه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show(Rbll.Done(id));
                ShowDataGridview();
            }
        }

        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }
    }
}
