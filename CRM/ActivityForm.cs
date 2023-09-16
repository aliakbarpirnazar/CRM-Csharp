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
    public partial class ActivityForm : Form
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
        public ActivityForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        ActivityInfoClass Information = new ActivityInfoClass();
        ReminderBLL Rbll = new ReminderBLL();
        ActivityBLL Abll = new ActivityBLL();
        UserBLL Ubll = new UserBLL();
        User u = new User();
        CustomerBLL Cbll = new CustomerBLL();
        Customer C = new Customer();
        ActivityCategoryBLL ACbll = new ActivityCategoryBLL();
        ActivityCategory AC = new ActivityCategory();
        MsgBox MsgBox = new MsgBox();

        int id;
        void ShowDataGridview()
        {
            dataGridViewX1.DataSource = null;
           dataGridViewX1.DataSource = Abll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;

        }
        void ClearTextboxs()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
            textBoxX5.Text = "";
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void colorPickerButton1_SelectedColorChanged(object sender, EventArgs e) 
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            ShowDataGridview();
            AutoCompleteStringCollection Names = new AutoCompleteStringCollection();
            foreach (var item in Ubll.ReadUserNames())
            {
                Names.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = Names;
            AutoCompleteStringCollection Phone = new AutoCompleteStringCollection();
            foreach (var item in Cbll.ReadPhone())
            {
                Phone.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = Phone;
            AutoCompleteStringCollection ACNames = new AutoCompleteStringCollection();
            foreach (var item in ACbll.ReadCategoryNames())
            {
                ACNames.Add(item);
            }
            textBoxX3.AutoCompleteCustomSource = ACNames;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBoxX2.Enabled = false;
            u = Ubll.ReadU(textBoxX2.Text);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxX1.Enabled = false;
            C= Cbll.ReadC(textBoxX1.Text);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBoxX3.Enabled = false;
            AC= ACbll.ReadAC(textBoxX3.Text);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Activity a = new Activity();
            a.Title = textBoxX5.Text;
            a.Info = richTextBox1.Text;
            a.RegDate=DateTime.Now;
             MsgBox.MyShowDialog("", Abll.Create(a, u, C, AC), "", false, false);
            if (checkBox1.Checked)
            {
                Reminder r = new Reminder();
                r.Title = textBoxX5.Text;
                r.ReminderInfo = richTextBox1.Text;
                r.RegDate = DateTime.Now;
                r.RemindDate = dateTimeInput1.Value;
                MsgBox.MyShowDialog("", Rbll.Create(r, u), "", false, false);

            }
            ShowDataGridview();
            ClearTextboxs();
            

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
label11_Click(sender, e);
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MsgBox.MyShowDialog("اخطار", "درصوردت حذف فعالیت تمام اطلاعات فعالیت حذف میشود \nآیا مطمئن هستید که میخواهید حذف کنید؟", "", true, true);

            if (dr == DialogResult.Yes)
            {
                // MessageBox.Show(bll.Delete(id));
                MsgBox.MyShowDialog("اطلاعیه", Abll.Delete(id), "", false, false);
                ShowDataGridview();
            }
        }

        private void نمایشجزئیاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activity a = Abll.Read(id);
            Information.ActivityInfo(a.Title, a.Info);

        }

        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }
    }
}
