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
using System.Windows.Data;

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
        public static NotNullDateConverter NotNullDateConverter = new NotNullDateConverter();

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            MainTabControl.IsEnabled = false;
            CartridgeDataGrid.IsEnabled = false;
            CashRegisterDataGrid.IsEnabled = false;
            IndividualEntrepreneurDataGrid.IsEnabled = false;
            EmployeesDataGrid.IsEnabled = false;
            PrinterDataGrid.IsEnabled = false;
            SimCardDataGrid.IsEnabled = false;
            DocumentsDataGrid.IsEnabled = false;

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
                        HolderGridViewDataColumn.ItemsSource = dataGets.HolderList;
                        HolderGridViewDataColumn.DisplayMemberPath = "FullNameHolder";
                        HolderGridViewDataColumn.DataContext = "Holder";
                        HolderGridViewDataColumn.SelectedValueMemberPath = "Id";
                        HolderGridViewDataColumn.DataMemberBinding = new Binding("IdEmployees");
                        break;
                    case "simCard":
                        SimCardDataGrid.ItemsSource = list;
                        NameTerminalGridViewComboBoxColumn.ItemsSource = dataGets.CashRegisterList;
                        NameTerminalGridViewComboBoxColumn.DisplayMemberPath = "NameDevice";
                        NameTerminalGridViewComboBoxColumn.DataContext = "CashRegister";
                        NameTerminalGridViewComboBoxColumn.SelectedValueMemberPath = "Id";
                        NameTerminalGridViewComboBoxColumn.DataMemberBinding = new Binding("IdCashRegister");

                        TypeDeviceGridViewComboBoxColumn.ItemsSource = new string[] { "Автономные кассовые аппараты", "Фискальные регистраторы", "POS-терминал", "ЧПМ (МЭР)" };
                        TypeDeviceGridViewComboBoxColumn.DataMemberBinding = new Binding("TypeDevice");

                        BrandSimCardGridViewComboBoxColumn.ItemsSource = new string[] { "AZUR", "MSPOS" };
                        BrandSimCardGridViewComboBoxColumn.DataMemberBinding = new Binding("Brand");

                        IndividualEntrepreneurGridViewComboBoxColumn.ItemsSource = dataGets.IndividualList;
                        IndividualEntrepreneurGridViewComboBoxColumn.DisplayMemberPath = "FullNameIndividualEntrepreneur";
                        IndividualEntrepreneurGridViewComboBoxColumn.DataContext = "IndividualEntrepreneur";
                        IndividualEntrepreneurGridViewComboBoxColumn.SelectedValueMemberPath = "Id";
                        IndividualEntrepreneurGridViewComboBoxColumn.DataMemberBinding = new Binding("IdIndividual");

                        OperatorGridViewComboBoxColumn.ItemsSource = new string[] { "Билайн", "MTC", "Мегафон", "Теле2", "Неизвестно" };
                        OperatorGridViewComboBoxColumn.DataMemberBinding = new Binding("Operator");

                        StatusSimCardGridViewComboBoxColumn.ItemsSource = new string[] { "Работает", "Нет симкарты", "Неизвестно", "Списан", "Истек срок ФН" };
                        StatusSimCardGridViewComboBoxColumn.DataMemberBinding = new Binding("Status");
                        break;
                    case "employees":
                        EmployeesDataGrid.ItemsSource = list;
                        StatusEmployeesGridViewComboBoxColumn.ItemsSource = new string[] { "Работает", "Уволен", "Неизвестно", "Стажировка" };
                        StatusEmployeesGridViewComboBoxColumn.DataMemberBinding = new Binding("Status");
                        break;
                    case "ie":
                        IndividualEntrepreneurDataGrid.ItemsSource = list;
                        break;
                    case "Documents":
                        DocumentsDataGrid.ItemsSource = list;
                        break;
                    case "countersPage":
                        DocumentsDataGrid.ItemsSource = list;
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
        /// Event to Documents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentsDataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            AddAndEditElement(e, new DataManipulationLanguage<IndividualEntrepreneur>(), "DocumentsDataGrid_RowEditEnded");
        }

        private void DeleteDocumentsRadButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteElement(DocumentsDataGrid, new DataManipulationLanguage<IndividualEntrepreneur>(), "DeleteDocumentsRadButton_Click");
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
                            NameNavigationItem = "printer";
                            UpdateTable(NameNavigationItem, dataGets.PrinterList);
                            break;
                        case "Картриджи":
                            NameNavigationItem = "cartrides";
                            UpdateTable(NameNavigationItem, dataGets.CartridgesList);
                            break;
                        case "Контрольная-кассовая машина (ККМ)":
                            NameNavigationItem = "cashRegister";
                            UpdateTable(NameNavigationItem, dataGets.CashRegisterList);
                            break;
                        case "Sim-карты":
                            NameNavigationItem = "simCard";
                            UpdateTable(NameNavigationItem, dataGets.SimCardList);
                            break;
                        case "Сотрудники":
                            NameNavigationItem = "employees";
                            UpdateTable(NameNavigationItem, dataGets.EmployessList);
                            break;
                        case "Индивидуальные предприниматели":
                            NameNavigationItem = "ie";
                            UpdateTable(NameNavigationItem, dataGets.IndividualList);
                            break;
                        case "Накладные": 
                            NameNavigationItem = "Documents";
                            UpdateTable(NameNavigationItem, dataGets.DocumentsList);
                            break;
                        case "Счетчик распечатанных страниц":
                            NameNavigationItem = "countersPage";
                            UpdateTable(NameNavigationItem, dataGets.CountersPagesList);
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
            CartridgeDataGrid.IsEnabled = true;
            CashRegisterDataGrid.IsEnabled = true;
            IndividualEntrepreneurDataGrid.IsEnabled = true;
            EmployeesDataGrid.IsEnabled = true;
            PrinterDataGrid.IsEnabled = true;
            SimCardDataGrid.IsEnabled = true;
            DocumentsDataGrid.IsEnabled = true;

            DataManipulationLanguage<Printer> printer = new DataManipulationLanguage<Printer>();
            DataManipulationLanguage<Cartridge> cartridge = new DataManipulationLanguage<Cartridge>();
            DataManipulationLanguage<CashRegister> cashRegister = new DataManipulationLanguage<CashRegister>();
            DataManipulationLanguage<SimCard> simCard = new DataManipulationLanguage<SimCard>();
            DataManipulationLanguage<Employees> employees = new DataManipulationLanguage<Employees>();
            DataManipulationLanguage<IndividualEntrepreneur> ie = new DataManipulationLanguage<IndividualEntrepreneur>();
            //DataManipulationLanguage<Documents> Documents = new DataManipulationLanguage<Documents>();
           // DataManipulationLanguage<CountersPage> counterPage = new DataManipulationLanguage<CountersPage>();
            DataManipulationLanguage<Holder> holder = new DataManipulationLanguage<Holder>();

            dataGets.PrinterList = printer.List();
            dataGets.CartridgesList = cartridge.List();
            dataGets.CashRegisterList = cashRegister.GetCashRegistersList();
            dataGets.SimCardList = simCard.GetSimCardList();
            dataGets.EmployessList = employees.List();
            dataGets.IndividualList = ie.GetIndividualEntrepreneur();
            //dataGets.DocumentsList = Documents.List();
           // dataGets.CountersPagesList = counterPage.List();
            dataGets.HolderList = holder.GetHolderList();

            MessageBox.Show(ConnectItem.Header.ToString(), "Настройка подключение базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ConnectItem_Unchecked(object sender, RoutedEventArgs e)
        {
            ConnectItem.Header = "Отключено";
            MessageBox.Show(ConnectItem.Header.ToString(), "Настройка подключение базы данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            MainTabControl.IsEnabled = false;
            CartridgeDataGrid.IsEnabled = false;
            CashRegisterDataGrid.IsEnabled = false;
            IndividualEntrepreneurDataGrid.IsEnabled = false;
            EmployeesDataGrid.IsEnabled = false;
            PrinterDataGrid.IsEnabled = false;
            SimCardDataGrid.IsEnabled = false;
            DocumentsDataGrid.IsEnabled = false;
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
                   // binaryformatter.Serialize(memorystream, dataGets.DocumentsList[dataGets.SelectedXIndex].FilePDF);
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