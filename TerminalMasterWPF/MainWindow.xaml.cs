using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
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
using System.Diagnostics;

namespace TerminalMasterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string NameNavigationItem;
        private readonly DataGets dataGets = new DataGets();
        private readonly ConnectSQL connect = new ConnectSQL();
        private readonly LogFile log = new LogFile();

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            connect.ConnectRead();
        }

        private async void UpdateTable<T>(string items, IList<T> list)
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
                        DataManipulationLanguage<Holder> holder = new DataManipulationLanguage<Holder>();
                        HolderGridViewDataColumn.ItemsSource = holder.GetHolders();
                        break;
                    case "simCard":
                        SimCardDataGrid.ItemsSource = list;
                        break;
                    case "employees":
                        EmployeesDataGrid.ItemsSource = list;
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
                await log.WriteLogAsync(ex.Message, "UpdateTable");
            }
        }

        private async void AddAndEditElement<T>(GridViewRowEditEndedEventArgs e, DataManipulationLanguage<T> element, string voidMessage) where T : class
        {
            try
            {
                if (e.EditAction == GridViewEditAction.Cancel)
                {
                    return;
                }

                if (e.EditOperationType == GridViewEditOperationType.Insert)
                {
                    if (e.NewData is T newElement)
                    {
                        element.Add(newElement);
                    }
                }

                if (e.EditOperationType == GridViewEditOperationType.Edit)
                {
                    if (e.NewData is T newElement)
                    {
                        element.Update(newElement);
                    }
                }

                UpdateTable(NameNavigationItem, element.List());
            }
            catch (Exception ex)
            {
                await log.WriteLogAsync(ex.Message, voidMessage);
            }
        }

        private async void DeleteElement<T>(RadGridView gridView, DataManipulationLanguage<T> element, string voidMessage) where T : class
        {
            try
            {
                if (gridView.SelectedItems.Count == 0)
                {
                    return;
                }

                List<T> itemsPrinterRemove = new List<T>();

                foreach (object item in gridView.SelectedItems)
                {
                    itemsPrinterRemove.Add(item as T);
                }

                foreach (T item in itemsPrinterRemove)
                {
                    ((List<T>)gridView.ItemsSource).Remove(item as T);
                    element.Delete(item);
                }

            }
            catch (Exception ex)
            {
                await log.WriteLogAsync(ex.Message, voidMessage);
            }
        }

        /// <summary>
        /// Event to Printer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrinterDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            AddAndEditElement(e, new DataManipulationLanguage<Printer>(), "PrinterDataGrid_RowEditEnded");
        }

        private void DeletePrinterRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(PrinterDataGrid, new DataManipulationLanguage<Printer>(), "DeletePrinterRadButton_Click");
        }

        /// <summary>
        /// Event Cointers Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountersPageDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            AddAndEditElement(e, new DataManipulationLanguage<CountersPage>(), "CountersPageDataGrid_RowEditEnded");
        }

        private void DeleteCounterPageRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(CountersPageDataGrid, new DataManipulationLanguage<CountersPage>(), "DeleteCounterPageRadButton_Click");
        }

        /// <summary>
        /// Event to Cartidge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartridgeDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            AddAndEditElement(e, new DataManipulationLanguage<Cartridge>(), "CartridgeDataGrid_RowEditEnded");
        }

        private void DeleteCartridgeRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(CartridgeDataGrid, new DataManipulationLanguage<Cartridge>(), "DeleteCartridgeRadButton_Click");
        }

        /// <summary>
        /// Event to CashRegister
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CashRegisterDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            AddAndEditElement(e, new DataManipulationLanguage<CashRegister>(), "CashRegisterDataGrid_RowEditEnded");
        }

        private void DeleteCashRegisterRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(CashRegisterDataGrid, new DataManipulationLanguage<CashRegister>(), "DeleteCashRegisterRadButton_Click");
        }

        /// <summary>
        /// Event to SimCard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimCardDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            AddAndEditElement(e, new DataManipulationLanguage<SimCard>(), "SimCardDataGrid_RowEditEnded");
        }

        private void DeleteSimCardRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(SimCardDataGrid, new DataManipulationLanguage<SimCard>(), "DeleteSimCardRadButton_Click");
        }

        /// <summary>
        /// Event to Employees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployeesDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        { 
            AddAndEditElement(e, new DataManipulationLanguage<Employees>(), "EmployeesDataGrid_RowEditEnded");
        }

        private void DeleteEmployeesRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(EmployeesDataGrid, new DataManipulationLanguage<Employees>(), "DeleteEmployeesRadButton_Click");
        }

        /// <summary>
        /// Event to Individual Entrepreneur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndividualEntrepreneurDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            AddAndEditElement(e, new DataManipulationLanguage<IndividualEntrepreneur>(), "IndividualEntrepreneurDataGrid_RowEditEnded");
        }

        private void DeleteIndividualEntrepreneurRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(IndividualEntrepreneurDataGrid, new DataManipulationLanguage<IndividualEntrepreneur>(), "DeleteIndividualEntrepreneurRadButton_Click");
        }

        /// <summary>
        /// Event to Waybill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaybillDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            AddAndEditElement(e, new DataManipulationLanguage<IndividualEntrepreneur>(), "WaybillDataGrid_RowEditEnded");
        }

        private void DeleteWaybillRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(WaybillDataGrid, new DataManipulationLanguage<IndividualEntrepreneur>(), "DeleteWaybillRadButton_Click");
        }

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
                            UpdateTable(NameNavigationItem, cashRegister.GetCashRegistersList());
                            break;
                        case "Sim-карты":
                            DataManipulationLanguage<SimCard> simCard = new DataManipulationLanguage<SimCard>();
                            NameNavigationItem = "simCard";
                            UpdateTable(NameNavigationItem, simCard.List());
                            break;
                        case "Сотрудники":
                            DataManipulationLanguage<Employees> employees = new DataManipulationLanguage<Employees>();
                            NameNavigationItem = "employees";
                            UpdateTable(NameNavigationItem, employees.List());
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
                log.WriteLogAsync(ex.Message, "MainTabControl_SelectionChanged");
            }
        }

        /// <summary>
        /// Click Button and Check
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
            EmployeesDataGrid.Columns.Clear();
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
               // if (result == true && dataGets.SelectedXIndex >= 0)
               // {
                    BinaryFormatter binaryformatter = new BinaryFormatter();
                    MemoryStream memorystream = new MemoryStream();
                   // binaryformatter.Serialize(memorystream, dataGets.WaybillList[dataGets.SelectedXIndex].FilePDF);
                    byte[] data = memorystream.ToArray();

                    using (FileStream fileStream = File.Create(saveFileDialog.FileName))
                    {
                        fileStream.Write(data, 0, data.Length);
                    }

                    MessageBox.Show("Успешно скачанно", "Скачивание", MessageBoxButton.OK, MessageBoxImage.Information);
              //  }
            //    else
             //   {
                    MessageBox.Show("Выберите строку для скачивания", "Скачивание", MessageBoxButton.OK, MessageBoxImage.Warning);
           //     }

            }
            catch (Exception ex)
            {
                log.WriteLogAsync(ex.Message, "DowloandButton_Click");
            }
        }

        private void SettingsDataBase_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindows connectWindows = new ConnectWindows();
            connectWindows.ShowDialog();
        }
    }
}