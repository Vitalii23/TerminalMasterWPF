using System;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using TerminalMasterWPF.Logging;
using System.Collections.ObjectModel;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.Model.People;

namespace TerminalMasterWPF.ViewModel
{
    class AddElement
    {
        private LogFile logFile = new LogFile();
        private DataContext dataContext = new DataContext((App.Current as App).ConnectionString);

        public void AddDataElement(string[] element, string items)
        {

            try
            {
                switch (items)
                {
                    case "cartrides":
                        //dataContext.GetTable<Cartridge>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    case "phoneBook":
                        //dataContext.GetTable<PhoneBook>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    case "printer":
                        //dataContext.GetTable<Printer>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    case "holder":
                        //dataContext.GetTable<Holder>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    case "user":
                        //dataContext.GetTable<User>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    case "ie":
                        //dataContext.GetTable<IndividualEntrepreneur>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    case "cashRegister":
                        //dataContext.GetTable<CashRegister>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    case "simCard":
                        //dataContext.GetTable<SimCard>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    case "waybill":
                        //dataContext.GetTable<Waybill>().InsertOnSubmit(element);
                        dataContext.SubmitChanges();
                        break;
                    default:
                        break;
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