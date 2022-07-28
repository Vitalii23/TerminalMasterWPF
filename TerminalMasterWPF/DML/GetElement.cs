using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.Model.People;

namespace TerminalMasterWPF.ViewModel
{
    class GetElement
    {
        LogFile logFile = new LogFile();
        public Dictionary<string, string> values;
        public ObservableCollection<Cartridge> GetCartridges(string connection, string selection, int id)
        {
            string GetCartridgeQuery = null;
            if (selection.Equals("ALL"))
            {
                GetCartridgeQuery = "SELECT id, brand, model, vendor_code, status FROM Cartrides;";
            }

            if (selection.Equals("ONE"))
            {
                GetCartridgeQuery = "SELECT id, brand, model, vendor_code, status FROM Cartrides WHERE id = " + id;
            }


            ObservableCollection<Cartridge> cartridges = new ObservableCollection<Cartridge>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetCartridgeQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var cartridge = new Cartridge
                                    {
                                        Id = reader.GetInt32(0),
                                        Brand = reader.GetString(1),
                                        Model = reader.GetString(2),
                                        VendorCode = reader.GetString(3),
                                        Status = reader.GetString(4)
                                    };
                                    cartridges.Add(cartridge);
                                }
                            }
                        }
                    }
                }
                return cartridges;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine(eSql.Message);
                logFile.WriteLogAsync(eSql.Message, "GetCartridges");
            }
            return null;
        }
        public ObservableCollection<CashRegister> GetCashRegister(string connection, string selection, int id)
        {

            string GetCashRegister = null;
            if (selection.Equals("ALL"))
            {
                GetCashRegister = "SELECT dbo.CashRegister.id, " +
                    "dbo.CashRegister.name, " +
                    "dbo.CashRegister.brand, " +
                    "dbo.CashRegister.factory_number, " +
                    "dbo.CashRegister.serial_number, " +
                    "dbo.CashRegister.payment_number, " +
                    "dbo.CashRegister.date_reception, " +
                    "dbo.CashRegister.date_end_fiscal_memory, " +
                    "dbo.CashRegister.date_key_activ_fisc_data, " +
                    "dbo.CashRegister.location, " +
                    "dbo.CashRegister.id_holder," +
                    "dbo.CashRegister.id_user," +
                    "dbo.Holder.last_name, " +
                    "dbo.Holder.first_name, " +
                    "dbo.Holder.middle_name, " +
                    "dbo.UserDevice.last_name, " +
                    "dbo.UserDevice.first_name, " +
                    "dbo.UserDevice.middle_name " +
                    "FROM dbo.CashRegister " +
                    "INNER JOIN dbo.Holder ON dbo.Holder.id = dbo.CashRegister.id_holder " +
                    "INNER JOIN dbo.UserDevice ON dbo.UserDevice.id = dbo.CashRegister.id_user;";
            }

            if (selection.Equals("ONE"))
            {
                GetCashRegister = "SELECT dbo.CashRegister.id, " +
                    "dbo.CashRegister.name, " +
                    "dbo.CashRegister.brand, " +
                    "dbo.CashRegister.factory_number, " +
                    "dbo.CashRegister.serial_number, " +
                    "dbo.CashRegister.payment_number, " +
                    "dbo.CashRegister.date_reception, " +
                    "dbo.CashRegister.date_key_activ_fisc_data, " +
                    "dbo.CashRegister.date_end_fiscal_memory, " +
                    "dbo.CashRegister.location, " +
                    "dbo.CashRegister.id_holder," +
                    "dbo.CashRegister.id_user," +
                    "dbo.Holder.last_name, " +
                    "dbo.Holder.first_name, " +
                    "dbo.Holder.middle_name, " +
                    "dbo.UserDevice.last_name, " +
                    "dbo.UserDevice.first_name, " +
                    "dbo.UserDevice.middle_name " +
                    "FROM dbo.CashRegister " +
                    "INNER JOIN dbo.Holder ON dbo.Holder.id = dbo.CashRegister.id_holder " +
                    "INNER JOIN dbo.UserDevice ON dbo.UserDevice.id = dbo.CashRegister.id_user " +
                    "WHERE dbo.CashRegister.id =  " + id;
            }

            ObservableCollection<CashRegister> cashRegisters = new ObservableCollection<CashRegister>();
            try
            {
                using (var connect = new SqlConnection(connection))
                {
                    connect.Open();
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetCashRegister;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    CashRegister cashRegister = new CashRegister();
                                    cashRegister.Id = reader.GetInt32(0);
                                    cashRegister.NameDevice = reader.GetString(1);
                                    cashRegister.Brand = reader.GetString(2);
                                    cashRegister.FactoryNumber = reader.GetString(3);
                                    cashRegister.SerialNumber = reader.GetString(4);
                                    cashRegister.PaymentNumber = reader.GetString(5);
                                    cashRegister.DateReception = reader.GetDateTime(6);
                                    cashRegister.DateReceptionString = cashRegister.DateReception.ToShortDateString();
                                    cashRegister.DateEndFiscalMemory = reader.GetDateTime(7);
                                    cashRegister.DateEndFiscalMemoryString = cashRegister.DateEndFiscalMemory.ToShortDateString();
                                    cashRegister.DateKeyActivationFiscalDataOperator = reader.GetDateTime(8);
                                    cashRegister.DateKeyActivationFiscalDataOperatorString = cashRegister.DateKeyActivationFiscalDataOperator.ToShortDateString();
                                    cashRegister.Location = reader.GetString(9);
                                    cashRegister.IdHolder = reader.GetInt32(10);
                                    cashRegister.IdUser = reader.GetInt32(11);
                                    cashRegister.Holder = reader.GetString(12) + " " + reader.GetString(13) + " " + reader.GetString(14);
                                    cashRegister.User = reader.GetString(15) + " " + reader.GetString(16) + " " + reader.GetString(17);
                                    cashRegisters.Add(cashRegister);
                                }
                            }
                        }
                    }
                }
                return cashRegisters;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetCashRegister");
            }
            return null;
        }
        public ObservableCollection<PhoneBook> GetPhoneBook(string connection, string selection, int id)
        {
            string GetPhoneBook = null;
            if (selection.Equals("ALL"))
            {
                GetPhoneBook = "SELECT id, first_name, last_name, middle_name, post, internal_number, mobile_number FROM PhoneBook;";
            }

            if (selection.Equals("ONE"))
            {
                GetPhoneBook = "SELECT id, first_name, last_name, middle_name, post, internal_number, mobile_number FROM PhoneBook WHERE id = " + id;
            }

            var phoneBooks = new ObservableCollection<PhoneBook>();
            try
            {
                using (var connect = new SqlConnection(connection))
                {
                    connect.Open();
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetPhoneBook;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var phoneBook = new PhoneBook()
                                    {
                                        Id = reader.GetInt32(0),
                                        FirstName = reader.GetString(1),
                                        LastName = reader.GetString(2),
                                        MiddleName = reader.GetString(3),
                                        Post = reader.GetString(4),
                                        InternalNumber = Convert.ToString(reader.GetInt32(5)),
                                        MobileNumber = reader.GetString(6)
                                    };
                                    phoneBooks.Add(phoneBook);
                                }
                            }
                        }
                    }
                }
                return phoneBooks;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetPhoneBook");
            }
            return null;
        }
        public ObservableCollection<Printer> GetPrinter(string connection, string selection, int id)
        {
            string GetPrinter = null;
            if (selection.Equals("ALL"))
            {
                GetPrinter = "SELECT id, brand, model, cartridge, name_port, location, status, vendor_code, counters, date FROM Printer;";
            }

            if (selection.Equals("ONE"))
            {
                GetPrinter = "SELECT id, brand, model, cartridge, name_port, location, status, vendor_code, counters, date FROM Printer WHERE id = " + id;
            }

            var printers = new ObservableCollection<Printer>();
            try
            {
                using (var connect = new SqlConnection(connection))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetPrinter;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var printer = new Printer()
                                    {
                                        Id = reader.GetInt32(0),
                                        BrandPrinter = reader.GetString(1),
                                        ModelPrinter = reader.GetString(2),
                                        Cartridge = reader.GetString(3),
                                        NamePort = reader.GetString(4),
                                        LocationPrinter = reader.GetString(5),
                                        Status = reader.GetString(6),
                                        VendorCodePrinter = reader.GetString(7),
                                        Сounters = reader.GetInt32(8),
                                        DatePrinter = reader.GetDateTime(9)
                                    };
                                    printer.DatePrinterString = printer.DatePrinter.ToShortDateString();
                                    printers.Add(printer);
                                }
                            }
                        }
                    }
                }
                return printers;
             }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetPrinter");
            }
            return null;
        }
        public ObservableCollection<SimCard> GetSimCard(string connection, string selection, int id)
        {
            string GetSimCard = null;
            if (selection.Equals("ALL"))
            {
                GetSimCard = "SELECT dbo.SimCard.id, " +
                    "dbo.CashRegister.name, " +
                    "dbo.SimCard.operator, " +
                    "dbo.SimCard.identifaction_number, " +
                    "dbo.CashRegister.brand, " +
                    "dbo.SimCard.type_device, " +
                    "dbo.SimCard.tms, " +
                    "dbo.SimCard.icc, " +
                    "dbo.SimCard.status, " +
                    "dbo.SimCard.id_individual_entrepreneur, " +
                    "dbo.SimCard.id_cashRegister, " +
                    "dbo.IndividualEntrepreneur.last_name, " +
                    "dbo.IndividualEntrepreneur.first_name, " +
                    "dbo.IndividualEntrepreneur.middle_name " +
                    "FROM dbo.SimCard " +
                    "INNER JOIN dbo.IndividualEntrepreneur ON dbo.IndividualEntrepreneur.id = dbo.SimCard.id_individual_entrepreneur " +
                    "INNER JOIN dbo.CashRegister ON dbo.CashRegister.id = dbo.Simcard.id_cashRegister;";
            }

            if (selection.Equals("ONE"))
            {
                GetSimCard = "SELECT dbo.SimCard.id, " +
                  "dbo.CashRegister.name, " +
                  "dbo.SimCard.operator, " +
                  "dbo.SimCard.identifaction_number, " +
                  "dbo.CashRegister.brand, " +
                  "dbo.SimCard.type_device, " +
                  "dbo.SimCard.tms, " +
                  "dbo.SimCard.icc, " +
                  "dbo.SimCard.status, " +
                  "dbo.SimCard.id_individual_entrepreneur, " +
                  "dbo.SimCard.id_cashRegister, " +
                  "dbo.IndividualEntrepreneur.last_name, " +
                  "dbo.IndividualEntrepreneur.first_name, " +
                  "dbo.IndividualEntrepreneur.middle_name " +
                  "FROM dbo.SimCard " +
                  "INNER JOIN dbo.IndividualEntrepreneur ON dbo.IndividualEntrepreneur.id = dbo.SimCard.id_individual_entrepreneur " +
                  "INNER JOIN dbo.CashRegister ON dbo.CashRegister.id = dbo.Simcard.id_cashRegister " +
                  "WHERE dbo.SimCard.id = " + id;
            }

            var simCards = new ObservableCollection<SimCard>();
            try
            {
                using (var connect = new SqlConnection(connection))
                {
                    connect.Open();
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetSimCard;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    SimCard simcard = new SimCard
                                    {
                                        Id = reader.GetInt32(0),
                                        NameTerminal = reader.GetString(1),
                                        Operator = reader.GetString(2),
                                        IdentNumber = reader.GetString(3),
                                        Brand = reader.GetString(4),
                                        TypeDevice = reader.GetString(5),
                                        TMS = reader.GetString(6),
                                        ICC = reader.GetString(7),
                                        Status = reader.GetString(8),
                                        IdIndividual = reader.GetInt32(9),
                                        IdCashRegister = reader.GetInt32(10),
                                        IndividualEntrepreneur = reader.GetString(11) + " " + reader.GetString(12) + " " + reader.GetString(13)
                                    };
                                    simCards.Add(simcard);
                                }
                            }
                        }
                    }
                }
                return simCards;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetSimCard");
            }
            return null;
        }
        public ObservableCollection<IndividualEntrepreneur> GetIndividual(string connection, string selection, int id)
        {
            string GetIndividual = null;
            if (selection.Equals("ALL"))
            {
                GetIndividual = "SELECT id, last_name, first_name, middle_name, psrnie, tin FROM IndividualEntrepreneur;";
            }

            if (selection.Equals("ONE"))
            {
                GetIndividual = "SELECT id, last_name, first_name, middle_name, psrnie, tin FROM IndividualEntrepreneur WHERE id = " + id;
            }

            SqlConnectionStringBuilder bulder = new SqlConnectionStringBuilder
            {
                DataSource = @"tcp:192.168.0.223\SQL223",
                InitialCatalog = @"TerminalMasterDB",
                UserID = @"DVA",
                Password = @"Kolizey$",
                IntegratedSecurity = false,
                ConnectTimeout = 30,
                Encrypt = false,
                TrustServerCertificate = false,
                ApplicationIntent = ApplicationIntent.ReadWrite,
                MultiSubnetFailover = false
            };
            //DataSource = @"KV-SQL-N\SQL223",
            //DataSource = @"tcp:192.168.0.223\SQL223",

            connection = @"Server=192.168.0.223;Trusted_Connection=No;Database=TerminalMasterDB;User Id=DVA;Password=Kolizey$;";

            ObservableCollection<IndividualEntrepreneur> individuals = new ObservableCollection<IndividualEntrepreneur>();
            try
            {
                using (SqlConnection connect = new SqlConnection(bulder.ConnectionString))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetIndividual;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    IndividualEntrepreneur individual = new IndividualEntrepreneur()
                                    {
                                        Id = reader.GetInt32(0),
                                        LastName = reader.GetString(1),
                                        FirstName = reader.GetString(2),
                                        MiddleName = reader.GetString(3),
                                        PSRNIE = reader.GetString(4),
                                        TIN = reader.GetString(5),
                                    };
                                    individuals.Add(individual);
                                }
                            }
                        }
                    }
                    else
                    {
                        connect.InfoMessage += Connect_InfoMessage;
                        connect.StateChange += Connect_StateChange;
                    }
                }
                return individuals;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine(eSql.InnerException);
                Debug.WriteLine(eSql.Message);
                Debug.WriteLine(eSql.StackTrace);
                Debug.WriteLine(eSql.Source);
                //logFile.WriteLogAsync(eSql.Message, "GetIndividual");
            }
            return null;
        }

        private void Connect_StateChange(object sender, StateChangeEventArgs e)
        {
            Console.WriteLine("The current Connection state has changed from {0} to {1}.", e.OriginalState, e.CurrentState);
        }

        private void Connect_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            foreach (SqlError err in e.Errors)
            {
                Debug.WriteLine(
              "The {0} has received a severity {1}, state {2} error number {3}\n" +
              "on line {4} of procedure {5} on server {6}:\n{7}",
               err.Source, err.Class, err.State, err.Number, err.LineNumber,
               err.Procedure, err.Server, err.Message);
            }
        }

        public ObservableCollection<Holder> GetHolder(string connection, string selection, int id)
        {
            string GetHolder = null;
            if (selection.Equals("ALL"))
            {
                GetHolder = "SELECT id, last_name, first_name, middle_name, number, status FROM Holder ;";
            }

            if (selection.Equals("ONE"))
            {
                GetHolder = "SELECT id, last_name, first_name, middle_name, number, status FROM Holder WHERE id = " + id;
            }

            var holders = new ObservableCollection<Holder>();
            try
            {
                using (var connect = new SqlConnection(connection))
                {
                    connect.Open();
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetHolder;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var holder = new Holder()
                                    {
                                        Id = reader.GetInt32(0),
                                        LastName = reader.GetString(1),
                                        FirstName = reader.GetString(2),
                                        MiddleName = reader.GetString(3),
                                        Number = reader.GetString(4),
                                        Status = reader.GetString(5)
                                    };
                                    holders.Add(holder);
                                }
                            }
                        }
                    }
                }
                return holders;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetHolder");
            }
            return null;
        }
        public ObservableCollection<User> GetUser(string connection, string selection, int id)
        {
            string GetUser = null;
            if (selection.Equals("ALL"))
            {
                GetUser = "SELECT id, last_name, first_name, middle_name, number, status FROM UserDevice;";
            }

            if (selection.Equals("ONE"))
            {
                GetUser = "SELECT id, last_name, first_name, middle_name, number, status FROM UserDevice WHERE id = " + id;
            }

            var users = new ObservableCollection<User>();
            try
            {
                using (var connect = new SqlConnection(connection))
                {
                    connect.Open();
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetUser;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var user = new User()
                                    {
                                        Id = reader.GetInt32(0),
                                        LastName = reader.GetString(1),
                                        FirstName = reader.GetString(2),
                                        MiddleName = reader.GetString(3),
                                        Number = reader.GetString(4),
                                        Status = reader.GetString(5)
                                    };
                                    users.Add(user);
                                }
                            }
                        }
                    }
                }
                return users;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetUser");
            }
            return null;
        }
        public ObservableCollection<Waybill> GetWaybill(string connection, string selection, int id)
        {
            string GetWaybill = null;
            if (selection.Equals("ALL"))
            {
                GetWaybill = "SELECT dbo.Waybill.id, " +
                    "dbo.Waybill.name_document, " +
                    "dbo.Waybill.number_document, " +
                    "dbo.Waybill. number_suppliers, " +
                    "dbo.Waybill.date_document, " +
                    "dbo.Waybill.file_name, " +
                    "dbo.Waybill.id_holder, " +
                    "dbo.Holder.last_name, " +
                    "dbo.Holder.first_name, " +
                    "dbo.Holder.middle_name " +
                    "FROM dbo.Waybill " +
                    "INNER JOIN dbo.Holder ON dbo.Holder.id = dbo.Waybill.id_holder ";
            }

            if (selection.Equals("ONE"))
            {
                GetWaybill = "SELECT dbo.Waybill.id, " +
                    "dbo.Waybill.name_document, " +
                    "dbo.Waybill.number_document, " +
                    "dbo.Waybill.number_suppliers, " +
                    "dbo.Waybill.date_document, " +
                    "dbo.Waybill.file_name, " +
                    "dbo.Waybill.id_holder, " +
                    "dbo.Holder.last_name, " +
                    "dbo.Holder.first_name, " +
                    "dbo.Holder.middle_name " +
                    "FROM dbo.Waybill " +
                    "INNER JOIN dbo.Holder ON dbo.Holder.id = dbo.Waybill.id_holder " +
                    "FROM dbo.Waybill WHERE id = " + id;
            }

            var waybills = new ObservableCollection<Waybill>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetWaybill;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Waybill waybill = new Waybill();
                                    waybill.Id = reader.GetInt32(0);
                                    waybill.NameDocument = reader.GetString(1);
                                    waybill.NumberDocument = reader.GetString(2);
                                    waybill.NumberSuppliers = reader.GetString(3);
                                    waybill.DateDocument = reader.GetDateTime(4);
                                    waybill.DateDocumentString = waybill.DateDocument.ToShortDateString();
                                    waybill.FileName = reader.GetString(5);
                                    waybill.FilePDF = GetDocument(waybill.Id, connection);
                                    waybill.IdHolder = reader.GetInt32(6);
                                    waybill.Holder = reader.GetString(7) + " " + reader.GetString(8) + " " + reader.GetString(9);
                                    waybills.Add(waybill);
                                }
                            }
                        }
                    }
                }
                return waybills;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetWaybill");
            }
            return null;
        }
        private static byte[] GetDocument(int documentId, string connection)
        {
            using (SqlConnection connect = new SqlConnection(connection))
            {
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = @" SELECT dbo.Waybill.file_pdf FROM dbo.Waybill WHERE  dbo.Waybill.id = @Id";
                    cmd.Parameters.AddWithValue("@Id", documentId);
                    connect.Open();
                    return cmd.ExecuteScalar() as byte[];
                }
            }
        }
    }
}
