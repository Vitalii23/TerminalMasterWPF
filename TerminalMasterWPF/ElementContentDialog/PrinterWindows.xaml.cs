using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.ViewModel;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for PrinterWindows.xaml
    /// </summary>
    public partial class PrinterWindows : Window
    {

        private AddElement add = new AddElement();
        private UpdateElement update = new UpdateElement();
        private LogFile logFile = new LogFile();
        public PrinterWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            string[] status = { "В работе", "Сервис", "Списан", "Не исправен" };
            AddComboxItem(status, StatusComboBox);
        }

        public string SelectData { get; set; }
        public int SelectIndex { get; set; }
        public int SelectId { get; set; }
        internal ObservableCollection<Printer> SelectPrinter { get; set; }
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
                string status = (string)StatusComboBox.SelectedValue;
                string[] printers = { BrandTextBox.Text, ModelTextBox.Text, CartridgeTextBox.Text, NamePortTextBox.Text, LocationTextBox.Text, status, VendorCodeTextBox.Text, PaperTextBox.Text, DatePrinterDatePicker.Text };

                if (SelectData.Equals("ADD"))
                {
                    add.AddDataElement((App.Current as App).ConnectionString, printers, "printer");
                }

                if (SelectData.Equals("UPDATE"))
                {
                    update.UpdateDataElement((App.Current as App).ConnectionString, printers, SelectId, "printer");
                }

                ModelTextBox.Text = string.Empty;
                NamePortTextBox.Text = string.Empty;
                LocationTextBox.Text = string.Empty;
                Close();
            }
            catch (Exception ex)
            {
               logFile.WriteLogAsync(ex.Message, "Printer_ContentDialog_PrimaryButtonClick");
            }
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectData.Equals("GET"))
                {
                    BrandTextBox.Text = SelectPrinter[SelectIndex].BrandPrinter;
                    ModelTextBox.Text = SelectPrinter[SelectIndex].ModelPrinter;
                    CartridgeTextBox.Text = SelectPrinter[SelectIndex].Cartridge;
                    NamePortTextBox.Text = SelectPrinter[SelectIndex].NamePort;
                    LocationTextBox.Text = SelectPrinter[SelectIndex].LocationPrinter;
                    StatusComboBox.SelectedValue = SelectPrinter[SelectIndex].Status;
                    VendorCodeTextBox.Text = SelectPrinter[SelectIndex].VendorCodePrinter;
                    PaperTextBox.Text = Convert.ToString(SelectPrinter[SelectIndex].Сounters);
                    DatePrinterDatePicker.Text = SelectPrinter[SelectIndex].DatePrinter.ToString();
                    SelectData = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "Printer_ContentDialog_Opened");
            }
        }
    }
}
