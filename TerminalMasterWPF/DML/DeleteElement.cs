using System;
using System.Data.SqlClient;
using System.Diagnostics;
using TerminalMasterWPF.Logging;

namespace TerminalMasterWPF.ViewModel
{
    class DeleteElement
    {
        LogFile logFile = new LogFile();
        public async void DeleteDataElement(string connection, int id, string items)
        {
            try
            {
                string AddQuery = null;

                switch (items)
                {
                    case "cartrides":
                        AddQuery = "DELETE FROM dbo.Cartrides WHERE id = " + id;
                        break;
                    case "cashRegister":
                        AddQuery = "DELETE FROM dbo.CashRegister WHERE id = " + id;
                        break;
                    case "phoneBook":
                        AddQuery = "DELETE FROM dbo.PhoneBook WHERE id = " + id;
                        break;
                    case "printer":
                        AddQuery = "DELETE FROM dbo.Printer WHERE id = " + id;
                        break;
                    case "simCard":
                        AddQuery = "DELETE FROM dbo.SimCard WHERE id = " + id;
                        break;
                    case "holder":
                        AddQuery = "DELETE FROM dbo.Holder WHERE id = " + id;
                        break;
                    case "user":
                        AddQuery = "DELETE FROM dbo.UserDevice WHERE id = " + id;
                        break;
                    case "ie":
                        AddQuery = "DELETE FROM dbo.IndividualEntrepreneur WHERE id = " + id;
                        break;
                    case "waybill":
                        AddQuery = "DELETE FROM dbo.Waybill WHERE id = " + id;
                        break;
                    default:
                        break;
                }

                var connect = new SqlConnection(connection);
                connect.Open();
                if (connect.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand cmd = connect.CreateCommand();
                    cmd.CommandText = AddQuery;
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                }
            }
            catch (Exception eSql)
            {
                await logFile.WriteLogAsync(eSql.Message, "DeleteDataElement");
            }

        }
    }
}
