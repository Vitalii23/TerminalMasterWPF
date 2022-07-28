using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using TerminalMasterWPF.Model;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for PrinterWindows.xaml
    /// </summary>
    public partial class PrinterWindows : Window
    {
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
            //try
            //{
            //    string status = (string)StatusComboBox.SelectedValue;
            //    string datePrinter = dateTime.Value.Year.ToString() + "-" + dateTime.Value.Month.ToString() + "-" + dateTime.Value.Day.ToString();
            //    string[] printers = { BrandTextBox.Text, ModelTextBox.Text, CartridgeTextBox.Text, NamePortTextBox.Text, LocationTextBox.Text, status, VendorCodeTextBox.Text, PaperTextBox.Text, datePrinter };

            //    if (SelectData.Equals("ADD"))
            //    {
            //        add.AddDataElement((App.Current as App).ConnectionString, printers, "printer");
            //    }

            //    if (SelectData.Equals("UPDATE"))
            //    {
            //        update.UpdateDataElement((App.Current as App).ConnectionString, printers, SelectId, "printer");
            //    }

            //    ModelTextBox.Text = string.Empty;
            //    NamePortTextBox.Text = string.Empty;
            //    LocationTextBox.Text = string.Empty;
            //}
            //catch (Exception e)
            //{
            //    await logFile.WriteLogAsync(e.Message, "Printer_ContentDialog_PrimaryButtonClick");
            //}
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
