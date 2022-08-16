using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using TerminalMasterWPF.DML;
using TerminalMasterWPF.ElementContentDialog;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Settings;
using TerminalMasterWPF.ViewModel;
using TerminalMasterWPF.Model;
using Microsoft.Win32;
using System.Collections.Generic;
using TerminalMasterWPF.Model.People;
using System.Collections.ObjectModel;

namespace TerminalMasterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string NameNavigationItem, CheckASCorDesc = null, CheckTag = null;
        private readonly DataGets dataGets = new DataGets();
        private ConnectSQL connect = new ConnectSQL();
        private LogFile logFile = new LogFile();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            connect.ConnectRead();
        }

        private void UpdateTable<T>(string items, IList<T> list)
        {
            try
            {
                switch (items)
                {
                    case "printer":
                        PrinterDataGrid.ItemsSource = list;
                        break;
                    case "cartrides":
                        CartridgeDataGrid.ItemsSource = list;
                        break;
                    case "cashRegister":
                        CashRegisterDataGrid.ItemsSource = list;
                        break;
                    case "simCard":
                        SimCardDataGrid.ItemsSource = list;
                        break;
                    case "phoneBook":
                        PhoneBookDataGrid.ItemsSource = list;
                        break;
                    case "holder":
                        HolderDataGrid.ItemsSource = list;
                        break;
                    case "user":
                        UserDataGrid.ItemsSource = list;
                        break;
                    case "ie":
                        IndividualEntrepreneurDataGrid.ItemsSource = list;
                        break;
                    case "waybill":
                        WaybillDataGrid.ItemsSource = list;
                        break;
                    case "countersPage":
                        WaybillDataGrid.ItemsSource = list;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "UpdateTable");
            }
        }

        /// <summary>
        /// Event to Printer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrinterDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            DataManipulationLanguage<Printer> printer = new DataManipulationLanguage<Printer>();
            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }

            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                if (e.NewData is Printer newPrinter)
                {
                    printer.Add(newPrinter);
                }
            }

            if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                if (e.NewData is Printer newPrinter)
                {
                    printer.Update(newPrinter);
                }
            }

            UpdateTable(NameNavigationItem, printer.List());
        }

        private void DeletePrinterRadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataManipulationLanguage<Printer> printer = new DataManipulationLanguage<Printer>();
                if (PrinterDataGrid.SelectedItems.Count == 0)
                {
                    return;
                }

                List<Printer> itemsPrinterRemove = new List<Printer>();

                foreach (var item in PrinterDataGrid.SelectedItems)
                {
                    itemsPrinterRemove.Add(item as Printer);
                }

                foreach (var item in itemsPrinterRemove)
                {
                    ((List<Printer>)PrinterDataGrid.ItemsSource).Remove(item as Printer);
                    printer.Delete(item);
                }

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "DeletePrinterRadButton_Click");
            }

        }

        /// <summary>
        /// Event Cointers Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event to Cartidge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event to CashRegister
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event to SimCard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event to PhoneBook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event to Holder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event to User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event to Individual Entrepreneur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event to Waybill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Event Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;
            try
            {
                if (ConnectItem.Header.Equals("Подключено") && ConnectItem.IsChecked)
                {
                    switch (tabItem)
                    {
                        case "Принтеры":
                            DataManipulationLanguage<Printer> printer = new DataManipulationLanguage<Printer>();
                            NameNavigationItem = "printer";
                            UpdateTable(NameNavigationItem, printer.List());
                            break;
                        case "Картриджи":
                            DataManipulationLanguage<Cartridge> cartridge = new DataManipulationLanguage<Cartridge>();
                            NameNavigationItem = "cartrides";
                            UpdateTable(NameNavigationItem, cartridge.List());
                            break;
                        case "Контрольная-кассовая машина (ККМ)":
                            DataManipulationLanguage<CashRegister> cashRegister = new DataManipulationLanguage<CashRegister>();
                            NameNavigationItem = "cashRegister";
                            UpdateTable(NameNavigationItem, cashRegister.List());
                            break;
                        case "Sim-карты":
                            DataManipulationLanguage<SimCard> simCard = new DataManipulationLanguage<SimCard>();
                            NameNavigationItem = "simCard";
                            UpdateTable(NameNavigationItem, simCard.List());
                            break;
                        case "Телефоны сотрудников":
                            DataManipulationLanguage<PhoneBook> phoneBook = new DataManipulationLanguage<PhoneBook>();
                            NameNavigationItem = "phoneBook";
                            UpdateTable(NameNavigationItem, phoneBook.List());
                            break;
                        case "Владельцы":
                            DataManipulationLanguage<Holder> holder = new DataManipulationLanguage<Holder>();
                            NameNavigationItem = "holder";
                            UpdateTable(NameNavigationItem, holder.List());
                            break;
                        case "Пользователи":
                            DataManipulationLanguage<User> user = new DataManipulationLanguage<User>();
                            NameNavigationItem = "user";
                            UpdateTable(NameNavigationItem, user.List());
                            break;
                        case "Индивидуальные предприниматели":
                            DataManipulationLanguage<IndividualEntrepreneur> ie = new DataManipulationLanguage<IndividualEntrepreneur>();
                            NameNavigationItem = "ie";
                            UpdateTable(NameNavigationItem, ie.List());
                            break;
                        case "Накладные":
                            DataManipulationLanguage<Waybill> waybill = new DataManipulationLanguage<Waybill>();
                            NameNavigationItem = "waybill";
                            UpdateTable(NameNavigationItem, waybill.List());
                            break;
                        case "Счетчик распечатанных страниц":
                            DataManipulationLanguage<CountersPage> counterPage = new DataManipulationLanguage<CountersPage>();
                            NameNavigationItem = "countersPage";
                            UpdateTable(NameNavigationItem, counterPage.List());
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "UpdateTable");
            }
        }

        /// <summary>
        /// Click Button Add, Edit, Update, Delete, Dowloand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ConnectItem_Checked(object sender, RoutedEventArgs e)
        {
            ConnectItem.Header = "Подключено";
            MainTabControl.IsEnabled = true;
            MessageBox.Show(ConnectItem.Header.ToString(), "Настройка подключение базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ConnectItem_Unchecked(object sender, RoutedEventArgs e)
        {
            ConnectItem.Header = "Отключено";
            MessageBox.Show(ConnectItem.Header.ToString(), "Настройка подключение базы данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            MainTabControl.IsEnabled = false;
            CartridgeDataGrid.Columns.Clear();
            CashRegisterDataGrid.Columns.Clear();
            IndividualEntrepreneurDataGrid.Columns.Clear();
            HolderDataGrid.Columns.Clear();
            UserDataGrid.Columns.Clear();
            PhoneBookDataGrid.Columns.Clear();
            PrinterDataGrid.Columns.Clear();
            SimCardDataGrid.Columns.Clear();
            WaybillDataGrid.Columns.Clear();
        }

        private void ConnectItem_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectItem.IsChecked)
            {
                ConnectItem.IsChecked = false;
            }
            else
            {
                ConnectItem.IsChecked = true;
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти", "Выход из программы", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void deleteCountersRadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteCaRadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteCartridgeRadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DowloandButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    FilterIndex = 4,
                    Filter = "File image (*.jpeg)|*.jpeg|" +
                    "File image (*.jpg)|*.jpg|" +
                    "File image (*.png)|*.png|" +
                    "Documents file (*.pdf)|*.pdf",
                    RestoreDirectory = true
                };

                bool? result = saveFileDialog.ShowDialog();
                if (result == true && dataGets.SelectedXIndex >= 0)
                {
                    BinaryFormatter binaryformatter = new BinaryFormatter();
                    MemoryStream memorystream = new MemoryStream();
                    binaryformatter.Serialize(memorystream, dataGets.WaybillList[dataGets.SelectedXIndex].FilePDF);
                    byte[] data = memorystream.ToArray();

                    using (FileStream fileStream = File.Create(saveFileDialog.FileName))
                    {
                        fileStream.Write(data, 0, data.Length);
                    }

                    MessageBox.Show("Успешно скачанно", "Скачивание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выберите строку для скачивания", "Скачивание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "DowloandButton_Click");
            }
        }

        private void SettingsDataBase_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindows connectWindows = new ConnectWindows();
            connectWindows.ShowDialog();
        }
    }
}