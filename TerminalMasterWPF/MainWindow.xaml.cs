using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TerminalMasterWPF.DML;
using TerminalMasterWPF.ElementContentDialog;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Settings;
using TerminalMasterWPF.ViewModel;

namespace TerminalMasterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string NameNavigationItem, CheckASCorDesc, CheckTag;
        private readonly DataGets dataGets = new DataGets();
        private GetElement Get = new GetElement();
        private DeleteElement Delete = new DeleteElement();
        private OrderByElement Order = new OrderByElement();
        private ConnectSQL connect = new ConnectSQL();
        //private DataGridSortDirection? CheckSort;
        private bool triggerSort = true, triggerHeader, triggerPropertyNameList;
        private Dictionary<string, string> PropertyNameDictionary;
        private LogFile logFile = new LogFile();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            connect.ConnectRead();
        }

        private void UpdateTable(string items)
        {
            try
            {
                switch (items)
                {
                    case "printer":
                        PrinterDataGrid.ItemsSource = dataGets.PrinterList;
                        break;
                    case "cartrides":
                        CartridgeDataGrid.ItemsSource = dataGets.CartridgesList;
                        break;
                    case "cashRegister":
                        CashRegisterDataGrid.ItemsSource = dataGets.CashRegisterList;
                        break;
                    case "simCard":
                        SimCardDataGrid.ItemsSource = dataGets.SimCardList;
                        break;
                    case "phoneBook":
                        PhoneBookDataGrid.ItemsSource = dataGets.PhoneBookList;
                        break;
                    case "holder":
                        HolderDataGrid.ItemsSource = dataGets.HolderList;
                        break;
                    case "user":
                        UserDataGrid.ItemsSource = dataGets.UserList;
                        break;
                    case "ie":
                        IndividualEntrepreneurDataGrid.ItemsSource = dataGets.IndividualEntrepreneurList;
                        break;
                    case "waybill":
                        WaybillDataGrid.ItemsSource = dataGets.WaybillList;
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
        private void PrinterDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "ModelPrinter":
                        e.Column.Header = "Модель";
                        break;
                    case "NamePort":
                        e.Column.Header = "IP-Адрес";
                        break;
                    case "VendorCodePrinter":
                        e.Column.Header = "Артикули";
                        break;
                    case "LocationPrinter":
                        e.Column.Header = "Расположение принтера";
                        break;
                    case "Сounters":
                        e.Column.Header = "Распечатанных страниц";
                        break;
                    case "BrandPrinter":
                        e.Column.Header = "Фирма";
                        break;
                    case "Cartridge":
                        e.Column.Header = "Картридж";
                        break;
                    case "DatePrinter":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "Дата состояния";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "DatePrinterString":
                        e.Column.Header = "Дата состояния";
                        break;
                    case "Status":
                        e.Column.Header = "Статус";
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void PrinterDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void PrinterDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void PrinterDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void PrinterDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        private void PrinterDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = PrinterDataGrid.Items.IndexOf(PrinterDataGrid.CurrentItem);
            Debug.WriteLine("SelectedXIndex => " + dataGets.SelectedXIndex);
        }
        /// <summary>
        /// Event to Cartidge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartridgeDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "Brand":
                        e.Column.Header = "Бренд";
                        break;
                    case "Model":
                        e.Column.Header = "Модель";
                        break;
                    case "VendorCode":
                        e.Column.Header = "Артикуль";
                        break;
                    case "Status":
                        e.Column.Header = "Статус";
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void CartridgeDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void CartridgeDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void CartridgeDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void CartridgeDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        /// <summary>
        /// Event to CashRegister
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CashRegisterDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "NameDevice":
                        e.Column.Header = "ККМ";
                        break;
                    case "FactoryNumber":
                        e.Column.Header = "Заводской номер";
                        break;
                    case "Brand":
                        e.Column.Header = "Бренд";
                        break;
                    case "SerialNumber":
                        e.Column.Header = "Серийный номер";
                        break;
                    case "PaymentNumber":
                        e.Column.Header = "Номер счета";
                        break;
                    case "Holder":
                        e.Column.Header = "Владелец";
                        break;
                    case "User":
                        e.Column.Header = "Пользователь";
                        break;
                    case "DateReception":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "Дата получения";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "DateReceptionString":
                        e.Column.Header = "Дата получения";
                        break;
                    case "DateEndFiscalMemory":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "Дата окончания ФН";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "DateEndFiscalMemoryString":
                        e.Column.Header = "Дата окончания ФН";
                        break;
                    case "DateKeyActivationFiscalDataOperator":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "Дата активации ОФД";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "DateKeyActivationFiscalDataOperatorString":
                        e.Column.Header = "Дата активации ОФД";
                        break;
                    case "Location":
                        e.Column.Header = "Место нахождения";
                        break;
                    case "Status":
                        e.Column.Header = "Статус";
                        break;
                    case "IdHolder":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "IdHolder";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "IdUser":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "IdUser";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void CashRegisterDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void CashRegisterDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void CashRegisterDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void CashRegisterDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        /// <summary>
        /// Event to SimCard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimCardDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "NameTerminal":
                        e.Column.Header = "Имя терминала";
                        break;
                    case "Brand":
                        e.Column.Header = "Бренд";
                        break;
                    case "Operator":
                        e.Column.Header = "Оператор связи";
                        break;
                    case "IdentNumber":
                        e.Column.Header = "Идентификационный номер (ИН)";
                        break;
                    case "TypeDevice":
                        e.Column.Header = "Тип устройства";
                        break;
                    case "TMS":
                        e.Column.Header = "Номер телефона (TMS)";
                        break;
                    case "ICC":
                        e.Column.Header = "Уникальный серийный номер (ICC)";
                        break;
                    case "IndividualEntrepreneur":
                        e.Column.Header = "Индивидуальный предприниматель (ИП)";
                        break;
                    case "IdIndividual":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "IdIndividual";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "IdCashRegister":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "IdCashRegister";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "Status":
                        e.Column.Header = "Статус";
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void SimCardDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void SimCardDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void SimCardDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void SimCardDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        /// <summary>
        /// Event to PhoneBook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneBookDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "LastName":
                        e.Column.Header = "Фамилия";
                        break;
                    case "FirstName":
                        e.Column.Header = "Имя";
                        break;
                    case "MiddleName":
                        e.Column.Header = "Отчество";
                        break;
                    case "Post":
                        e.Column.Header = "Должность";
                        break;
                    case "InternalNumber":
                        e.Column.Header = "Внутренный номер";
                        break;
                    case "MobileNumber":
                        e.Column.Header = "Мобильный номер";
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void PhoneBookDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void PhoneBookDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void PhoneBookDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void PhoneBookDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        /// <summary>
        /// Event to Holder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HolderDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "LastName":
                        e.Column.Header = "Фамилия";
                        break;
                    case "FirstName":
                        e.Column.Header = "Имя";
                        break;
                    case "MiddleName":
                        e.Column.Header = "Отчество";
                        break;
                    case "Number":
                        e.Column.Header = "Мобильный номер";
                        break;
                    case "Status":
                        e.Column.Header = "Статус";
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void HolderDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void HolderDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void HolderDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void HolderDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        /// <summary>
        /// Event to User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "LastName":
                        e.Column.Header = "Фамилия";
                        break;
                    case "FirstName":
                        e.Column.Header = "Имя";
                        break;
                    case "MiddleName":
                        e.Column.Header = "Отчество";
                        break;
                    case "Number":
                        e.Column.Header = "Мобильный номер";
                        break;
                    case "Status":
                        e.Column.Header = "Статус";
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void UserDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void UserDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void UserDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void UserDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        /// <summary>
        /// Event to Individual Entrepreneur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndividualEntrepreneurDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "LastName":
                        e.Column.Header = "Фамилия";
                        break;
                    case "FirstName":
                        e.Column.Header = "Имя";
                        break;
                    case "MiddleName":
                        e.Column.Header = "Отчество";
                        break;
                    case "Number":
                        e.Column.Header = "Мобильный номер";
                        break;
                    case "PSRNIE":
                        e.Column.Header = "ОГРНИП";
                        break;
                    case "TIN":
                        e.Column.Header = "ИНН";
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void IndividualEntrepreneurDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void IndividualEntrepreneurDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void IndividualEntrepreneurDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void IndividualEntrepreneurDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        /// <summary>
        /// Event to Waybill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaybillDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Id":
                        e.Column.Header = "ID";
                        break;
                    case "NameDocument":
                        e.Column.Header = "Имя документа";
                        break;
                    case "NumberDocument":
                        e.Column.Header = "Номер документа";
                        break;
                    case "NumberSuppliers":
                        e.Column.Header = "Номер поставщика";
                        break;
                    case "DateDocument":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "Дата документа";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "DateDocumentString":
                        e.Column.Header = "Дата документа";
                        break;
                    case "FileName":
                        e.Column.Header = "Имя файла";
                        break;
                    case "FilePDF":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "Файлы";
                        break;
                    case "IdHolder":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "IdHolder";
                        e.Column.Visibility = Visibility.Collapsed;
                        triggerPropertyNameList = false;
                        triggerHeader = false;
                        break;
                    case "Holder":
                        e.Column.Header = "Владелец";
                        break;
                    default:
                        break;
                }

                //    if (triggerPropertyNameList)
                //    {
                //        PropertyNameDictionary.Add(e.Column.Header.ToString(), e.PropertyName);
                //    }

                //    if (triggerHeader)
                //    {
                //        SelectionItemComboBox.Items.Add(e.Column.Header);
                //    }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "MainDataGrid_AutoGeneratingColumn");
            }
        }
        private void WaybillDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void WaybillDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        private void WaybillDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void WaybillDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;
            try
            {
                switch (tabItem)
                {
                    case "Принтеры":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = false;
                        CheckTag = null;

                        CartridgeDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();
                        NameNavigationItem = "printer";
                        triggerSort = true;

                        dataGets.PrinterList = Get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);

                        UpdateTable(NameNavigationItem);
                        break;
                    case "Картриджи":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = false;
                        CheckTag = null;

                        CartridgeDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();
                        NameNavigationItem = "cartrides";
                        triggerSort = true;

                        dataGets.CartridgesList = Get.GetCartridges((App.Current as App).ConnectionString, "ALL", 0);

                        UpdateTable(NameNavigationItem);
                        break;
                    case "Контрольная-кассовая машина (ККМ)":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = false;
                        CheckTag = null;

                        CashRegisterDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();
                        NameNavigationItem = "cashRegister";
                        triggerSort = true;

                        dataGets.CashRegisterList = Get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);

                        UpdateTable(NameNavigationItem);
                        break;
                    case "Sim-карты":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = false;
                        CheckTag = null;

                        SimCardDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();

                        dataGets.SimCardList = Get.GetSimCard((App.Current as App).ConnectionString, "ALL", 0);

                        NameNavigationItem = "simCard";
                        triggerSort = true;
                        UpdateTable(NameNavigationItem);
                        break;
                    case "Телефоны сотрудников":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = false;
                        CheckTag = null;

                        PhoneBookDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();
                        NameNavigationItem = "phoneBook";
                        triggerSort = true;

                        dataGets.PhoneBookList = Get.GetPhoneBook((App.Current as App).ConnectionString, "ALL", 0); ;

                        UpdateTable(NameNavigationItem);
                        break;
                    case "Владельцы":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = false;
                        CheckTag = null;

                        HolderDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();
                        NameNavigationItem = "holder";
                        triggerSort = true;

                        dataGets.HolderList = Get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);

                        UpdateTable(NameNavigationItem);
                        break;
                    case "Пользователи":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = false;
                        CheckTag = null;

                        UserDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();
                        NameNavigationItem = "user";
                        triggerSort = true;

                        dataGets.UserList = Get.GetUser((App.Current as App).ConnectionString, "ALL", 0);

                        UpdateTable(NameNavigationItem);
                        break;
                    case "Индивидуальные предприниматели":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = false;
                        CheckTag = null;

                        IndividualEntrepreneurDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();

                        NameNavigationItem = "ie";
                        triggerSort = true;

                        dataGets.IndividualEntrepreneurList = Get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);

                        UpdateTable(NameNavigationItem);
                        break;
                    case "Накладные":
                        PropertyNameDictionary = new Dictionary<string, string>();
                        triggerPropertyNameList = true;
                        triggerHeader = true;
                        DowloandButton.IsEnabled = true;
                        CheckTag = null;

                        WaybillDataGrid.Columns.Clear();
                        PropertyNameDictionary.Clear();

                        NameNavigationItem = "waybill";
                        triggerSort = true;

                        dataGets.WaybillList = Get.GetWaybill((App.Current as App).ConnectionString, "ALL", 0);

                        UpdateTable(NameNavigationItem);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                logFile.WriteLogAsync(ex.Message, "UpdateTable");
            }
        }
        /// <summary>
        /// Click Button Add, Edit, Update, Delete, Dowloand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                triggerPropertyNameList = false;
                triggerHeader = false;
                CheckASCorDesc = null;
                switch (NameNavigationItem)
                {
                    case "printer":
                        PrinterWindows printer = new PrinterWindows()
                        {
                            SelectData = "ADD"
                        };
                        printer.ShowDialog();
                        dataGets.PrinterList = Get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "cartrides":
                        //CartridgeContentDialog cartridge = new CartridgeContentDialog
                        //{
                        //    SelectData = "ADD"
                        //};
                        //await cartridge.ShowAsync();
                        //dataGets.CartridgesList = Get.GetCartridges((App.Current as App).ConnectionString, "ALL", 0);
                        //UpdateTable(NameNavigationItem);
                        break;
                    case "cashRegister":
                        //CashRegisterContentDialog cashRegister = new CashRegisterContentDialog
                        //{
                        //    SelectData = "ADD"
                        //};
                        //await cashRegister.ShowAsync();
                        //dataGets.CashRegisterList = Get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);
                        //UpdateTable(NameNavigationItem);
                        break;
                    case "simCard":
                        //SimCardContentDialog simCard = new SimCardContentDialog
                        //{
                        //    SelectData = "ADD"
                        //};
                        //await simCard.ShowAsync();
                        //dataGets.SimCardList = Get.GetSimCard((App.Current as App).ConnectionString, "ALL", 0);
                        //UpdateTable(NameNavigationItem);
                        break;
                    case "phoneBook":
                        //PhoneBookContentDialog phoneBook = new PhoneBookContentDialog
                        //{
                        //    SelectData = "ADD"
                        //};
                        //await phoneBook.ShowAsync();
                        //dataGets.PhoneBookList = Get.GetPhoneBook((App.Current as App).ConnectionString, "ALL", 0);
                        //UpdateTable(NameNavigationItem);
                        break;
                    case "holder":
                        //PeopleContentDialog holder = new PeopleContentDialog
                        //{
                        //    SelectData = "ADD",
                        //    People = NameNavigationItem
                        //};
                        //await holder.ShowAsync();
                        //dataGets.HolderList = Get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);
                        //UpdateTable(NameNavigationItem);
                        break;
                    case "user":
                        //PeopleContentDialog user = new PeopleContentDialog
                        //{
                        //    SelectData = "ADD",
                        //    People = NameNavigationItem
                        //};
                        //await user.ShowAsync();
                        //dataGets.UserList = Get.GetUser((App.Current as App).ConnectionString, "ALL", 0);
                        //UpdateTable(NameNavigationItem);
                        break;
                    case "ie":
                        //indContentDialog individual = new indContentDialog
                        //{
                        //    SelectData = "ADD",
                        //    People = NameNavigationItem
                        //};
                        //await individual.ShowAsync();
                        //dataGets.IndividualEntrepreneurList = Get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);
                        //UpdateTable(NameNavigationItem);
                        break;
                    case "waybill":
                        //WaybillContentDialog waybill = new WaybillContentDialog
                        //{
                        //    SelectData = "ADD"
                        //};
                        //await waybill.ShowAsync();
                        //dataGets.WaybillList = Get.GetWaybill((App.Current as App).ConnectionString, "ALL", 0);
                        //UpdateTable(NameNavigationItem);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
               logFile.WriteLogAsync(ex.Message, "AppBarButtonAdd_Tapped");
            }
        }

        private void CartridgeDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void CashRegisterDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void SimCardDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void PhoneBookDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void HolderDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void IndividualEntrepreneurDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void WaybillDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void SettingsDataBase_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindows connectWindows = new ConnectWindows();
            connectWindows.ShowDialog();
        }

        private void ConnectItem_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                triggerPropertyNameList = false;
                triggerHeader = false;
                switch (NameNavigationItem)
                {
                    case "printer":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            PrinterWindows printer = new PrinterWindows
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.PrinterList[dataGets.SelectedXIndex].Id,
                                SelectPrinter = dataGets.PrinterList
                            };
                            printer.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "cartrides":
                        if (CartridgeDataGrid.SelectedIndex >= 0)
                        {
                            //CartridgeContentDialog cartridge = new CartridgeContentDialog
                            //{
                            //    SelectData = "GET",
                            //    SelectIndex = MainDataGrid.SelectedIndex,
                            //    SelectId = dataGets.CartridgesList[MainDataGrid.SelectedIndex].Id,
                            //    SelectCartrides = dataGets.CartridgesList
                            //};
                            //await cartridge.ShowAsync();
                            //if (CheckASCorDesc.Equals("Ascending"))
                            //{
                            //    dataGets.CartridgesList = Order.GetOrderByCartridges((App.Current as App).ConnectionString, "Ascending", CheckTag);
                            //}
                            //else if (CheckASCorDesc.Equals("Descending"))
                            //{
                            //    dataGets.CartridgesList = Order.GetOrderByCartridges((App.Current as App).ConnectionString, "Descending", CheckTag);
                            //}
                            //else
                            //{
                            //    dataGets.CartridgesList = Get.GetCartridges((App.Current as App).ConnectionString, "ALL", 0);
                            //}
                            //UpdateTable(NameNavigationItem);
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "cashRegister":
                        if (CashRegisterDataGrid.SelectedIndex >= 0)
                        {
                            //CashRegisterContentDialog cashRegister = new CashRegisterContentDialog
                            //{
                            //    SelectData = "GET",
                            //    SelectIndex = MainDataGrid.SelectedIndex,
                            //    SelectId = dataGets.CashRegisterList[MainDataGrid.SelectedIndex].Id,
                            //    SelectCashRegister = dataGets.CashRegisterList
                            //};
                            //await cashRegister.ShowAsync();
                            //if (CheckASCorDesc.Equals("Ascending"))
                            //{
                            //    dataGets.CashRegisterList = Order.GetOrderByCashRegister((App.Current as App).ConnectionString, "Ascending", CheckTag);
                            //}
                            //else if (CheckASCorDesc.Equals("Descending"))
                            //{
                            //    dataGets.CashRegisterList = Order.GetOrderByCashRegister((App.Current as App).ConnectionString, "Descending", CheckTag);
                            //}
                            //else
                            //{
                            //    dataGets.CashRegisterList = Get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);
                            //}
                            //UpdateTable(NameNavigationItem);
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "simCard":
                        if (SimCardDataGrid.SelectedIndex >= 0)
                        {
                            //SimCardContentDialog simCard = new SimCardContentDialog
                            //{
                            //    SelectData = "GET",
                            //    SelectIndex = MainDataGrid.SelectedIndex,
                            //    SelectId = dataGets.SimCardList[MainDataGrid.SelectedIndex].Id,
                            //    SelectSimCard = dataGets.SimCardList
                            //};
                            //await simCard.ShowAsync();
                            //if (CheckASCorDesc.Equals("Ascending"))
                            //{
                            //    dataGets.SimCardList = Order.GetOrderBySimCard((App.Current as App).ConnectionString, "Ascending", CheckTag);
                            //}
                            //else if (CheckASCorDesc.Equals("Descending"))
                            //{
                            //    dataGets.SimCardList = Order.GetOrderBySimCard((App.Current as App).ConnectionString, "Descending", CheckTag);
                            //}
                            //else
                            //{
                            //    dataGets.SimCardList = Get.GetSimCard((App.Current as App).ConnectionString, "ALL", 0);
                            //}
                            //UpdateTable(NameNavigationItem);
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "phoneBook":
                        if (PhoneBookDataGrid.SelectedIndex >= 0)
                        {
                            //PhoneBookContentDialog phoneBook = new PhoneBookContentDialog
                            //{
                            //    SelectData = "GET",
                            //    SelectIndex = MainDataGrid.SelectedIndex,
                            //    SelectId = dataGets.PhoneBookList[MainDataGrid.SelectedIndex].Id,
                            //    SelectPhoneBook = dataGets.PhoneBookList
                            //};
                            //await phoneBook.ShowAsync();
                            //if (CheckASCorDesc.Equals("Ascending"))
                            //{
                            //    dataGets.PhoneBookList = Order.GetOrderByPhoneBook((App.Current as App).ConnectionString, "Ascending", CheckTag);
                            //}
                            //else if (CheckASCorDesc.Equals("Descending"))
                            //{
                            //    dataGets.PhoneBookList = Order.GetOrderByPhoneBook((App.Current as App).ConnectionString, "Descending", CheckTag);
                            //}
                            //else
                            //{
                            //    dataGets.PhoneBookList = Get.GetPhoneBook((App.Current as App).ConnectionString, "ALL", 0);
                            //}
                            //UpdateTable(NameNavigationItem);
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "holder":
                        if (HolderDataGrid.SelectedIndex >= 0)
                        {
                            //PeopleContentDialog holder = new PeopleContentDialog
                            //{
                            //    SelectData = "GET",
                            //    SelectIndex = MainDataGrid.SelectedIndex,
                            //    SelectId = dataGets.HolderList[MainDataGrid.SelectedIndex].Id,
                            //    SelectHolder = dataGets.HolderList,
                            //    People = NameNavigationItem
                            //};
                            //await holder.ShowAsync();
                            //if (CheckASCorDesc.Equals("Ascending"))
                            //{
                            //    dataGets.HolderList = Order.GetOrderByHolder((App.Current as App).ConnectionString, "Ascending", CheckTag);
                            //}
                            //else if (CheckASCorDesc.Equals("Descending"))
                            //{
                            //    dataGets.HolderList = Order.GetOrderByHolder((App.Current as App).ConnectionString, "Descending", CheckTag);
                            //}
                            //else
                            //{
                            //    dataGets.HolderList = Get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);
                            //}
                            //UpdateTable(NameNavigationItem);
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "user":
                        if (UserDataGrid.SelectedIndex >= 0)
                        {
                            //PeopleContentDialog user = new PeopleContentDialog
                            //{
                            //    SelectData = "GET",
                            //    SelectIndex = MainDataGrid.SelectedIndex,
                            //    SelectId = dataGets.UserList[MainDataGrid.SelectedIndex].Id,
                            //    SelectUser = dataGets.UserList,
                            //    People = NameNavigationItem
                            //};
                            //await user.ShowAsync();
                            //if (CheckASCorDesc.Equals("Ascending"))
                            //{
                            //    dataGets.UserList = Order.GetOrderByUser((App.Current as App).ConnectionString, "Ascending", CheckTag);
                            //}
                            //else if (CheckASCorDesc.Equals("Descending"))
                            //{
                            //    dataGets.UserList = Order.GetOrderByUser((App.Current as App).ConnectionString, "Descending", CheckTag);
                            //}
                            //else
                            //{
                            //    dataGets.UserList = Get.GetUser((App.Current as App).ConnectionString, "ALL", 0);
                            //}
                            //UpdateTable(NameNavigationItem);
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "ie":
                        if (IndividualEntrepreneurDataGrid.SelectedIndex >= 0)
                        {
                        //    indContentDialog individual = new indContentDialog
                        //    {
                        //        SelectData = "GET",
                        //        SelectIndex = MainDataGrid.SelectedIndex,
                        //        SelectId = dataGets.IndividualEntrepreneurList[MainDataGrid.SelectedIndex].Id,
                        //        SelectInd = dataGets.IndividualEntrepreneurList,
                        //        People = NameNavigationItem
                        //    };
                        //    await individual.ShowAsync();
                        //    if (CheckASCorDesc.Equals("Ascending"))
                        //    {
                        //        dataGets.IndividualEntrepreneurList = Order.GetOrderByIndividual((App.Current as App).ConnectionString, "Ascending", CheckTag);
                        //    }
                        //    else if (CheckASCorDesc.Equals("Descending"))
                        //    {
                        //        dataGets.IndividualEntrepreneurList = Order.GetOrderByIndividual((App.Current as App).ConnectionString, "Descending", CheckTag);
                        //    }
                        //    else
                        //    {
                        //        dataGets.IndividualEntrepreneurList = Get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);
                        //    }
                        //    UpdateTable(NameNavigationItem);
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "waybill":
                        if (WaybillDataGrid.SelectedIndex >= 0)
                        {
                        //    WaybillContentDialog waybill = new WaybillContentDialog
                        //    {
                        //        SelectData = "GET",
                        //        SelectIndex = MainDataGrid.SelectedIndex,
                        //        SelectId = dataGets.WaybillList[MainDataGrid.SelectedIndex].Id,
                        //        SelectWaybill = dataGets.WaybillList
                        //    };
                        //    await waybill.ShowAsync();
                        //    if (CheckASCorDesc.Equals("Ascending"))
                        //    {
                        //        dataGets.WaybillList = Order.GetOrderByWaybill((App.Current as App).ConnectionString, "Ascending", CheckTag);
                        //    }
                        //    else if (CheckASCorDesc.Equals("Descending"))
                        //    {
                        //        dataGets.WaybillList = Order.GetOrderByWaybill((App.Current as App).ConnectionString, "Descending", CheckTag);
                        //    }
                        //    else
                        //    {
                        //        dataGets.WaybillList = Get.GetWaybill((App.Current as App).ConnectionString, "ALL", 0);
                        //    }
                        //    UpdateTable(NameNavigationItem);
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "AppBarButtonEdit_Tapped");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                triggerPropertyNameList = false;
                triggerHeader = false;

                if(dataGets.SelectedXIndex >= 0)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент.", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        switch (NameNavigationItem)
                        {
                            case "printer":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.PrinterList[dataGets.SelectedXIndex].Id, NameNavigationItem);
                                dataGets.PrinterList = Get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            case "cartrides":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.CartridgesList[dataGets.SelectedXIndex].Id, NameNavigationItem);
                                dataGets.CartridgesList = Get.GetCartridges((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            case "cashRegister":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.CashRegisterList[dataGets.SelectedXIndex].Id, NameNavigationItem);
                                dataGets.CashRegisterList = Get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            case "simCard":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SimCardList[dataGets.SelectedXIndex].Id, NameNavigationItem);
                                dataGets.SimCardList = Get.GetSimCard((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            case "phoneBook":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.PhoneBookList[dataGets.SelectedXIndex].Id, NameNavigationItem);
                                dataGets.PhoneBookList = Get.GetPhoneBook((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            case "holder":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.HolderList[dataGets.SelectedXIndex].Id, NameNavigationItem);
                                dataGets.HolderList = Get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            case "user":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.UserList[dataGets.SelectedXIndex].Id, NameNavigationItem);
                                dataGets.UserList = Get.GetUser((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            case "ie":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.IndividualEntrepreneurList[dataGets.SelectedXIndex].Id, NameNavigationItem);
                                dataGets.IndividualEntrepreneurList = Get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            case "waybill":
                                Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.WaybillList[WaybillDataGrid.SelectedIndex].Id, NameNavigationItem);
                                dataGets.WaybillList = Get.GetWaybill((App.Current as App).ConnectionString, "ALL", 0);
                                UpdateTable(NameNavigationItem);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления", "Удаление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "AppBarButtonDelete_Tapped");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                triggerPropertyNameList = false;
                triggerHeader = false;
                switch (NameNavigationItem)
                {
                    case "printer":
                        dataGets.PrinterList= Get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "cartrides":
                        dataGets.CartridgesList = Get.GetCartridges((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "cashRegister":
                        dataGets.CashRegisterList = Get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "simCard":
                        dataGets.SimCardList = Get.GetSimCard((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "phoneBook":
                        dataGets.PhoneBookList = Get.GetPhoneBook((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "holder":
                        dataGets.HolderList = Get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "user":
                        dataGets.UserList = Get.GetUser((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "ie":
                        dataGets.IndividualEntrepreneurList = Get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    case "waybill":
                        dataGets.WaybillList = Get.GetWaybill((App.Current as App).ConnectionString, "ALL", 0);
                        UpdateTable(NameNavigationItem);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
               logFile.WriteLogAsync(ex.Message, "AppBarButtonUpdate_Tapped");
            }
        }
        private void DowloandButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                triggerPropertyNameList = false;
                triggerHeader = false;

                if (WaybillDataGrid.SelectedIndex >= 0)
                {
                    BinaryFormatter binaryformatter = new BinaryFormatter();
                    MemoryStream memorystream = new MemoryStream();
                    binaryformatter.Serialize(memorystream, dataGets.WaybillList[WaybillDataGrid.SelectedIndex].FilePDF);
                    byte[] data = memorystream.ToArray();
                //    AsStorageFile(data, dataGets.WaybillList[WaybillDataGrid.SelectedIndex].FileName);

                }
                else
                {
                   MessageBox.Show("Выберите строку для скачивания", "Скачивание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "AppBarButtonDowloand_Tapped");
            }
        }
    }
}
