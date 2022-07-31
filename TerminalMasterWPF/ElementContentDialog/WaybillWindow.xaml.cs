using Microsoft.Win32;
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
        GetElement get = new GetElement();
        private LogFile logFile = new LogFile();
        private ObservableCollection<Holder> holders;
        private string file;
        private string pdfFile;
        public WaybillWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            holders = get.GetHolder((App.Current as App).ConnectionString, "ALL", 0);

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
                    InitialDirectory = "C:\\",
                    FilterIndex = 4,
                    Filter = "jpg image (*.jpg)|*.jpg|jpeg image (*.jpeg)|*.jpeg|png image (*.png)|*.png|pdf file (*.pdf)|*.pdf|",
                    RestoreDirectory = true
                };

                bool? result = openFile.ShowDialog();

                if (result == true)
                {
                    file = openFile.FileName;
                    pdfFile = "(SELECT * FROM  OPENROWSET(BULK '" + file + "', SINGLE_BLOB) AS file_pdf)";
                    FileNameTextblock.Text = file;
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "WaybillContentDialog_ContentDialog_Opened");
            }
        }
    }
}