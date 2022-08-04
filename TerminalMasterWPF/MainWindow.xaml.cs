using System;
using System.Data;
using System.Collections.Generic;
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
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace TerminalMasterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string NameNavigationItem, CheckASCorDesc = null, CheckTag = null;
        private readonly DataGets dataGets = new DataGets();
        private GetElement Get = new GetElement();
        private DeleteElement Delete = new DeleteElement();
        private OrderByElement Order = new OrderByElement();
        private ConnectSQL connect = new ConnectSQL();
        //private Dictionary<string, string> PropertyNameDictionary;
        private LogFile logFile = new LogFile();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AddButton.IsEnabled = false;
            EditButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            UpdateButton.IsEnabled = false;
            DowloandButton.IsEnabled = false;
            MainTabControl.IsEnabled = false;
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

        private DataGridCell GetCell(int row, int column, DataGrid dataGrid)
        {
            DataGridRow rowContainer = GetRow(row, dataGrid);
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;

        }

        private DataGridRow GetRow(int index, DataGrid dataGrid)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dataGrid.UpdateLayout();
                dataGrid.ScrollIntoView(dataGrid.Items[index]);
                row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        private static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>
                    (v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "BrandPrinter":
                    CheckTag = "brand";
                    break;
                case "ModelPrinter":
                    CheckTag = "model";
                    break;
                case "Cartridge":
                    CheckTag = "cartridge";
                    break;
                case "NamePort":
                    CheckTag = "name_port";
                    break;
                case "LocationPrinter":
                    CheckTag = "location_port";
                    break;
                case "Status":
                    CheckTag = "status";
                    break;
                case "VendorCodePrinter":
                    CheckTag = "vendor_code";
                    break;
                case "Сounters":
                    CheckTag = "counters";
                    break;
                case "DatePrinterString":
                    CheckTag = "date_printer";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.PrinterList = Order.GetOrderByPrinter((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.PrinterList = Order.GetOrderByPrinter((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }

        }

        private void PrinterDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = PrinterDataGrid.Items.IndexOf(PrinterDataGrid.CurrentItem);
            DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, PrinterDataGrid);
            TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
            dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "Brand":
                    CheckTag = "brand";
                    break;
                case "Model":
                    CheckTag = "model";
                    break;
                case "VendorCode":
                    CheckTag = "vendor_code";
                    break;
                case "Status":
                    CheckTag = "status";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.CartridgesList = Order.GetOrderByCartridges((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.CartridgesList = Order.GetOrderByCartridges((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
        }

        private void CartridgeDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = CartridgeDataGrid.Items.IndexOf(CartridgeDataGrid.CurrentItem);
            DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, CartridgeDataGrid);
            TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
            dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
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
                        break;
                    case "DateReceptionString":
                        e.Column.Header = "Дата получения";
                        break;
                    case "DateEndFiscalMemory":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "Дата окончания ФН";
                        e.Column.Visibility = Visibility.Collapsed;
                        break;
                    case "DateEndFiscalMemoryString":
                        e.Column.Header = "Дата окончания ФН";
                        break;
                    case "DateKeyActivationFiscalDataOperator":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "Дата активации ОФД";
                        e.Column.Visibility = Visibility.Collapsed;
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
                        break;
                    case "IdUser":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "IdUser";
                        e.Column.Visibility = Visibility.Collapsed;
                        break;
                    default:
                        break;
                }
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "NameDevice":
                    CheckTag = "name";
                    break;
                case "Brand":
                    CheckTag = "brand";
                    break;
                case "FactoryNumber":
                    CheckTag = "factory_number";
                    break;
                case "SerialNumber":
                    CheckTag = "serial_number";
                    break;
                case "PaymentNumber":
                    CheckTag = "payment_number";
                    break;
                case "Holder":
                    CheckTag = "holder";
                    break;
                case "User":
                    CheckTag = "user";
                    break;
                case "DateReception":
                    CheckTag = "date_reception";
                    break;
                case "DateEndFiscalMemory":
                    CheckTag = "date_end_fiscal_memory";
                    break;
                case "DateKeyActivationFiscalDataOperator":
                    CheckTag = "date_key_activ_fisc_data";
                    break;
                case "Location":
                    CheckTag = "location";
                    break;
                case "IdHolder":
                    CheckTag = "id_holder";
                    break;
                case "IdUser":
                    CheckTag = "id_user";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.CashRegisterList = Order.GetOrderByCashRegister((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.CashRegisterList = Order.GetOrderByCashRegister((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
        }

        private void CashRegisterDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = CashRegisterDataGrid.Items.IndexOf(CashRegisterDataGrid.CurrentItem);
            DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, CashRegisterDataGrid);
            TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
            dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
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
                        break;
                    case "IdCashRegister":
                        e.Column.CanUserSort = false;
                        e.Column.Header = "IdCashRegister";
                        e.Column.Visibility = Visibility.Collapsed;
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "NameTerminal":
                    CheckTag = "name_terminal";
                    break;
                case "Operator":
                    CheckTag = "operator";
                    break;
                case "IdentNumber":
                    CheckTag = "identifaction_number";
                    break;
                case "TypeDevice":
                    CheckTag = "type_device";
                    break;
                case "TMS":
                    CheckTag = "tms";
                    break;
                case "ICC":
                    CheckTag = "icc";
                    break;
                case "IndividualEntrepreneur":
                    CheckTag = "individual_entrepreneur";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.SimCardList = Order.GetOrderBySimCard((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag); // bug getorder
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.SimCardList = Order.GetOrderBySimCard((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
        }

        private void SimCardDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = SimCardDataGrid.Items.IndexOf(SimCardDataGrid.CurrentItem);
            DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, SimCardDataGrid);
            TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
            dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "LastName":
                    CheckTag = "last_name";
                    break;
                case "FirstName":
                    CheckTag = "first_name";
                    break;
                case "MiddleName":
                    CheckTag = "middle_name";
                    break;
                case "Post":
                    CheckTag = "post";
                    break;
                case "InternalNumber":
                    CheckTag = "internal_number";
                    break;
                case "MobileNumber":
                    CheckTag = "mobile_number";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.PhoneBookList = Order.GetOrderByPhoneBook((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.PhoneBookList = Order.GetOrderByPhoneBook((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
        }

        private void PhoneBookDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = PhoneBookDataGrid.Items.IndexOf(PhoneBookDataGrid.CurrentItem);
            DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, PhoneBookDataGrid);
            TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
            dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "LastName":
                    CheckTag = "last_name";
                    break;
                case "FirstName":
                    CheckTag = "first_name";
                    break;
                case "MiddleName":
                    CheckTag = "middle_name";
                    break;
                case "Status":
                    CheckTag = "status";
                    break;
                case "Number":
                    CheckTag = "number";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.HolderList = Order.GetOrderByHolder((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.HolderList = Order.GetOrderByHolder((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
        }

        private void HolderDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dataGets.SelectedXIndex = HolderDataGrid.Items.IndexOf(HolderDataGrid.CurrentItem);
                DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, HolderDataGrid);
                TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
                dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine(ex.StackTrace);
            }
  
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "LastName":
                    CheckTag = "last_name";
                    break;
                case "FirstName":
                    CheckTag = "first_name";
                    break;
                case "MiddleName":
                    CheckTag = "middle_name";
                    break;
                case "Status":
                    CheckTag = "status";
                    break;
                case "Number":
                    CheckTag = "number";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.UserList = Order.GetOrderByUser((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.UserList = Order.GetOrderByUser((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
        }

        private void UserDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = UserDataGrid.Items.IndexOf(UserDataGrid.CurrentItem);
            DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, UserDataGrid);
            TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
            dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "LastName":
                    CheckTag = "last_name";
                    break;
                case "FirstName":
                    CheckTag = "first_name";
                    break;
                case "MiddleName":
                    CheckTag = "middle_name";
                    break;
                case "PSRNIE":
                    CheckTag = "psrnie";
                    break;
                case "TIN":
                    CheckTag = "tin";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.IndividualEntrepreneurList = Order.GetOrderByIndividual((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.IndividualEntrepreneurList = Order.GetOrderByIndividual((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
        }

        private void IndividualEntrepreneurDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = IndividualEntrepreneurDataGrid.Items.IndexOf(IndividualEntrepreneurDataGrid.CurrentItem);
            DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, IndividualEntrepreneurDataGrid);
            TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
            dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
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
                        break;
                    case "Holder":
                        e.Column.Header = "Владелец";
                        break;
                    default:
                        break;
                }
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
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Id":
                    CheckTag = "id";
                    break;
                case "NameDocument":
                    CheckTag = "name_document";
                    break;
                case "NumberDocument":
                    CheckTag = "number_document";
                    break;
                case "NumberSuppliers":
                    CheckTag = "number_suppliers";
                    break;
                case "DateDocument":
                    CheckTag = "date_document";
                    break;
                case "FileName":
                    CheckTag = "file_name";
                    break;
                case "FilePDF":
                    CheckTag = "file_pdf";
                    break;
                default:
                    break;
            }

            if (e.Column.SortDirection == null || e.Column.SortDirection == ListSortDirection.Descending)
            {
                CheckASCorDesc = ListSortDirection.Ascending.ToString();
                dataGets.WaybillList = Order.GetOrderByWaybill((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
            else
            {
                CheckASCorDesc = ListSortDirection.Descending.ToString();
                dataGets.WaybillList = Order.GetOrderByWaybill((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
            }
        }

        private void WaybillDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGets.SelectedXIndex = WaybillDataGrid.Items.IndexOf(WaybillDataGrid.CurrentItem);
            DataGridCell cell = GetCell(dataGets.SelectedXIndex, 0, WaybillDataGrid);
            TextBlock lblsourceAddress = GetVisualChild<TextBlock>(cell);
            dataGets.SelectedId = Convert.ToInt32(lblsourceAddress.Text);
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
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = false;

                            CartridgeDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();
                            NameNavigationItem = "printer";

                            dataGets.PrinterList = Get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);

                            UpdateTable(NameNavigationItem);
                            break;
                        case "Картриджи":
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = false;

                            CartridgeDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();
                            NameNavigationItem = "cartrides";

                            dataGets.CartridgesList = Get.GetCartridges((App.Current as App).ConnectionString, "ALL", 0);

                            UpdateTable(NameNavigationItem);
                            break;
                        case "Контрольная-кассовая машина (ККМ)":
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = false;

                            CashRegisterDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();
                            NameNavigationItem = "cashRegister";

                            dataGets.CashRegisterList = Get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);

                            UpdateTable(NameNavigationItem);
                            break;
                        case "Sim-карты":
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = false;

                            SimCardDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();

                            dataGets.SimCardList = Get.GetSimCard((App.Current as App).ConnectionString, "ALL", 0);

                            NameNavigationItem = "simCard";
                            UpdateTable(NameNavigationItem);
                            break;
                        case "Телефоны сотрудников":
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = false;

                            PhoneBookDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();
                            NameNavigationItem = "phoneBook";

                            dataGets.PhoneBookList = Get.GetPhoneBook((App.Current as App).ConnectionString, "ALL", 0); ;

                            UpdateTable(NameNavigationItem);
                            break;
                        case "Владельцы":
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = false;

                            HolderDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();
                            NameNavigationItem = "holder";

                            dataGets.HolderList = Get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);

                            UpdateTable(NameNavigationItem);
                            break;
                        case "Пользователи":
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = false;

                            UserDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();
                            NameNavigationItem = "user";

                            dataGets.UserList = Get.GetUser((App.Current as App).ConnectionString, "ALL", 0);

                            UpdateTable(NameNavigationItem);
                            break;
                        case "Индивидуальные предприниматели":
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = false;

                            IndividualEntrepreneurDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();

                            NameNavigationItem = "ie";

                            dataGets.IndividualEntrepreneurList = Get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);

                            UpdateTable(NameNavigationItem);
                            break;
                        case "Накладные":
                            //PropertyNameDictionary = new Dictionary<string, string>();
                            DowloandButton.IsEnabled = true;

                            WaybillDataGrid.Columns.Clear();
                            //PropertyNameDictionary.Clear();

                            NameNavigationItem = "waybill";

                            dataGets.WaybillList = Get.GetWaybill((App.Current as App).ConnectionString, "ALL", 0);

                            UpdateTable(NameNavigationItem);
                            break;
                        default:
                            break;
                    }
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

        private void ConnectItem_Checked(object sender, RoutedEventArgs e)
        {
            ConnectItem.Header = "Подключено";
            MainTabControl.IsEnabled = true;
            MessageBox.Show(ConnectItem.Header.ToString(), "Настройка подключение базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
            AddButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            UpdateButton.IsEnabled = true;
            PrinterDataGrid.ItemsSource = Get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);
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
            AddButton.IsEnabled = false;
            EditButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            UpdateButton.IsEnabled = false;
        }

        private void ConnectItem_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectItem.IsChecked == true)
            {
                ConnectItem.IsChecked = false;
            }
            else
            {
                ConnectItem.IsChecked = true;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConnectItem.Header.Equals("Подключено") && ConnectItem.IsChecked)
                {
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
                            CartridgeWindow cartridge = new CartridgeWindow
                            {
                                SelectData = "ADD"
                            };
                            cartridge.ShowDialog();
                            dataGets.CartridgesList = Get.GetCartridges((App.Current as App).ConnectionString, "ALL", 0);
                            UpdateTable(NameNavigationItem);
                            break;
                        case "cashRegister":
                            CashRegisterWindows cashRegister = new CashRegisterWindows
                            {
                                SelectData = "ADD"
                            };
                            cashRegister.ShowDialog();
                            dataGets.CashRegisterList = Get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);
                            UpdateTable(NameNavigationItem);
                            break;
                        case "simCard":
                            SimCardWindows simCard = new SimCardWindows
                            {
                                SelectData = "ADD"
                            };
                            simCard.ShowDialog();
                            dataGets.SimCardList = Get.GetSimCard((App.Current as App).ConnectionString, "ALL", 0);
                            UpdateTable(NameNavigationItem);
                            break;
                        case "phoneBook":
                            PhoneBookWindows phoneBook = new PhoneBookWindows
                            {
                                SelectData = "ADD"
                            };
                            phoneBook.ShowDialog();
                            dataGets.PhoneBookList = Get.GetPhoneBook((App.Current as App).ConnectionString, "ALL", 0);
                            UpdateTable(NameNavigationItem);
                            break;
                        case "holder":
                            PeopleWindow holder = new PeopleWindow
                            {
                                SelectData = "ADD",
                                People = NameNavigationItem
                            };
                            holder.ShowDialog();
                            dataGets.HolderList = Get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);
                            UpdateTable(NameNavigationItem);
                            break;
                        case "user":
                            PeopleWindow user = new PeopleWindow
                            {
                                SelectData = "ADD",
                                People = NameNavigationItem
                            };
                            user.ShowDialog();
                            dataGets.UserList = Get.GetUser((App.Current as App).ConnectionString, "ALL", 0);
                            UpdateTable(NameNavigationItem);
                            break;
                        case "ie":
                            IndWindow individual = new IndWindow
                            {
                                SelectData = "ADD",
                                People = NameNavigationItem
                            };
                            individual.ShowDialog();
                            dataGets.IndividualEntrepreneurList = Get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);
                            UpdateTable(NameNavigationItem);
                            break;
                        case "waybill":
                            WaybillWindow waybill = new WaybillWindow
                            {
                                SelectData = "ADD"
                            };
                            waybill.ShowDialog();
                            dataGets.WaybillList = Get.GetWaybill((App.Current as App).ConnectionString, "ALL", 0);
                            UpdateTable(NameNavigationItem);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "AppBarButtonAdd_Tapped");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (NameNavigationItem)
                {
                    case "printer":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            PrinterWindows printer = new PrinterWindows
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.SelectedId
                            };
                            printer.SelectPrinter = CheckASCorDesc == null
                                ? dataGets.PrinterList
                                : CheckASCorDesc.Equals("Descending")
                                    ? Order.GetOrderByPrinter((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                                    : Order.GetOrderByPrinter((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);

                            printer.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "cartrides":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            CartridgeWindow cartridge = new CartridgeWindow
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.SelectedId
                            };
                            cartridge.SelectCartrides = CheckASCorDesc == null
                                ? dataGets.CartridgesList
                                : CheckASCorDesc.Equals("Descending")
                                    ? Order.GetOrderByCartridges((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                                    : Order.GetOrderByCartridges((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);

                            cartridge.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "cashRegister":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            CashRegisterWindows cashRegister = new CashRegisterWindows
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.CashRegisterList[dataGets.SelectedXIndex].Id
                            };
                            cashRegister.SelectCashRegister = CheckASCorDesc == null
                                ? dataGets.CashRegisterList
                                : CheckASCorDesc.Equals("Descending")
                                    ? Order.GetOrderByCashRegister((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                                    : Order.GetOrderByCashRegister((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
                            cashRegister.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "simCard":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            SimCardWindows simCard = new SimCardWindows
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.SelectedId
                            };
                            simCard.SelectSimCard = CheckASCorDesc == null
                                ? dataGets.SimCardList
                                : CheckASCorDesc.Equals("Descending")
                                    ? Order.GetOrderBySimCard((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                                    : Order.GetOrderBySimCard((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
                            simCard.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "phoneBook":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            PhoneBookWindows phoneBook = new PhoneBookWindows
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.SelectedId
                            };
                            phoneBook.SelectPhoneBook = CheckASCorDesc == null
                            ? dataGets.PhoneBookList
                            : CheckASCorDesc.Equals("Descending")
                                ? Order.GetOrderByPhoneBook((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                                : Order.GetOrderByPhoneBook((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
                            phoneBook.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "holder":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            PeopleWindow holder = new PeopleWindow
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.SelectedId,
                                People = NameNavigationItem
                            };
                            holder.SelectHolder = CheckASCorDesc == null
                            ? dataGets.HolderList
                            : CheckASCorDesc.Equals("Descending")
                                ? Order.GetOrderByHolder((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                                : Order.GetOrderByHolder((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
                            holder.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "user":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            PeopleWindow user = new PeopleWindow
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.SelectedId,
                                People = NameNavigationItem
                            };
                            user.SelectUser = CheckASCorDesc == null
                           ? dataGets.UserList
                           : CheckASCorDesc.Equals("Descending")
                               ? Order.GetOrderByUser((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                               : Order.GetOrderByUser((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
                            user.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "ie":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            IndWindow individual = new IndWindow
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.SelectedId,
                                People = NameNavigationItem
                            };
                            individual.SelectInd = CheckASCorDesc == null
                           ? dataGets.IndividualEntrepreneurList
                           : CheckASCorDesc.Equals("Descending")
                               ? Order.GetOrderByIndividual((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                               : Order.GetOrderByIndividual((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
                            individual.Show();
                        }
                        else
                        {
                            MessageBox.Show("Выберите строку для изменения");
                        }
                        break;
                    case "waybill":
                        if (dataGets.SelectedXIndex >= 0)
                        {
                            WaybillWindow waybill = new WaybillWindow
                            {
                                SelectData = "GET",
                                SelectIndex = dataGets.SelectedXIndex,
                                SelectId = dataGets.SelectedId
                            };
                            waybill.SelectWaybill = CheckASCorDesc == null
                           ? dataGets.WaybillList
                           : CheckASCorDesc.Equals("Descending")
                               ? Order.GetOrderByWaybill((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag)
                               : Order.GetOrderByWaybill((App.Current as App).ConnectionString, CheckASCorDesc, CheckTag);
                            waybill.Show();
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
                logFile.WriteLogAsync(ex.Message, "EditButton_Click");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConnectItem.Header.Equals("Подключено") && ConnectItem.IsChecked)
                {
                    if (dataGets.SelectedXIndex >= 0)
                    {
                        if (MessageBox.Show("Вы точно хотите удалить этот элемент.", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            switch (NameNavigationItem)
                            {
                                case "printer":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
                                    dataGets.PrinterList = Get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);
                                    UpdateTable(NameNavigationItem);
                                    break;
                                case "cartrides":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
                                    dataGets.CartridgesList = Get.GetCartridges((App.Current as App).ConnectionString, "ALL", 0);
                                    UpdateTable(NameNavigationItem);
                                    break;
                                case "cashRegister":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
                                    dataGets.CashRegisterList = Get.GetCashRegister((App.Current as App).ConnectionString, "ALL", 0);
                                    UpdateTable(NameNavigationItem);
                                    break;
                                case "simCard":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
                                    dataGets.SimCardList = Get.GetSimCard((App.Current as App).ConnectionString, "ALL", 0);
                                    UpdateTable(NameNavigationItem);
                                    break;
                                case "phoneBook":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
                                    dataGets.PhoneBookList = Get.GetPhoneBook((App.Current as App).ConnectionString, "ALL", 0);
                                    UpdateTable(NameNavigationItem);
                                    break;
                                case "holder":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
                                    dataGets.HolderList = Get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);
                                    UpdateTable(NameNavigationItem);
                                    break;
                                case "user":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
                                    dataGets.UserList = Get.GetUser((App.Current as App).ConnectionString, "ALL", 0);
                                    UpdateTable(NameNavigationItem);
                                    break;
                                case "ie":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
                                    dataGets.IndividualEntrepreneurList = Get.GetIndividual((App.Current as App).ConnectionString, "ALL", 0);
                                    UpdateTable(NameNavigationItem);
                                    break;
                                case "waybill":
                                    Delete.DeleteDataElement((App.Current as App).ConnectionString, dataGets.SelectedId, NameNavigationItem);
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

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "DeleteButton_Click");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConnectItem.Header.Equals("Подключено") && ConnectItem.IsChecked)
                {
                    CheckASCorDesc = null;
                    CheckTag = null;
                    switch (NameNavigationItem)
                    {
                        case "printer":
                            dataGets.PrinterList = Get.GetPrinter((App.Current as App).ConnectionString, "ALL", 0);
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

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "UpdateButton_Click");
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
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine(ex.InnerException);
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
