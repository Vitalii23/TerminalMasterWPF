using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
    /// Interaction logic for DocumentWindows.xaml
    /// </summary>
    public partial class DocumentWindows : Window
    {
        private LogFile logFile = new LogFile();
        private string file;
        public DocumentWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public string SelectData { get; set; }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog
                {
                    //InitialDirectory = @"\\KV-SQL-N\TerminalDataFiles",
                    FilterIndex = 2,
                    Filter = "File image (*.jpeg, *.jpg, .png)|*.jpeg;*.jpg;*.png|" +
                    "Documents file (*.pdf)|*.pdf",
                    RestoreDirectory = true
                };

                bool? result = openFile.ShowDialog();
                if (result == true)
                {
                    file = @"(SELECT * FROM  OPENROWSET(BULK '" + openFile.FileName + "', SINGLE_BLOB) AS file_binary)";
                    FileNameTextblock.Text = NameDocumentTextBox.Text;
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "FileButton_Click");
            }
        }

        private void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            if (SelectData.Equals("ADD"))
            {
                string sqlExpression = $"INSERT INTO Users (name_document, date_document, file_binary) VALUES (@name, @date, @file)";

                using (SqlConnection connection = new SqlConnection((App.Current as App).ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    SqlParameter nameParam = new SqlParameter("@name", NameDocumentTextBox.Text);
                    command.Parameters.Add(nameParam);

                    SqlParameter dateParam = new SqlParameter("@date", DateDocumentDatePicker.Text);
                    command.Parameters.Add(dateParam);

                    SqlParameter fileParam = new SqlParameter("@file", file);
                    command.Parameters.Add(fileParam);
                }
            }

            if (SelectData.Equals("Edit"))
            {
                string sqlExpression = $"INSERT INTO Users (name_document, date_document, file_binary) VALUES (@name, @date, @file)";

                using (SqlConnection connection = new SqlConnection((App.Current as App).ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    SqlParameter nameParam = new SqlParameter("@name", NameDocumentTextBox.Text);
                    command.Parameters.Add(nameParam);

                    SqlParameter dateParam = new SqlParameter("@date", DateDocumentDatePicker.Text);
                    command.Parameters.Add(dateParam);

                    SqlParameter fileParam = new SqlParameter("@file", file);
                    command.Parameters.Add(fileParam);
                }
            }
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            NameDocumentTextBox.Text = string.Empty;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
