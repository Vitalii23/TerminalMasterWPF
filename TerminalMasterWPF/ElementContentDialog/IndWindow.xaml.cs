using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model.People;
using TerminalMasterWPF.ViewModel;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for IndWindow.xaml
    /// </summary>
    public partial class IndWindow : Window
    {
        private AddElement add = new AddElement();
        private UpdateElement update = new UpdateElement();
        private Regex regex = new Regex(@"([A-Za-z0-9-\])}[{(,=/~`@!#№;%$:^&?*_|><\\\s]+)");
        private LogFile logFile = new LogFile();
        public IndWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public void AddComboxItem(string[] text, ComboBox combo)
        {
            for (int i = 0; i < text.Length; i++)
            {
                combo.Items.Add(text[i]);
            }
        }
        public string SelectData { get; set; }
        public int SelectIndex { get; set; }
        public int SelectId { get; set; }
        public string People { get; set; }
        internal ObservableCollection<IndividualEntrepreneur> SelectInd { get; set; }
        private void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] ie = { FirstNameTextBox.Text, LastNameTextBox.Text, MiddleNameTextBox.Text, PSRNIETextBox.Text, TINTextBox.Text, };

                if (SelectData.Equals("ADD")) { add.AddDataElement(ie, People); }

                if (SelectData.Equals("UPDATE")) { update.UpdateDataElement((App.Current as App).ConnectionString, ie, SelectId, People); }

                FirstNameTextBox.Text = string.Empty;
                LastNameTextBox.Text = string.Empty;
                MiddleNameTextBox.Text = string.Empty;
                PSRNIETextBox.Text = string.Empty;
                TINTextBox.Text = string.Empty;
                Close();
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "IndContentDialog_ContentDialog_PrimaryButtonClick");
            }
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            MiddleNameTextBox.Text = string.Empty;
            PSRNIETextBox.Text = string.Empty;
            TINTextBox.Text = string.Empty;
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (People.Equals("ie"))
                {
                    if (SelectData.Equals("GET"))
                    {
                        LastNameTextBox.Text = SelectInd[SelectIndex].LastName;
                        FirstNameTextBox.Text = SelectInd[SelectIndex].FirstName;
                        MiddleNameTextBox.Text = SelectInd[SelectIndex].MiddleName;
                        PSRNIETextBox.Text = SelectInd[SelectIndex].PSRNIE;
                        TINTextBox.Text = SelectInd[SelectIndex].TIN;
                        SelectData = "UPDATE";
                    }
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "IndContentDialog_ContentDialog_Opened");
            }
        }

        private void LastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MiddleNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
