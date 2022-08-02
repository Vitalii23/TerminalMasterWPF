using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TerminalMasterWPF.DML;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.ViewModel;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for WaybillWindow.xaml
    /// </summary>
    public partial class WaybillWindow : Window
    {
        AddElement add = new AddElement();
        UpdateElement update = new UpdateElement();
        OrderByElement orderBy = new OrderByElement();
        private LogFile logFile = new LogFile();
        private ObservableCollection<Holder> holders;
        private string pdfFile;
        public WaybillWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            holders = orderBy.GetOrderByHolder((App.Current as App).ConnectionString, "Ascending", "last_name");

            FilePDFButton.IsEnabled = true;

            for (int i = 0; i < holders.Count; i++)
            {
                HolderComboBox.Items.Add(holders[i].LastName + " " + holders[i].FirstName + " " + holders[i].MiddleName);
            }
        }


        public string SelectData { get; set; }
        public int SelectIndex { get; set; }
        public int SelectId { get; set; }
        internal ObservableCollection<Waybill> SelectWaybill { get; set; }

        private void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string holderValue = (string)HolderComboBox.SelectedValue;

                string[] waybills = { NameDocumentTextBox.Text, NumberDocumentTextBox.Text, NumberSuppliersTextBox.Text, DateDocumentDatePicker.Text, FileNameTextblock.Text };
                int[] Ids = new int[] { holders[HolderComboBox.SelectedIndex].Id };

                if (SelectData.Equals("ADD"))
                {
                    add.AddDataElement((App.Current as App).ConnectionString, waybills, Ids, pdfFile, "waybill");
                }

                if (SelectData.Equals("UPDATE"))
                {
                    update.UpdateDataElement((App.Current as App).ConnectionString, waybills, Ids, SelectId, "waybill");
                }

                NameDocumentTextBox.Text = string.Empty;
                NumberDocumentTextBox.Text = string.Empty;
                NumberSuppliersTextBox.Text = string.Empty;
                Close();
            }
            catch (Exception ex)
            {
               logFile.WriteLogAsync(ex.Message, "WaybillContentDialog_ContentDialog_PrimaryButtonClick");
            }
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            NameDocumentTextBox.Text = string.Empty;
            NumberDocumentTextBox.Text = string.Empty;
            NumberSuppliersTextBox.Text = string.Empty;
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectData.Equals("GET"))
                {
                    NameDocumentTextBox.Text = SelectWaybill[SelectIndex].NameDocument;
                    NumberDocumentTextBox.Text = SelectWaybill[SelectIndex].NumberDocument;
                    NumberSuppliersTextBox.Text = SelectWaybill[SelectIndex].NumberSuppliers;
                    DateDocumentDatePicker.Text = SelectWaybill[SelectIndex].DateDocument.ToString();
                    FileNameTextblock.Text = SelectWaybill[SelectIndex].FileName;
                    HolderComboBox.SelectedValue = SelectWaybill[SelectIndex].Holder;
                    FilePDFButton.IsEnabled = false;
                    SelectData = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "WaybillContentDialog_ContentDialog_Opened");
            }
        }

        private void FilePDFButton_Click(object sender, RoutedEventArgs e)
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
                    pdfFile = @"(SELECT * FROM  OPENROWSET(BULK '" + openFile.FileName + "', SINGLE_BLOB) AS file_pdf)";
                    FileNameTextblock.Text = NameDocumentTextBox.Text + "(" + NumberDocumentTextBox.Text + ")";
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "WaybillContentDialog_ContentDialog_Opened");
            }
        }
    }
}