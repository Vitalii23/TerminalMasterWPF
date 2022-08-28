using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TerminalMasterWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string ConnectionString { get { return "Data Source=DESKTOP-GIENFQV;Initial Catalog=TerminalMasterDB;Integrated Security=True"; } }

        public App()
        {
            InitializeComponent();
        }
    }
}
