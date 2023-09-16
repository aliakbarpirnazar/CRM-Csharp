using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Media.Effects;

namespace CRM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void OpenWinForm(Form f)
        {
            Window g= this.FindName("Main") as Window;
            BlurBitmapEffect blurBitmapEffect = new BlurBitmapEffect();
            blurBitmapEffect.Radius = 20;
            g.BitmapEffect = blurBitmapEffect;

            f.ShowDialog();
            blurBitmapEffect.Radius = 0;
            g.BitmapEffect=blurBitmapEffect;
        }

        private void BTNCustomer(object sender, MouseButtonEventArgs e)
        {
            CustomersForm f = new CustomersForm();
            OpenWinForm(f);
        }

        private void BTNProduct(object sender, MouseButtonEventArgs e)
        {
            ProductForm f = new ProductForm();
            OpenWinForm(f);

        }

        private void BTNInvoice(object sender, MouseButtonEventArgs e)
        {
            InvoiceForm f = new InvoiceForm();
            OpenWinForm(f);
        }

        

        private void BTNActivity(object sender, MouseButtonEventArgs e)
        {
            ActivityForm f = new ActivityForm();
            OpenWinForm(f);
        }

        private void BTNReminder(object sender, MouseButtonEventArgs e)
        {
            RemindersForm f = new RemindersForm();
            OpenWinForm(f);
        }

        private void BTNSMSPanel(object sender, MouseButtonEventArgs e)
        {
            SMSPanelForm f = new SMSPanelForm();
            OpenWinForm(f);
        }

        
        private void BTNExit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BTNUserManager(object sender, MouseButtonEventArgs e)
        {
            UserForm f = new UserForm();
            OpenWinForm(f);
        }

        private void BtnUserLogin(object sender, MouseButtonEventArgs e)
        {
            MsgBox m = new MsgBox();
            m.MyShowDialog("پیغام خطا", " اخطار تستی", "", false, true);
        }

        private void BTNSetting(object sender, MouseButtonEventArgs e)
        {
            SettingForm f = new SettingForm();
            OpenWinForm(f);
        }

        private void Lo(object sender, RoutedEventArgs e)
        {

        }

        private void Load_XML(object sender, RoutedEventArgs e)
        {
            LoginForm f = new LoginForm();
            OpenWinForm(f);
        }
    }
}
