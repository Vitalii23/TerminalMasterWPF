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
using System.Windows.Input;
using System.Windows.Data;
using System.Collections;
using Telerik.Windows.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace TerminalMasterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string NameNavigationItem;
        private DataGets dataGets = new DataGets();
        private readonly ConnectSQL connect = new ConnectSQL();
        private DataManipulationLanguage<Printer> printer;
        private DataManipulationLanguage<Cartridge> cartridge;
        private DataManipulationLanguage<CashRegister> cashRegister;
        private DataManipulationLanguage<SimCard> simCard;
        private DataManipulationLanguage<Employees> employees;
        private DataManipulationLanguage<IndividualEntrepreneur> ie;
        private DataManipulationLanguage<Documents> documents;
        private DataManipulationLanguage<CountersPage> counterPage;
        private DataManipulationLanguage<Holder> holder;
        private readonly LogFile log = new LogFile();
        private int count = 0;

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
                    case "documents":
                        DocumentsDataGrid.ItemsSource = list;
                        break;
                    case "countersPage":
                        CountersPageDataGrid.ItemsSource = list;
                        PrintersGridViewDataColumn.ItemsSource = dataGets.PrinterList;
                        PrintersGridViewDataColumn.DisplayMemberPath = "FullNamePrinters";
                        PrintersGridViewDataColumn.DataContext = "Printer";
                        PrintersGridViewDataColumn.SelectedValueMemberPath = "Id";
                        PrintersGridViewDataColumn.DataMemberBinding = new Binding("IdPrinter");
                        break;
                    case "all":
                        PrinterDataGrid.ItemsSource = list;
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

        private async void UpdateData(string items)
        {
            try
            {

                switch (items)
                {
                    case "printer":
                        dataGets.PrinterList = printer.GetPrinters();
                        break;
                    case "cashRegister":
                        dataGets.CashRegisterList = cashRegister.GetCashRegisters();
                        dataGets.HolderList = holder.GetHolderList();
                        break;
                    case "cartridge":
                        dataGets.CartridgesList = cashRegister.GetCartridges();
                        break;
                    case "employees":
                        dataGets.EmployessList = employees.GetEmployees();
                        break;
                    case "simCard":
                        dataGets.SimCardList = simCard.GetSimCardList();
                        break;
                    case "ie":
                        dataGets.IndividualList = ie.GetIndividualEntrepreneur();
                        break;
                    case "documents":
                        dataGets.DocumentsList = documents.GetDocuments();
                        break;
                    case "countersPage":
                        dataGets.CountersPagesList = counterPage.GetCountersPagesList();
                        break;
                    case "all":
                        dataGets.PrinterList = printer.GetPrinters();
                        dataGets.CashRegisterList = cashRegister.GetCashRegisters();
                        dataGets.CartridgesList = cashRegister.GetCartridges();
                        dataGets.EmployessList = employees.GetEmployees();
                        dataGets.HolderList = holder.GetHolderList();
                        dataGets.SimCardList = simCard.GetSimCardList();
                        dataGets.IndividualList = ie.GetIndividualEntrepreneur();
                        dataGets.DocumentsList = documents.GetDocuments();
                        dataGets.CountersPagesList = counterPage.GetCountersPagesList();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception e)
            {
                await log.WriteLogAsync(e.Message, "UpdateData");
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

                    return;
                }

                if (e.EditOperationType == GridViewEditOperationType.Edit)
                {
                    if (e.NewData is T newElement)
                    {
                        element.Update(newElement);
                    }

                    return;
                }

                UpdateData(NameNavigationItem);
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

                ObservableCollection<T> itemsRemove = new ObservableCollection<T>();

                foreach (object item in gridView.SelectedItems)
                {
                    itemsRemove.Add(item as T);
                }

                foreach (T item in itemsRemove)
                {
                    ((ObservableCollection<T>)gridView.ItemsSource).Remove(item as T);
                    element.Delete(item);
                }

            }
            catch (Exception ex)
            {
                await log.WriteLogAsync(ex.Message, voidMessage);
            }
        }

        private void GroupingdDataGrid(GridViewGroupingEventArgs e, RadDataPager radDataPager)
        {
            if (e.Index == null && e.Action != GroupingEventAction.Sort)
            {
                count--;
            }

            if (e.Action == GroupingEventAction.Place)
            {
                count++;
                radDataPager.PageSize = 0;
            }

            if (e.Action == GroupingEventAction.Remove && count == 0)
            {
                radDataPager.PageSize = 32;
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

        private void PrinterDataGrid_Grouping(object sender, GridViewGroupingEventArgs e)
        {
            GroupingdDataGrid(e, PrinterRadDataPager);
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

        private void CountersPageDataGrid_Grouping(object sender, GridViewGroupingEventArgs e)
        {
            GroupingdDataGrid(e, CounterRadDataPager);
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

        private void CartridgeDataGrid_Grouping(object sender, GridViewGroupingEventArgs e)
        {
            GroupingdDataGrid(e, CartridgeRadDataPager);
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

        private void CashRegisterDataGrid_Grouping(object sender, GridViewGroupingEventArgs e)
        {
            GroupingdDataGrid(e, CashRegisterRadDataPager);
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

        private void SimCardDataGrid_Grouping(object sender, GridViewGroupingEventArgs e)
        {
            GroupingdDataGrid(e, SimCardRadDataPager);
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

        private void EmployeesDataGrid_Grouping(object sender, GridViewGroupingEventArgs e)
        {
            GroupingdDataGrid(e, EmployeesRadDataPager);
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

        private void IndividualEntrepreneurDataGrid_Grouping(object sender, GridViewGroupingEventArgs e)
        {
            GroupingdDataGrid(e, IndRadDataPager);
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

        private void DocumentsDataGrid_Grouping(object sender, GridViewGroupingEventArgs e)
        {
            GroupingdDataGrid(e, DocumentRadDataPager);
        }

        private async void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog
                {
                    InitialDirectory = @"\\KV-SQL-N\TerminalDataFiles",
                    FilterIndex = 2,
                    Filter = "File image (*.jpeg, *.jpg, .png)|*.jpeg;*.jpg;*.png|" +
                    "Documents file (*.pdf)|*.pdf",
                    RestoreDirectory = true
                };

                bool? result = openFile.ShowDialog();
                if (result == true)
                {
                    dataGets.Documents.FileNamePath = @"(SELECT * FROM  OPENROWSET(BULK '" + openFile.FileName + "', SINGLE_BLOB) AS file_binary)";
                }
            }
            catch (Exception ex)
            {
                await log.WriteLogAsync(ex.Message, "AddFileButton_Click");
            }
        }

        private async void DowloanderFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = 0;

                if (DocumentsDataGrid.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Выберите строку для скачивания", "Скачивание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ObservableCollection<Documents> itemsDocumentsRemove = new ObservableCollection<Documents>();

                foreach (object item in DocumentsDataGrid.SelectedItems)
                {
                    itemsDocumentsRemove.Add(item as Documents);
                }

                foreach (Documents item in itemsDocumentsRemove)
                {
                    id = item.Id;
                }

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

                if (result == true)
                {
                    BinaryFormatter binaryformatter = new BinaryFormatter();
                    MemoryStream memorystream = new MemoryStream();
                    binaryformatter.Serialize(memorystream, documents.GetByte(id));
                    byte[] data = memorystream.ToArray();

                    using (FileStream fileStream = File.Create(saveFileDialog.FileName))
                    {
                        fileStream.Write(data, 0, data.Length);
                    }

                    MessageBox.Show("Успешно скачанно", "Скачивание", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                await log.WriteLogAsync(ex.Message, "DowloanderFileButton_Click");
            }
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
                        case "Документы":
                            NameNavigationItem = "documents";
                            UpdateTable(NameNavigationItem, dataGets.DocumentsList);
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

            printer = new DataManipulationLanguage<Printer>();
            cartridge = new DataManipulationLanguage<Cartridge>();
            cashRegister = new DataManipulationLanguage<CashRegister>();
            simCard = new DataManipulationLanguage<SimCard>();
            employees = new DataManipulationLanguage<Employees>();
            ie = new DataManipulationLanguage<IndividualEntrepreneur>();
            documents = new DataManipulationLanguage<Documents>();
            counterPage = new DataManipulationLanguage<CountersPage>();
            holder = new DataManipulationLanguage<Holder>();

            UpdateData("all");

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


            printer.GetPrinters().Clear();
            cartridge.GetCartridges().Clear();
            cashRegister.GetCashRegisters().Clear();
            simCard.GetSimCardList().Clear();
            employees.GetEmployees().Clear();
            ie.GetIndividualEntrepreneur().Clear();
            documents.GetDocuments().Clear();
            counterPage.GetCountersPagesList().Clear();
            holder.GetHolderList().Clear();

            dataGets.PrinterList.Clear();
            dataGets.CartridgesList.Clear();
            dataGets.CashRegisterList.Clear();
            dataGets.SimCardList.Clear();
            dataGets.EmployessList.Clear();
            dataGets.IndividualList.Clear();
            dataGets.DocumentsList.Clear();
            dataGets.CountersPagesList.Clear();
            dataGets.HolderList.Clear();
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

        private void SettingsDataBase_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindows connectWindows = new ConnectWindows();
            connectWindows.ShowDialog();
        }

        private void DocumentsDataGrid_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            
        }
    }
}