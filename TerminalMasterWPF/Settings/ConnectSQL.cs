using System.Data.SqlClient;

namespace TerminalMasterWPF.Settings
{
    class ConnectSQL
    {
        public void ConnectRead()
        {
            SqlConnectionStringBuilder bulder = new SqlConnectionStringBuilder
            {
                DataSource = SettingsUser.Default.DataSource,
                InitialCatalog = SettingsUser.Default.InitialCatalog,
                UserID = SettingsUser.Default.UserID,
                Password = SettingsUser.Default.Password,
                IntegratedSecurity = SettingsUser.Default.IntegratedSecurity,
                ConnectTimeout = SettingsUser.Default.ConnectTimeout,
                Encrypt = SettingsUser.Default.Encrypt,
                TrustServerCertificate = SettingsUser.Default.TrustServerCertificate,
                ApplicationIntent = ApplicationIntent.ReadWrite,
                MultiSubnetFailover = SettingsUser.Default.MultiSubnetFailover
            };
           (App.Current as App).ConnectionString = bulder.ConnectionString;
        }
    }
}
