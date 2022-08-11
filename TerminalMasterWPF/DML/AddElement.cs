using System;
using System.Data.SqlClient;
using System.Diagnostics;
using TerminalMasterWPF.Logging;

namespace TerminalMasterWPF.ViewModel
{
    class AddElement
    {
        private LogFile logFile = new LogFile();

        public void AddDataElement(string connection, string[] element, string items)
        {
            try
            {
                string AddQuery = null;
                string values = null;

                for (int i = 0; i < element.Length; i++)
                {
                    if (i == 0)
                    {
                        values += "('" + element[i] + "','";
                    }
                    else if (i == element.Length - 1)
                    {
                        values += element[i] + "')";
                    }
                    else
                    {
                        values += element[i] + "','";
                    }
                }

                switch (items)
                {
                    case "cartrides":
                        AddQuery = "INSERT INTO dbo.Cartrides (brand, model, vendor_code, status) VALUES " + values;
                        break;
                    case "phoneBook":
                        AddQuery = "INSERT INTO dbo.PhoneBook (last_name, first_name, middle_name, post, internal_number, mobile_number) VALUES " + values;
                        break;
                    case "printer":
                        AddQuery = "INSERT INTO dbo.Printer (brand, model, cartridge, name_port, location, status, vendor_code, counters, date) VALUES " + values;
                        break;
                    case "holder":
                        AddQuery = "INSERT INTO dbo.Holder (last_name, first_name, middle_name, number, status) VALUES " + values;
                        break;
                    case "user":
                        AddQuery = "INSERT INTO dbo.UserDevice (last_name, first_name, middle_name, number, status) VALUES " + values;
                        break;
                    case "ie":
                        AddQuery = "INSERT INTO dbo.IndividualEntrepreneur (last_name, first_name, middle_name, psrnie, tin) VALUES " + values;
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
                logFile.WriteLogAsync(eSql.Message, items + "_AddDataElement");
            }

        }

        public void AddDataElement(string connection, string[] element, int[] id,  string items)
        {
            try
            {
                string AddQuery = null;
                string values = null;

                for (int i = 0; i < element.Length; i++)
                {
                    if (i == 0)
                    {
                        values += "('" + element[i] + "','";
                    }
                    else if (i == element.Length - 1)
                    {
                        values += element[i] + "',";
                    }
                    else
                    {
                        values += element[i] + "','";
                    }
                }

                if (items.Equals("cashRegister"))
                {
                    AddQuery = "INSERT INTO dbo.CashRegister (name, brand, factory_number, serial_number, payment_number, date_reception, date_end_fiscal_memory, date_key_activ_fisc_data,  location, id_holder, id_user) VALUES " + values  + id[0] + "," + id[1] + ")";
                }

                if (items.Equals("simCard"))
                {
                    AddQuery = "INSERT INTO dbo.SimCard (operator, identifaction_number, type_device, tms, icc, status, id_individual_entrepreneur, id_cashRegister) VALUES " + values + id[0] + "," + id[1] + ")";
                }

                if (items.Equals("simCard"))
                {
                    AddQuery = "INSERT INTO dbo.CountersPage (printed_page_counter, date, id_printer) VALUES " + values + id[0] + ")";
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
                logFile.WriteLogAsync(eSql.Message, items + "_AddDataElement");
            }

        }

        public void AddDataElement(string connection, string[] element, int[] id, string path, string items)
        {
            try
            {
                string AddQuery = null;
                string values = null;

                for (int i = 0; i < element.Length; i++)
                {
                    if (i == 0)
                    {
                        values += "('" + element[i] + "','";
                    }
                    else if (i == element.Length - 1)
                    {
                        values += element[i] + "',";
                    }
                    else
                    {
                        values += element[i] + "','";
                    }
                }

                if (items.Equals("waybill"))
                {
                    AddQuery = "INSERT INTO dbo.Waybill (name_document, number_document, number_suppliers, date_document, file_name, file_pdf, id_holder) VALUES " + values + path + ", " + id[0] + ")";
                }

                SqlConnection connect = new SqlConnection(connection);
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
                logFile.WriteLogAsync(eSql.Message, items + "_AddDataElement");
            }

        }

    }
}