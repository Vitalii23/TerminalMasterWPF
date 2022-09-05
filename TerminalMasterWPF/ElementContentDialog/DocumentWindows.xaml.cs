using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using TerminalMasterWPF.Logging;

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

        private async void FileButton_Click(object sender, RoutedEventArgs e)
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
                    file = @"(SELECT * FROM  OPENROWSET(BULK '" + openFile.FileName + "', SINGLE_BLOB) AS file_binary)";
                    FileNameTextblock.Text = NameDocumentTextBox.Text;
                }
            }
            catch (Exception ex)
            {
                await logFile.WriteLogAsync(ex.Message, "FileButton_Click");
            }
        }

        private async void PrimaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectData.Equals("ADD"))
                {
                    string sqlExpression =  $"INSERT INTO dbo.Documents (name_document, date_document, file_binary) VALUES ('{NameDocumentTextBox.Text}','{DateDocumentDatePicker.Text}',{file})";

                    SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString);
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = connect.CreateCommand();
                        cmd.CommandText = sqlExpression;
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                    }
                }

                if (SelectData.Equals("UPDATE"))
                {
                    //string sqlExpression = $"UPDATE Documents (name_document, date_document, file_binary) VALUES (@name, @date, @file)";

                    using (SqlConnection connection = new SqlConnection((App.Current as App).ConnectionString))
                    {
                        await connection.OpenAsync();

                        //SqlCommand command = new SqlCommand(sqlExpression, connection);

                        SqlParameter nameParam = new SqlParameter("@name", NameDocumentTextBox.Text);
                        // command.Parameters.Add(nameParam);

                        SqlParameter dateParam = new SqlParameter("@date", DateDocumentDatePicker.Text);
                        //  command.Parameters.Add(dateParam);

                        SqlParameter fileParam = new SqlParameter("@file", file);
                        //  command.Parameters.Add(fileParam);

                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            SqlCommand cmd = connection.CreateCommand();
                            // cmd.CommandText = sqlExpression;
                            SqlDataReader reader = cmd.ExecuteReader();
                            reader.Read();
                        }
                    }
                }

                Close();
            } catch (Exception ex)
            {
                await logFile.WriteLogAsync(ex.Message, "PrimaryButtonClick_Click");
            }
        }

        private void SecondaryButtonClick_Click(object sender, RoutedEventArgs e)
        {
            NameDocumentTextBox.Text = string.Empty;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectData.Equals("GET"))
                {
                    //NameDocumentTextBox.Text = SelectWaybill[SelectIndex].NameDocument;
                    //DateDocumentDatePicker.Text = SelectWaybill[SelectIndex].DateDocument.ToString();
                    //FileNameTextblock.Text = SelectWaybill[SelectIndex].FileName;
                    FileButton.IsEnabled = false;
                    SelectData = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                logFile.WriteLogAsync(ex.Message, "WaybillContentDialog_ContentDialog_Opened");
            }
        }
    }
}
