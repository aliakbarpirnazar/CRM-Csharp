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
    public partial class UserForm : Form
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
        public UserForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        OpenFileDialog ofd = new OpenFileDialog();
        Image pic;
        UserBLL bll = new UserBLL();
        UserGroupBLL UGbll = new UserGroupBLL();
        MsgBox MsgBox = new MsgBox();
        string Savepic(string UserName)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\UserPic\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string PicName = UserName + ".JPG";
            try
            {
                string PicPath = ofd.FileName;
                File.Copy(PicPath, path+PicName,true);
            }
            catch (Exception e)
            {
                MessageBox.Show("سیستم قادر به ذخیره عکس نمیباشد \n" + e.Message);
               
            }
            return path + PicName;
        }
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
            textBoxX3.Text = "";
            textBoxX4.Text = "";
            textBoxX5.Text = "";
        }
        UserAccessRole FillAccessRole(string Section,bool CanEnter,bool CanCreate,bool CanUpdate,bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section= Section;
            uar.CanEnter= CanEnter;
            uar.CanCreate= CanCreate;
            uar.CanUpdate= CanUpdate;
            uar.CanDelete= CanDelete;
            return uar;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG | JPEG(*.JPEG)|*.JPEG";
            
            ofd.Title = "تصویر کاربر را انتخاب کنید";
            
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                pic = Image.FromFile(ofd.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.Image = pic;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Name = textBoxX1.Text;
            u.UserName = textBoxX2.Text;
            u.RegDate = DateTime.Now;
            if (textBoxX3.Text == textBoxX4.Text)
                u.Password = textBoxX4.Text;
            else
            {
                MessageBox.Show("تکرار کلمه عبور درست وارد نشده است");
                textBoxX3.Text = "";
                textBoxX4.Text = "";
            }
            u.Pic = Savepic(textBoxX2.Text);
            if (label1.Text == "ویرایش اطلاعات")
            {
                MessageBox.Show(bll.Update(u, id));
                label1.Text = "ثبت اطلاعات";
            }
            else if (label1.Text == "ثبت اطلاعات")
            {
                MessageBox.Show(bll.Create(u));
            }
            ShowDataGridview();
            ClearTextboxs();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label1_Click(sender, e);
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User u = bll.Read(id);
            textBoxX1.Text = u.Name;
            textBoxX2.Text = u.UserName;
            label1.Text = "ویرایش اطلاعات";
            pictureBox2.Image = Image.FromFile(u.Pic);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("آیا مطمعن هستید میخواهید کاربر را حذف کنید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show(bll.Delete(id));
                ShowDataGridview();
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            ShowDataGridview();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void line1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (checkBox24.Checked && checkBox23.Checked && checkBox22.Checked && checkBox21.Checked)
            {
                checkBox24.Checked = false;
                checkBox23.Checked = false;
                checkBox22.Checked = false;
                checkBox21.Checked = false;
            }
            else
            {
                checkBox24.Checked = true;
                checkBox23.Checked = true;
                checkBox22.Checked = true;
                checkBox21.Checked = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox8.Checked = true;
                checkBox12.Checked = true;
                checkBox16.Checked = true;
                checkBox20.Checked = true;
                checkBox24.Checked = true;
                checkBox28.Checked = true;
                checkBox32.Checked = true;
                checkBox36.Checked = true;
                checkBox40.Checked = true;
            }
            else
            {
                checkBox8.Checked = false;
                checkBox12.Checked = false;
                checkBox16.Checked = false;
                checkBox20.Checked = false;
                checkBox24.Checked = false;
                checkBox28.Checked = false;
                checkBox32.Checked = false;
                checkBox36.Checked = false;
                checkBox40.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox7.Checked = true;
                checkBox11.Checked = true;
                checkBox15.Checked = true;
                checkBox19.Checked = true;
                checkBox23.Checked = true;
                checkBox27.Checked = true;
                checkBox31.Checked = true;
                checkBox35.Checked = true;
                checkBox39.Checked = true;
            }
            else
            {
                checkBox7.Checked= false;
                checkBox11.Checked= false;
                checkBox15.Checked= false;
                checkBox19.Checked= false;
                checkBox23.Checked= false;
                checkBox27.Checked= false;
                checkBox31.Checked= false;
                checkBox35.Checked= false;
                checkBox39.Checked= false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox6.Checked = true;
                checkBox10.Checked = true;
                checkBox14.Checked = true;
                checkBox18.Checked = true;
                checkBox22.Checked = true;
                checkBox26.Checked = true;
                checkBox30.Checked = true;
                checkBox34.Checked = true;
                checkBox38.Checked = true;
            }
            else
            {
                checkBox6.Checked = false;
                checkBox10.Checked = false;
                checkBox14.Checked = false;
                checkBox18.Checked = false;
                checkBox22.Checked = false;
                checkBox26.Checked = false;
                checkBox30.Checked = false;
                checkBox34.Checked = false;
                checkBox38.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox5.Checked = true;
                checkBox9.Checked = true;
                checkBox13.Checked = true;
                checkBox17.Checked = true;
                checkBox21.Checked = true;
                checkBox25.Checked = true;
                checkBox29.Checked = true;
                checkBox33.Checked = true;
                checkBox37.Checked = true;
            }
            else
            {
                checkBox5.Checked = false;
                checkBox9.Checked = false;
                checkBox13.Checked = false;
                checkBox17.Checked = false;
                checkBox21.Checked = false;
                checkBox25.Checked = false;
                checkBox29.Checked = false;
                checkBox33.Checked = false;
                checkBox37.Checked = false;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if(checkBox8.Checked && checkBox7.Checked && checkBox6.Checked && checkBox5.Checked)
            {
                checkBox8.Checked = false;
                checkBox7.Checked = false;
                checkBox6.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                checkBox8.Checked = true;
                checkBox7.Checked = true;
                checkBox6.Checked = true;
                checkBox5.Checked = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && checkBox4.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (checkBox12.Checked && checkBox11.Checked && checkBox10.Checked && checkBox9.Checked)
            {
                checkBox12.Checked = false;
                checkBox11.Checked = false;
                checkBox10.Checked = false;
                checkBox9.Checked = false;
            }
            else
            {
                checkBox12.Checked = true;
                checkBox11.Checked = true;
                checkBox10.Checked = true;
                checkBox9.Checked = true;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (checkBox16.Checked && checkBox15.Checked && checkBox14.Checked && checkBox13.Checked)
            {
                checkBox16.Checked = false;
                checkBox15.Checked = false;
                checkBox14.Checked = false;
                checkBox13.Checked = false;
            }
            else
            {
                checkBox16.Checked = true;
                checkBox15.Checked = true;
                checkBox14.Checked = true;
                checkBox13.Checked = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (checkBox20.Checked && checkBox19.Checked && checkBox18.Checked && checkBox17.Checked)
            {
                checkBox20.Checked = false;
                checkBox19.Checked = false;
                checkBox18.Checked = false;
                checkBox17.Checked = false;
            }
            else
            {
                checkBox20.Checked = true;
                checkBox19.Checked = true;
                checkBox18.Checked = true;
                checkBox17.Checked = true;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (checkBox28.Checked && checkBox27.Checked && checkBox26.Checked && checkBox25.Checked)
            {
                checkBox28.Checked = false;
                checkBox27.Checked = false;
                checkBox26.Checked = false;
                checkBox25.Checked = false;
            }
            else
            {
                checkBox28.Checked = true;
                checkBox27.Checked = true;
                checkBox26.Checked = true;
                checkBox25.Checked = true;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            if (checkBox32.Checked && checkBox31.Checked && checkBox30.Checked && checkBox29.Checked)
            {
                checkBox32.Checked = false;
                checkBox31.Checked = false;
                checkBox30.Checked = false;
                checkBox29.Checked = false;
            }
            else
            {
                checkBox32.Checked = true;
                checkBox31.Checked = true;
                checkBox30.Checked = true;
                checkBox29.Checked = true;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            if (checkBox36.Checked && checkBox35.Checked && checkBox34.Checked && checkBox33.Checked)
            {
                checkBox36.Checked = false;
                checkBox35.Checked = false;
                checkBox34.Checked = false;
                checkBox33.Checked = false;
            }
            else
            {
                checkBox36.Checked = true;
                checkBox35.Checked = true;
                checkBox34.Checked = true;
                checkBox33.Checked = true;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            if (checkBox40.Checked && checkBox39.Checked && checkBox38.Checked && checkBox37.Checked)
            {
                checkBox40.Checked = false;
                checkBox39.Checked = false;
                checkBox38.Checked = false;
                checkBox37.Checked = false;
            }
            else
            {
                checkBox40.Checked = true;
                checkBox39.Checked = true;
                checkBox38.Checked = true;
                checkBox37.Checked = true;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            label2_Click(sender, e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            UserGroup ug = new UserGroup();
            ug.Title = textBoxX10.Text;
            ug.UserAccessRoles.Add(FillAccessRole(label4.Text, checkBox8.Checked, checkBox7.Checked, checkBox6.Checked, checkBox5.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label5.Text, checkBox12.Checked, checkBox11.Checked, checkBox10.Checked, checkBox9.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label8.Text, checkBox16.Checked, checkBox15.Checked, checkBox14.Checked, checkBox13.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label6.Text, checkBox20.Checked, checkBox19.Checked, checkBox18.Checked, checkBox17.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label10.Text, checkBox24.Checked, checkBox23.Checked, checkBox22.Checked, checkBox21.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label9.Text, checkBox28.Checked, checkBox27.Checked, checkBox26.Checked, checkBox25.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label12.Text, checkBox32.Checked, checkBox31.Checked, checkBox30.Checked, checkBox29.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label11.Text, checkBox36.Checked, checkBox35.Checked, checkBox34.Checked, checkBox33.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label14.Text, checkBox40.Checked, checkBox39.Checked, checkBox38.Checked, checkBox37.Checked));
            MsgBox.MyShowDialog("نتیجه ثبت اطلاعات", UGbll.Create(ug), "",false,false);
            
           }
    }
}
