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
    /// Interaction logic for PhoneBookWindows.xaml
    /// </summary>
    public partial class PhoneBookWindows : Window
    {
        private AddElement add = new AddElement();
        private UpdateElement update = new UpdateElement();
        private Regex regex = new Regex(@"([A-Za-z0-9-\])}[{(,=\/~`@!#+№;%$:^&?*_|><\\\s]+)");
        private LogFile logFile = new LogFile();
        public PhoneBookWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public string SelectData { get; set; }
        public int SelectIndex { get; set; }
        public int SelectId { get; set; }
        internal ObservableCollection<PhoneBook> SelectPhoneBook { get; set; }
        private void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] phoneBooks = { FirstNameTextBox.Text, LastNameTextBox.Text, MiddleNameTextBox.Text,
                PostTextBox.Text, LocationNumberTextBox.Text, MobileNumberMaskedTextBox.Text};

                if (SelectData.Equals("ADD")) { add.AddDataElement((App.Current as App).ConnectionString, phoneBooks, "phoneBook"); }

                if (SelectData.Equals("UPDATE")) { update.UpdateDataElement((App.Current as App).ConnectionString, phoneBooks, SelectId, "phoneBook"); }

                FirstNameTextBox.Text = string.Empty;
                LastNameTextBox.Text = string.Empty;
                MiddleNameTextBox.Text = string.Empty;
                PostTextBox.Text = string.Empty;
                LocationNumberTextBox.Text = string.Empty;
                MobileNumberMaskedTextBox.Text = string.Empty;

            }
            catch (Exception ex)
            {
               logFile.WriteLogAsync(ex.Message, "PhoneBookContentDialog_ContentDialog_PrimaryButtonClick");
            }
        }
        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            MiddleNameTextBox.Text = string.Empty;
            PostTextBox.Text = string.Empty;
            LocationNumberTextBox.Text = string.Empty;
            MobileNumberMaskedTextBox.Text = string.Empty;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                if (SelectData.Equals("GET"))
                {
                    FirstNameTextBox.Text = SelectPhoneBook[SelectIndex].FirstName;
                    LastNameTextBox.Text = SelectPhoneBook[SelectIndex].LastName;
                    MiddleNameTextBox.Text = SelectPhoneBook[SelectIndex].MiddleName;
                    PostTextBox.Text = SelectPhoneBook[SelectIndex].Post;
                    LocationNumberTextBox.Text = SelectPhoneBook[SelectIndex].InternalNumber;
                    MobileNumberMaskedTextBox.Text = SelectPhoneBook[SelectIndex].MobileNumber;
                    SelectData = "UPDATE";
                }

            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "PhoneBookContentDialog_ContentDialog_Opened");
            }
        }

        private void LastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstNameTextBox.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(FirstNameTextBox.Text);
            FirstNameTextBox.Text = regex.Replace(FirstNameTextBox.Text, "");
        }

        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LastNameTextBox.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(LastNameTextBox.Text);
            LastNameTextBox.Text = regex.Replace(LastNameTextBox.Text, "");
        }

        private void MiddleNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            MiddleNameTextBox.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(MiddleNameTextBox.Text);
            MiddleNameTextBox.Text = regex.Replace(MiddleNameTextBox.Text, "");
        }

        private void PostTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regexPost = new Regex(@"([A-Z2-9\])}[{(,=\/~`@!#+№;%$:^&?*_|><\\]+)");
            PostTextBox.Text = regexPost.Replace(PostTextBox.Text, "");
        }
    }
}
