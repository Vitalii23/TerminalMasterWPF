using System.Windows;

namespace TerminalMasterWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string ConnectionString { get; set; }

        public App()
        {
            InitializeComponent();
        }
    }
}
