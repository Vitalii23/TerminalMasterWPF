using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TerminalMasterWPF.Settings;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for ConnectWindows.xaml
    /// </summary>
    public partial class ConnectWindows : Window
    {
        SettingsUser user = new SettingsUser();
        ConnectSQL connect = new ConnectSQL();
        public ConnectWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            user.DataSource = DataSourceTextBox.Text;
            user.InitialCatalog = InitialCatalogTextBox.Text;
            user.UserID = UserIDTextBox.Text;
            user.Password = PasswordBox.Password;
            user.IntegratedSecurity = IntegratedSecurityCheckBox.IsChecked.Value;
            user.ConnectTimeout = Convert.ToInt16(ConnectTimeoutTextBox.Text);
            user.Encrypt = EncryptCheckBox.IsChecked.Value;
            user.TrustServerCertificate = TrustServerCertificateCheckBox.IsChecked.Value;
            user.MultiSubnetFailover = MultiSubnetFailoverCheckBox.IsChecked.Value;
            user.Save();
            Close();
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataSourceTextBox.Text = user.DataSource;
            InitialCatalogTextBox.Text = user.InitialCatalog;
            UserIDTextBox.Text = user.UserID;
            PasswordBox.Password = user.Password;
            IntegratedSecurityCheckBox.IsChecked = user.IntegratedSecurity;
            ConnectTimeoutTextBox.Text = user.ConnectTimeout.ToString();
            EncryptCheckBox.IsChecked = user.Encrypt;
            TrustServerCertificateCheckBox.IsChecked = user.TrustServerCertificate;
            MultiSubnetFailoverCheckBox.IsChecked = user.MultiSubnetFailover;
        }
    }
}
