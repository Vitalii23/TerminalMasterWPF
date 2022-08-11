using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TerminalMasterWPF.DML;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.ViewModel;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for CointersPageWindow.xaml
    /// </summary>
    public partial class CointersPageWindow : Window
    {
        private AddElement add = new AddElement();
        private UpdateElement update = new UpdateElement();
        private GetElement get = new GetElement();
        private LogFile logFile = new LogFile();
        private ObservableCollection<Printer> printer;

        public CointersPageWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            printer = get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);


            for (int i = 0; i < printer.Count; i++)
            {
                PrintersComboBox.Items.Add(printer[i].VendorCodePrinter);
            }

        }

        public string SelectData { get; set; }
        public int SelectIndex { get; set; }
        public int SelectId { get; set; }
        internal ObservableCollection<CountersPage> SelectCountersPage;

        public void AddComboxItem(string[] text, ComboBox combo)
        {
            for (int i = 0; i < text.Length; i++)
            {
                combo.Items.Add(text[i]);
            }
        }


        private void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string PrintersValue = (string)PrintersComboBox.SelectedValue;
                string[] countersPage = { CountersPageTextBox.Text, PrintersValue, DateDatePicker.Text};
                int[] Ids = new int[] { printer[PrintersComboBox.SelectedIndex].Id };

                if (SelectData.Equals("ADD"))
                {
                    add.AddDataElement((App.Current as App).ConnectionString, countersPage, Ids, "countersPage");
                }

                if (SelectData.Equals("UPDATE"))
                {
                    update.UpdateDataElement((App.Current as App).ConnectionString, countersPage, Ids, SelectId, "countersPage");
                }

                CountersPageTextBox.Text = string.Empty;
                Close();
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "CointersPage_ContentDialog_PrimaryButtonClick");
            }
            Close();
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            CountersPageTextBox.Text = string.Empty;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectData.Equals("GET"))
                {
                    CountersPageTextBox.Text = SelectCountersPage[SelectIndex].PrintedPageCounter;
                    PrintersComboBox.SelectedValue = SelectCountersPage[SelectIndex].Printers;
                    DateDatePicker.Text = SelectCountersPage[SelectIndex].Date.ToString();
                    SelectData = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "CointersPage_ContentDialog_Opened");
            }
        }
    }
}
