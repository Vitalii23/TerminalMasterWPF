using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TerminalMasterWPF.DML;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.ViewModel;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for CashRegisterWindows.xaml
    /// </summary>
    public partial class CashRegisterWindows : Window
    {
        //private AddElement add = new AddElement();
        private UpdateElement update = new UpdateElement();
        private OrderByElement orderBy = new OrderByElement();
        private LogFile logFile = new LogFile();
        private ObservableCollection<Holder> holders;
        private ObservableCollection<User> users;
        public CashRegisterWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            holders = orderBy.GetOrderByHolder("Ascending", "last_name");
            users = orderBy.GetOrderByUser("Ascending", "last_name");


            for (int i = 0; i < holders.Count; i++)
            {
                HolderComboBox.Items.Add(holders[i].LastName + " " + holders[i].FirstName + " " + holders[i].MiddleName);
            }

            for (int i = 0; i < users.Count; i++)
            {
                UserComboBox.Items.Add(users[i].LastName + " " + users[i].FirstName + " " + users[i].MiddleName);
            }

            string[] brand = { "AZUR", "MSPOS", "Атол FPrint-22ПТК", "Атол 55Ф" };
            AddComboxItem(brand, BrandComboBox);
        }
        public string SelectData { get; set; }
        public int SelectIndex { get; set; }
        public int SelectId { get; set; }
        internal ObservableCollection<CashRegister> SelectCashRegister;
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
                string brandValue = (string)BrandComboBox.SelectedValue;
                string[] cashRehisters = { NameTextBox.Text, brandValue, FactoryNumberTextBox.Text,
                SerialNumberTextBox.Text, PaymentNumberTextBox.Text, DateReceptionDatePicker.Text, 
                    DateKeyActivationFiscalDataOperatorDatePicker.Text, DateEndFiscalMemoryDatePicker.Text, LocationTextBox.Text};
                int[] Ids = new int[] { holders[HolderComboBox.SelectedIndex].Id, users[UserComboBox.SelectedIndex].Id };

                if (SelectData.Equals("ADD"))
                {

                    //add.AddDataElement((App.Current as App).ConnectionString, cashRehisters, Ids, "cashRegister");
                }

                if (SelectData.Equals("UPDATE"))
                {
                    update.UpdateDataElement((App.Current as App).ConnectionString, cashRehisters, Ids, SelectId, "cashRegister");
                }

                NameTextBox.Text = string.Empty;
                FactoryNumberTextBox.Text = string.Empty;
                SerialNumberTextBox.Text = string.Empty;
                PaymentNumberTextBox.Text = string.Empty;
                LocationTextBox.Text = string.Empty;
                Close();
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "CashRegister_ContentDialog_PrimaryButtonClick");
            }
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = string.Empty;
            FactoryNumberTextBox.Text = string.Empty;
            SerialNumberTextBox.Text = string.Empty;
            PaymentNumberTextBox.Text = string.Empty;
            LocationTextBox.Text = string.Empty;
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectData.Equals("GET"))
                {
                    NameTextBox.Text = SelectCashRegister[SelectIndex].NameDevice;
                    BrandComboBox.SelectedValue = SelectCashRegister[SelectIndex].Brand;
                    FactoryNumberTextBox.Text = SelectCashRegister[SelectIndex].FactoryNumber;
                    SerialNumberTextBox.Text = SelectCashRegister[SelectIndex].SerialNumber;
                    PaymentNumberTextBox.Text = SelectCashRegister[SelectIndex].PaymentNumber;
                    HolderComboBox.SelectedValue = SelectCashRegister[SelectIndex].Holder;
                    UserComboBox.SelectedValue = SelectCashRegister[SelectIndex].User;
                    DateReceptionDatePicker.Text = SelectCashRegister[SelectIndex].DateReception.ToString();
                    DateEndFiscalMemoryDatePicker.Text = SelectCashRegister[SelectIndex].DateEndFiscalMemory.ToString();
                    DateKeyActivationFiscalDataOperatorDatePicker.Text = SelectCashRegister[SelectIndex].DateKeyActivationFiscalDataOperator.ToString();
                    LocationTextBox.Text = SelectCashRegister[SelectIndex].Location;
                    SelectData = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "CashRegister_ContentDialog_Opened");
            }
        }
    }
}
