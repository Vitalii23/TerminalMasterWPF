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
    /// Interaction logic for CartridgeWindow.xaml
    /// </summary>
    public partial class CartridgeWindow : Window
    {
        private AddElement add = new AddElement();
        private UpdateElement update = new UpdateElement();
        private LogFile logFile = new LogFile();
        public CartridgeWindow()
        {
            InitializeComponent();

            string[] brand = { "Kyocera", "Sakura", "HP LaserJat", "NetProduct" };
            AddComboxItem(brand, BrandComboBox);

            string[] model = {  "TK-160", "TK-170/172", "TK-475", "TK-1140", "TK-1150", "TK-1200", "TK-3190",
                "P3055dn", "CE505X/CRG719H", "7553X", "N-CE505A", "(05X)CE505X", "(53X) Q7553X", "(85A)CE285", "DV-1140E", "DK-150", "DK-170" };
            AddComboxItem(model, ModelComboBox);

            string[] status = { "Заправлен", "Не заправлен", "Сервисе", "Не исправно" };
            AddComboxItem(status, StatusComboBox);
        }
        public string SelectData { get; set; }
        public int SelectIndex { get; set; }
        public int SelectId { get; set; }
        public void AddComboxItem(string[] text, ComboBox combo)
        {
            for (int i = 0; i < text.Length; i++)
            {
                combo.Items.Add(text[i]);
            }
        }
        internal ObservableCollection<Cartridge> SelectCartrides { get; set; }

        private void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string brandValue = (string)BrandComboBox.SelectedValue;
                string modelValue = (string)ModelComboBox.SelectedValue;
                string statusValue = (string)StatusComboBox.SelectedValue;

                string[] cartridges = { brandValue, modelValue, VendorCodeTextBox.Text, statusValue };

                if (SelectData.Equals("ADD")) { add.AddDataElement((App.Current as App).ConnectionString, cartridges, "cartrides"); }

                if (SelectData.Equals("UPDATE")) { update.UpdateDataElement((App.Current as App).ConnectionString, cartridges, SelectId, "cartrides"); }

                VendorCodeTextBox.Text = string.Empty;

                Close();
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "Cartridge_ContentDialog_PrimaryButtonClick");
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
                    BrandComboBox.SelectedValue = SelectCartrides[SelectIndex].Brand;
                    ModelComboBox.SelectedValue = SelectCartrides[SelectIndex].Model;
                    VendorCodeTextBox.Text = SelectCartrides[SelectIndex].VendorCode;
                    StatusComboBox.SelectedValue = SelectCartrides[SelectIndex].Status;
                    SelectData = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "Cartridge_ContentDialog_Opened");
            }
        }
    }
}
