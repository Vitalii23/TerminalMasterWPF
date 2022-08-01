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
using TerminalMasterWPF.Model;
using TerminalMasterWPF.ViewModel;

namespace TerminalMasterWPF.ElementContentDialog
{
    /// <summary>
    /// Interaction logic for PeopleWindow.xaml
    /// </summary>
    public partial class PeopleWindow : Window
    {
        private AddElement add = new AddElement();
        private UpdateElement update = new UpdateElement();
        private Regex regex = new Regex(@"([A-Za-z0-9-\])}[{(,=/~`@!#№;%$:^&?*_|><\\\s]+)");
        private LogFile logFile = new LogFile();
        public PeopleWindow()
        {
            InitializeComponent();
            string[] status = { "Рабочий", "Уволен", "Стажировка", "Неизвестно" };
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AddComboxItem(status, StatusComboBox);
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
        internal ObservableCollection<User> SelectUser { get; set; }
        internal ObservableCollection<Holder> SelectHolder { get; set; }
        private void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string statusValue = (string)StatusComboBox.SelectedValue;
                string[] peoples = { LastNameTextBox.Text, FirstNameTextBox.Text, MiddleNameTextBox.Text, MobileNumberMaskedTextBox.Text, statusValue };

                if (SelectData.Equals("ADD")) { add.AddDataElement((App.Current as App).ConnectionString, peoples, People); }

                if (SelectData.Equals("UPDATE")) { update.UpdateDataElement((App.Current as App).ConnectionString, peoples, SelectId, People); }

                FirstNameTextBox.Text = string.Empty;
                LastNameTextBox.Text = string.Empty;
                MiddleNameTextBox.Text = string.Empty;
                Close();

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, People + "_ContentDialog_PrimaryButtonClick");
            }
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            MiddleNameTextBox.Text = string.Empty;
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (People.Equals("holder"))
                {
                    PeopleCD.Title = "Владельцы";
                    if (SelectData.Equals("GET"))
                    {
                        LastNameTextBox.Text = SelectHolder[SelectIndex].LastName;
                        FirstNameTextBox.Text = SelectHolder[SelectIndex].FirstName;
                        MiddleNameTextBox.Text = SelectHolder[SelectIndex].MiddleName;
                        MobileNumberMaskedTextBox.Text = SelectHolder[SelectIndex].Number;
                        StatusComboBox.SelectedValue = SelectHolder[SelectIndex].Status;
                        SelectData = "UPDATE";
                    }
                }

                if (People.Equals("user"))
                {
                    PeopleCD.Title = "Пользователи";
                    if (SelectData.Equals("GET"))
                    {
                        LastNameTextBox.Text = SelectUser[SelectIndex].LastName;
                        FirstNameTextBox.Text = SelectUser[SelectIndex].FirstName;
                        MiddleNameTextBox.Text = SelectUser[SelectIndex].MiddleName;
                        MobileNumberMaskedTextBox.Text = SelectUser[SelectIndex].Number;
                        StatusComboBox.SelectedValue = SelectUser[SelectIndex].Status;
                        SelectData = "UPDATE";
                    }
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, People + "_PeopleContentDialog_Opened");
            }
        }
    }
}
