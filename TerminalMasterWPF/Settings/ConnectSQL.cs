using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalMasterWPF.Settings
{
    class ConnectSQL
    {
        public void ConnectWrite(string connect)
        {
            //ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            //localSettings.Values["setting connect"] = "a device specific setting";

            //ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
            //composite["connectionString"] = connect;
            //localSettings.Values["Connect"] = composite;
        }

        public void ConnectRead()
        {
            (App.Current as App).ConnectionString = bulder.ConnectionString;
        }

        SqlConnectionStringBuilder bulder = new SqlConnectionStringBuilder
        {
            DataSource = @"tcp:192.168.0.223\SQL223",
            InitialCatalog = @"TerminalMasterDB",
            UserID = @"DVA",
            Password = @"Kolizey$",
            IntegratedSecurity = false,
            ConnectTimeout = 30,
            Encrypt = false,
            TrustServerCertificate = false,
            ApplicationIntent = ApplicationIntent.ReadWrite,
            MultiSubnetFailover = false
        };
    }
}
