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
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.Model.People;
using TerminalMasterWPF.ViewModel;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for SimCardWindows.xaml
    /// </summary>
    public partial class SimCardWindows : Window
    {
        private AddElement add = new AddElement();
        private UpdateElement update = new UpdateElement();
        private GetElement get = new GetElement();
        private LogFile logFile = new LogFile();
        private ObservableCollection<IndividualEntrepreneur> individuals;
        private ObservableCollection<CashRegister> cashRegisters;
        public SimCardWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            string[] @operator = { "Билайн", "МТС", "Мегафон", "Теле2", "Неизвестно" };
            AddComboxItem(@operator, OperatorComboBox);

            individuals = get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);
            for (int i = 0; i < individuals.Count; i++)
            {
                IndividualEntrepreneurComboBox.Items.Add(individuals[i].LastName + " " + individuals[i].FirstName + " " + individuals[i].MiddleName);
            }

            cashRegisters = get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);
            for (int i = 0; i < cashRegisters.Count; i++)
            {
                NameCashRegisterComboBox.Items.Add(cashRegisters[i].NameDevice);
            }

            string[] typeDevice = { "ККМ" };
            AddComboxItem(typeDevice, TypeDeviceComboBox);

            string[] status = { "Рабочий", "Нет симки-карты", "Замена", "Сервис", "Истек срок ФН", "Неизвестно" };
            AddComboxItem(status, StatusComboBox);
        }

        public string SelectData { get; set; }
        public int SelectIndex { get; set; }
        public int SelectId { get; set; }
        internal ObservableCollection<SimCard> SelectSimCard { get; set; }
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
                string @operator = (string)OperatorComboBox.SelectedValue;
                string status = (string)StatusComboBox.SelectedValue;
                string typeDevice = (string)TypeDeviceComboBox.SelectedValue;
                int[] Ids = new int[] { individuals[IndividualEntrepreneurComboBox.SelectedIndex].Id, cashRegisters[NameCashRegisterComboBox.SelectedIndex].Id };
                string[] simCards = { @operator, IdentNumberTextBox.Text, typeDevice,
                TmsMaskedTextBox.Text, IccTextBox.Text, status };

                if (SelectData.Equals("ADD")) { add.AddDataElement((App.Current as App).ConnectionString, simCards, Ids, "simCard"); }

                if (SelectData.Equals("UPDATE")) { update.UpdateDataElement((App.Current as App).ConnectionString, simCards, Ids, SelectId, "simCard"); }

                IdentNumberTextBox.Text = string.Empty;
                TmsMaskedTextBox.Text = string.Empty;
                IccTextBox.Text = string.Empty;

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "SimCard_ContentDialog_PrimaryButtonClick");
            }
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            IdentNumberTextBox.Text = string.Empty;
            TmsMaskedTextBox.Text = string.Empty;
            IccTextBox.Text = string.Empty;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectData.Equals("GET"))
                {
                    NameCashRegisterComboBox.SelectedValue = SelectSimCard[SelectIndex].NameTerminal;
                    OperatorComboBox.SelectedValue = SelectSimCard[SelectIndex].Operator;
                    IdentNumberTextBox.Text = SelectSimCard[SelectIndex].IdentNumber;
                    TypeDeviceComboBox.SelectedValue = SelectSimCard[SelectIndex].TypeDevice;
                    TmsMaskedTextBox.Text = SelectSimCard[SelectIndex].TMS;
                    IccTextBox.Text = SelectSimCard[SelectIndex].ICC;
                    IndividualEntrepreneurComboBox.SelectedValue = SelectSimCard[SelectIndex].IndividualEntrepreneur;
                    StatusComboBox.SelectedValue = SelectSimCard[SelectIndex].Status;
                    SelectData = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "SimCard_ContentDialog_Opened");
            }
        }
    }
}
