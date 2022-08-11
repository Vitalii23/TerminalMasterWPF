using System;
using System.Data.SqlClient;
using System.Diagnostics;
using TerminalMasterWPF.Logging;

namespace TerminalMasterWPF.ViewModel
{
    class UpdateElement
    {
        private LogFile logFile = new LogFile();
        public async void UpdateDataElement(string connection, string[] element, int id, string items)
        {
            try
            {
                string AddQuery = null;

                switch (items)
                {
                    case "cartrides":
                        AddQuery = "UPDATE dbo.Cartrides SET brand = '" + element[0] + 
                            "', model = '" + element[1] + 
                            "', vendor_code = '" + element[2] +
                            "', status = '" + element[3] + 
                            "' WHERE id = " + id;
                        break;
                    case "phoneBook":
                        AddQuery = "UPDATE dbo.PhoneBook SET first_name = '" + element[0] + 
                            "', last_name = '" + element[1] + 
                            "', middle_name = '" + element[2] + 
                            "', post = '" + element[3] + 
                            "', internal_number = '" + element[4]+ 
                            "', mobile_number = '" + element[5] + 
                            "' WHERE id = " +  id;
                        break;
                    case "holder":
                        AddQuery = "UPDATE dbo.Holder SET last_name = '" + element[0] +
                            "', first_name = '" + element[1] +
                            "', middle_name = '" + element[2] +
                            "', number = '" + element[3] +
                            "', status = '" + element[4] +
                            "'  WHERE id = " + id;
                        break;
                    case "user":
                        AddQuery = "UPDATE dbo.UserDevice SET last_name = '" + element[0] +
                            "', first_name = '" + element[1] + 
                            "', middle_name = '" + element[2] +
                            "', number = '" + element[3] +
                            "', status = '" + element[4] +
                            "'  WHERE id = " + id;
                        break;
                    case "ie":
                        AddQuery = "UPDATE dbo.IndividualEntrepreneur SET SET last_name = '" + element[0] +
                            "', first_name = '" + element[1] +
                            "', middle_name = '" + element[2] +
                            "', psrnie = '" + element[3] +
                            "', tin = '" + element[4] +
                            "'  WHERE id = " + id;
                        break;
                    case "printer":
                        AddQuery = "UPDATE dbo.Printer SET brand = '" + element[0] +
                            "', model = '" + element[1] +
                            "', cartridge = '" + element[2] +
                            "', name_port = '" + element[3] +
                            "', location = '" + element[4] +
                            "', status = '" + element[5] +
                           "', vendor_code = '" + element[6] +
                           "', counters = '" + element[7] +
                           "', date = '" + element[8] +
                           "' WHERE id = " + id;
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
                Debug.WriteLine("Error: " + eSql.Message);
                await logFile.WriteLogAsync(eSql.Message, items + "_UpdateDataElement");
            }

        }

        public async void UpdateDataElement(string connection, string[] element, int[] ids, int id, string items)
        {
            try
            {
                string AddQuery = null;


                if (items.Equals("simCard")) 
                {
                    AddQuery = "UPDATE dbo.SimCard SET operator = '" + element[0] +
                        "', identifaction_number =  '" + element[1] +
                        "', type_device =  '" + element[2] +
                        "', tms =  '" + element[3] +
                        "', icc =  '" + element[4] +
                        "', status =  '" + element[5] +
                        "', id_individual_entrepreneur = " + ids[0] +
                        ", id_cashRegister = " + ids[1] +
                        " WHERE id = " + id;
                }


                if (items.Equals("cashRegister"))
                {
                    AddQuery = "UPDATE dbo.CashRegister SET name = '" + element[0] +
                        "', brand = '" + element[1] +
                        "', factory_number =  '" + element[2] +
                        "', serial_number =  '" + element[3] +
                        "', payment_number =  '" + element[4] +
                        "', date_reception =  '" + element[5] +
                        "', date_end_fiscal_memory =  '" + element[6] +
                        "', date_key_activ_fisc_data =  '" + element[7] +
                        "', location =  '" + element[8] +
                        "', id_holder =  '" + ids[0] +
                        "', id_user = '" + ids[1] +
                        "' WHERE Id = " + id;
                }

                if (items.Equals("waybill"))
                {
                    AddQuery = "UPDATE dbo.Waybill SET name_document = '" + element[0] +
                        "', number_document =  '" + element[1] +
                        "', number_suppliers =  '" + element[2] +
                        "', date_document =  '" + element[3] +
                        "', file_name =  '" + element[4] +
                        "', id_holder =  '" + ids[0] +
                        "' WHERE Id = " + id;
                }

                if (items.Equals("countersPage"))
                {
                    AddQuery = "UPDATE dbo.CountersPage SET printed_page_counter = '" + element[0] +
                        "', date =  '" + element[1] +
                        "', id_printer =  '" + ids[0] +
                        "' WHERE Id = " + id;
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
                await logFile.WriteLogAsync(eSql.Message, items + "_UpdateDataElement");
            }

        }
    }
}
