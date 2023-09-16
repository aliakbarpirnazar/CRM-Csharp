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
using Stimulsoft.Report;

namespace CRM
{
    public partial class InvoiceForm : Form
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
        public InvoiceForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }


        CustomerBLL Cbll = new CustomerBLL();
        Customer C = new Customer();
        ProductBLL Pbll = new ProductBLL();
        Product p = new Product();
        List<Product> Products = new List<Product>();
        MsgBox msg = new MsgBox();
        InvoiceBLL Ibll = new InvoiceBLL();
        int id,id_c;
        void FillDataGridviewX2()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = Products;
            dataGridViewX2.Columns["id"].Visible = false;
            dataGridViewX2.Columns["DeleteStatus"].Visible = false;
            dataGridViewX2.Columns["Stock"].Visible = false;
            dataGridViewX2.Columns["Name"].HeaderText = "نام محصول";
            dataGridViewX2.Columns["Price"].HeaderText = "قیمت";
        }
        void FillDataGridviewX1()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Ibll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;
        }
        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            pictureBox3_Click(sender, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxX1.Enabled = false;
            C = Cbll.ReadC(textBoxX1.Text);
            label14.Text = C.Name;
            label16.Text = C.Phone;
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            FillDataGridviewX2();
            FillDataGridviewX1();
            AutoCompleteStringCollection Phone = new AutoCompleteStringCollection();
            foreach (var item in Cbll.ReadPhone())
            {
                Phone.Add(item);
            }
            //foreach (var item in Cbll.ReadName())
            //{
            //    Phone.Add(item);
            //}
            textBoxX1.AutoCompleteCustomSource = Phone;
            label18.Text = DateTime.Now.ToString("yyyy/MM/dd");
            // add item
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in Pbll.ReadNames())
            {
                names.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = names;
            label18.Text = DateTime.Now.ToString("yyyy/MM/dd");

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            p = Pbll.ReadN(textBoxX2.Text);
            Products.Add(p);
            FillDataGridviewX2();
            textBoxX2.Text = "";
            string s = p.Name + "به ارزش  " + p.Price.ToString("N0") + " ریال";
            listBox1.Items.Add(s);
            double sum=0;
            foreach (var item in Products)
            {
                sum = sum + item.Price;
            }
            label23.Text =sum.ToString("N0");
            label24.Text =(sum-Convert.ToDouble(textBoxX3.Text)).ToString("N0");
            

        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            label24.Text = (Convert.ToDouble(label23.Text) - Convert.ToDouble(textBoxX3.Text)).ToString("N0");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            label2_Click(sender, e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Invoice i =     new Invoice();
            i.RegDate= DateTime.Now;
            if (checkBox1.Checked)
            {
                i.IsCheckedout = true;
                i.CheckoutDate= DateTime.Now;
            }
            else
            {
                i.IsCheckedout= false;
            }
            DialogResult res = msg.MyShowDialog("اطلاعیه", Ibll.Create(i,C,Products)+"آیا قصد چاپ فاکتور را دارید؟", "", true, false) ;
            if(res == DialogResult.Yes)
            {
                StiReport sti = new StiReport();
                sti.Load(@"C:\Users\Aliakbar\Desktop\invoice.mrt");
                sti.Dictionary.Variables["InvoiceNumber"].Value = Ibll.ReadInvoiceNum();
                sti.Dictionary.Variables["Date"].Value =label18.Text;
                sti.Dictionary.Variables["CustName"].Value = label14.Text;
                sti.Dictionary.Variables["CustNum"].Value = label16.Text;
                sti.RegBusinessObject("Product",Products);
                sti.Render();
                sti.Show();
            }
            FillDataGridviewX1();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
          
        }

        private void پرداختشدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = msg.MyShowDialog("اخطار", "آیا مطمئن هستید که پرداخت شد؟", "", true, false);

            if (dr == DialogResult.Yes)
            {
                // MessageBox.Show(bll.Delete(id));
                msg.MyShowDialog("اطلاعیه", Ibll.IschechedOut(id), "", false, false);
                FillDataGridviewX1();
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = msg.MyShowDialog("اخطار", "درصوردت حذف فاکتور تمام اطلاعات فاکتور حذف میشود \nآیا مطمئن هستید که میخواهید حذف کنید؟", "", true, true);

            if (dr == DialogResult.Yes)
            {
                // MessageBox.Show(bll.Delete(id));
                msg.MyShowDialog("اطلاعیه", Ibll.Delete(id), "", false, false);
                FillDataGridviewX1();
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void چاپفاکتورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoice i = new Invoice();
            i.RegDate = DateTime.Now;
            if (checkBox1.Checked)
            {
                i.IsCheckedout = true;
                i.CheckoutDate = DateTime.Now;
            }
            else
            {
                i.IsCheckedout = false;
            }
            DialogResult res = msg.MyShowDialog("اطلاعیه", "آیا قصد چاپ فاکتور را دارید؟", "", true, false);
            if (res == DialogResult.Yes)
            {
                StiReport sti = new StiReport();
                sti.Load(@"C:\Users\Aliakbar\Desktop\invoice.mrt");
                sti.Dictionary.Variables["InvoiceNumber"].Value = Ibll.PrintInvoice(id);
                sti.Dictionary.Variables["Date"].Value = label18.Text;
                sti.Dictionary.Variables["CustName"].Value = label14.Text;
                sti.Dictionary.Variables["CustNum"].Value = label16.Text;
                sti.RegBusinessObject("Product", Products);
                sti.Render();
                sti.Show();
            }
        }

        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
    }
}
