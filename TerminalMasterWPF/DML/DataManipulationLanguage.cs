using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.Model.People;

namespace TerminalMasterWPF.DML
{
    class DataManipulationLanguage<T> : IDataManipulationLanguage<T> where T : class
    {
        private LogFile log = new LogFile();

        public async void Add(T element)
        {
            try
            {
                string AddQuery = null;

                switch (element)
                {
                    case Cartridge cart:
                        AddQuery = $"INSERT INTO dbo.Cartrides (brand, model, vendor_code, status) VALUES ('{cart.Brand}', '{cart.Model}', '{cart.VendorCode}', '{cart.Status}')";
                        break;
                    case Employees empl:
                        AddQuery = $"INSERT INTO dbo.Employees (last_name, first_name, middle_name, post, internal_number, mobile_number, status, department) VALUES " +
                            $"('{empl.LastName}', '{empl.FirstName}', '{empl.MiddleName}', '{empl.Post}', '{empl.InternalNumber}', '{empl.MobileNumber}', '{empl.Status}', '{empl.Department}')";
                        break;
                    case Printer print:
                        AddQuery = $"INSERT INTO dbo.Printer (brand, model, cartridge, name_port, location, status, vendor_code) VALUES " +
                            $"('{print.BrandPrinter}', '{print.ModelPrinter}', '{print.Cartridge}', '{print.NamePort}', '{print.LocationPrinter}', '{print.Status}', '{print.VendorCodePrinter}')";
                        break;
                    case CashRegister cash:
                        AddQuery = $"INSERT INTO dbo.CashRegister (name, brand, factory_number, serial_number, payment_number, date_reception, date_end_fiscal_memory, date_key_activ_fisc_data, location, id_employees) VALUES " +
                            $"('{cash.NameDevice}', '{cash.Brand}', '{cash.FactoryNumber}', '{cash.SerialNumber}', '{cash.PaymentNumber}', '{cash.DateReception}', '{cash.DateEndFiscalMemory}', '{cash.DateKeyActivationFiscalDataOperator}', '{cash.Location}', {cash.IdEmployees})";
                        break;
                    case IndividualEntrepreneur ind:
                        AddQuery = $"INSERT INTO dbo.IndividualEntrepreneur (last_name, first_name, middle_name, psrnie, tin) VALUES ('{ind.LastName}', '{ind.FirstName}', '{ind.MiddleName}', '{ind.PSRNIE}', '{ind.TIN}')";
                        break;
                    case SimCard sim:
                        AddQuery = $"INSERT INTO dbo.SimCard (operator, identifaction_number, type_device, tms, icc, status, id_individual_entrepreneur, id_cashRegister) VALUES " +
                            $"('{sim.Operator}', '{sim.IdentNumber}', '{sim.TypeDevice}', '{sim.TMS}', '{sim.ICC}', '{sim.Status}', {sim.IdIndividual}, {sim.IdCashRegister})";
                        break;
                    case Documents doc:
                        AddQuery = $"INSERT INTO dbo.Documents (name_document, file_binary) VALUES ('{doc.NameDocument}', {GetFileName})";
                        break;
                    case CountersPage count:
                        AddQuery = $"INSERT INTO dbo.CountersPage (printed_page_counter, date, condition, id_printer) VALUES ({count.PrintedPageCounter}, '{count.Date}', '{count.Сondition}', {count.IdPrinter})";
                        break;
                    default:
                        break;
                }

                SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString);
                connect.Open();
                if (connect.State == ConnectionState.Open)
                {
                    SqlCommand cmd = connect.CreateCommand();
                    cmd.CommandText = AddQuery;
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                }
            }
            catch (Exception eSql)
            {
                await log.WriteLogAsync(eSql.Message, element.ToString() + " AddDataElement");
            }

        }

        public async void Delete(T element)
        {
            try
            {
                string DeleteQuery = null;

                switch (element)
                {
                    case Cartridge cart:
                        DeleteQuery = $"DELETE FROM dbo.Cartrides WHERE id = {cart.Id}";
                        break;
                    case CashRegister cash:
                        DeleteQuery = $"DELETE FROM dbo.CashRegister WHERE id = {cash.Id}";
                        break;
                    case Employees emp:
                        DeleteQuery = $"DELETE FROM dbo.PhoneBook WHERE id = {emp.Id}";
                        break;
                    case Printer print:
                        DeleteQuery = $"DELETE FROM dbo.Printer WHERE id = {print.Id}";
                        break;
                    case SimCard sim:
                        DeleteQuery = $"DELETE FROM dbo.SimCard WHERE id = {sim.Id}";
                        break;
                    case IndividualEntrepreneur ind:
                        DeleteQuery = $"DELETE FROM dbo.IndividualEntrepreneur WHERE id = {ind.Id}";
                        break;
                    case Documents doc:
                        DeleteQuery = $"DELETE FROM dbo.Documents WHERE id = {doc.Id}";
                        break;
                    case CountersPage count:
                        DeleteQuery = $"DELETE FROM dbo.CountersPage WHERE id = {count.Id}";
                        break;
                    default:
                        break;
                }

                SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString);
                connect.Open();
                if (connect.State == ConnectionState.Open)
                {
                    SqlCommand cmd = connect.CreateCommand();
                    cmd.CommandText = DeleteQuery;
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                }
            }
            catch (Exception eSql)
            {
                await log.WriteLogAsync(eSql.Message, "DeleteDataElement");
            }

        }

        public async void Update(T element)
        {
            try
            {
                string UpdateQuery = null;

                switch (element)
                {
                    case Cartridge cart:
                        UpdateQuery = $"UPDATE dbo.Cartrides SET brand = '{cart.Brand}', model = '{cart.Model}', vendor_code = '{cart.VendorCode}', status = '{cart.Status}' WHERE id = {cart.Id}";
                        break;
                    case Employees emp:
                        UpdateQuery = $"UPDATE dbo.Employees SET first_name = '{emp.FirstName}', last_name = '{emp.LastName}', middle_name = '{emp.MiddleName}', post = '{emp.Post}', internal_number = '{emp.InternalNumber}', " +
                            $"mobile_number = '{emp.MobileNumber}', status = '{emp.Status}', department = '{emp.Department}' WHERE id =  {emp.Id}";
                        break;
                    case IndividualEntrepreneur ind:
                        UpdateQuery = $"UPDATE dbo.IndividualEntrepreneur SET last_name = '{ind.LastName}' + , first_name = '{ind.FirstName}', middle_name = '{ind.MiddleName}', psrnie = '{ind.PSRNIE}', tin = '{ind.TIN}' WHERE id = {ind.Id}";
                        break;
                    case Printer print:
                        UpdateQuery = $"UPDATE dbo.Printer SET brand = '{print.BrandPrinter}', model = '{print.ModelPrinter}', cartridge = '{print.Cartridge}', name_port = '{print.NamePort}' , location = '{print.LocationPrinter}', vendor_code = '{print.VendorCodePrinter}' WHERE id = {print.Id}";
                        break;
                    case SimCard sim:
                        UpdateQuery = $"UPDATE dbo.SimCard SET operator = '{sim.Operator}', identifaction_number = '{sim.IdentNumber}', type_device = '{sim.TypeDevice}', tms = '{sim.TMS}', icc = '{sim.ICC}', status = '{sim.Status}'," +
                            $" id_individual_entrepreneur = {sim.IdIndividual}, id_cashRegister = {sim.IdCashRegister}, WHERE id = {sim.Id}";
                        break;
                    case CashRegister cash:
                        UpdateQuery = $"UPDATE dbo.CashRegister SET name = '{cash.NameDevice}', brand = '{cash.Brand}', factory_number = '{cash.FactoryNumber}', serial_number =  '{cash.SerialNumber}', payment_number = '{cash.PaymentNumber}'," +
                            $" date_reception = '{cash.DateReception}', date_end_fiscal_memory = '{cash.DateEndFiscalMemory}', date_key_activ_fisc_data = '{cash.DateKeyActivationFiscalDataOperator}', location = '{cash.Location}', id_employees = {cash.IdEmployees} WHERE Id = {cash.Id}";
                        break;
                    case Documents doc:
                        UpdateQuery = $"UPDATE dbo.Documents SET name_document = '{doc.NameDocument}', file_binary = {GetFileName} WHERE Id = {doc.Id}";
                        break;
                    case CountersPage count:
                        UpdateQuery = $"UPDATE dbo.CountersPage SET printed_page_counter = '{count.PrintedPageCounter}', date =  '{count.Date}', condition = {count.Сondition}, id_printer = '{count.IdPrinter}'  WHERE Id = {count.Id}";
                        break;
                    default:
                        break;
                }


                SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString);
                connect.Open();
                if (connect.State == ConnectionState.Open)
                {
                    SqlCommand cmd = connect.CreateCommand();
                    cmd.CommandText = UpdateQuery;
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                }
            }
            catch (Exception eSql)
            {
                await log.WriteLogAsync(eSql.Message, element.ToString() + "_UpdateDataElement");
            }

        }

        public ObservableCollection<Printer> GetPrinters()
        {
            string GetPrinter = "SELECT dbo.Printer.id, " +
                                        "dbo.Printer.brand, " +
                                        "dbo.Printer.model, " +
                                        "dbo.Printer.cartridge, " +
                                        "dbo.Printer.name_port, " +
                                        "dbo.Printer.location, " +
                                        "dbo.Printer.status, " +
                                        "dbo.Printer.vendor_code, " +
                                "(dbo.Printer.brand + ' ' + dbo.Printer.model + ' (' + dbo.Printer.vendor_code + ')') as printers " +
                                "FROM Printer;";

            ObservableCollection<Printer> printers = new ObservableCollection<Printer>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
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
                                        FullNamePrinters = reader.GetString(8)
                                    };
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
                log.WriteLogAsync(eSql.Message, "GetPrinter");
            }
            return null;
        }

        public ObservableCollection<CashRegister> GetCashRegisters()
        {
            string GetCashRegister = "SELECT dbo.CashRegister.id, " +
                                     "dbo.CashRegister.name, " +
                                     "dbo.CashRegister.brand, " +
                                     "dbo.CashRegister.factory_number, " +
                                     "dbo.CashRegister.serial_number, " +
                                     "dbo.CashRegister.payment_number, " +
                                     "dbo.CashRegister.date_reception, " +
                                     "dbo.CashRegister.date_end_fiscal_memory, " +
                                     "dbo.CashRegister.date_key_activ_fisc_data, " +
                                     "dbo.CashRegister.location, " +
                                     "dbo.CashRegister.id_employees, " +
                                     "(SELECT TOP (1) (dbo.Employees.last_name + ' ' + dbo.Employees.first_name + ' ' + dbo.Employees.middle_name) " +
                                     "FROM dbo.Employees " +
                                     "WHERE dbo.Employees.id = dbo.CashRegister.id_employees) AS holder " +
                                     "FROM dbo.CashRegister " +
                                     "INNER JOIN dbo.Employees ON dbo.Employees.id = dbo.CashRegister.id_employees; ";

            ObservableCollection<CashRegister> cashRegisters = new ObservableCollection<CashRegister>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetCashRegister;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    CashRegister cashRegister = new CashRegister
                                    {
                                        Id = reader.GetInt32(0),
                                        NameDevice = reader.GetString(1),
                                        Brand = reader.GetString(2),
                                        FactoryNumber = reader.GetString(3),
                                        SerialNumber = reader.GetString(4),
                                        PaymentNumber = reader.GetString(5),
                                        DateReception = reader.GetDateTime(6),
                                        DateEndFiscalMemory = reader.GetDateTime(7),
                                        DateKeyActivationFiscalDataOperator = reader.GetDateTime(8),
                                        Location = reader.GetString(9),
                                        IdEmployees = reader.GetInt32(10),
                                        HolderCashRegister = reader.GetString(11)
                                    };
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
                log.WriteLogAsync(eSql.Message, "GetCashRegister");
            }
            return null;
        }

        public ObservableCollection<SimCard> GetSimCardList()
        {
            string GetSimCard = "SELECT dbo.SimCard.id, " +
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
                                             "(SELECT TOP(1)(dbo.IndividualEntrepreneur.last_name + ' ' + dbo.IndividualEntrepreneur.first_name + ' ' + dbo.IndividualEntrepreneur.middle_name) " +
                                             "FROM dbo.IndividualEntrepreneur " +
                                             "WHERE dbo.IndividualEntrepreneur.id = dbo.SimCard.id_individual_entrepreneur) AS individual " +
                                  "FROM dbo.SimCard " +
                                  "INNER JOIN dbo.IndividualEntrepreneur ON dbo.IndividualEntrepreneur.id = dbo.SimCard.id_individual_entrepreneur " +
                                  "INNER JOIN dbo.CashRegister ON dbo.CashRegister.id = dbo.Simcard.id_cashRegister; ";

            ObservableCollection<SimCard> simcards = new ObservableCollection<SimCard>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
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
                                        IndividualEntrepreneur = reader.GetString(11)
                                    };
                                    simcards.Add(simcard);
                                }
                            }
                        }
                    }
                }
                return simcards;
            }
            catch (Exception eSql)
            {
                log.WriteLogAsync(eSql.Message, "GetSimCard");
            }
            return null;
        }

        public ObservableCollection<Holder> GetHolderList()
        {
            string GetHolder = "SELECT dbo.Employees.id, (dbo.Employees.last_name + ' ' + dbo.Employees.first_name + ' ' + dbo.Employees.middle_name) as holder " +
                         "FROM dbo.Employees " +
                         "WHERE dbo.Employees.status = 'Работает' AND dbo.Employees.post = 'Экспедитор' " +
                         "OR dbo.Employees.post = 'Системный администратор' " +
                         "OR dbo.Employees.post = 'Системный администратор по кассовой технике' " +
                         "ORDER BY dbo.Employees.last_name;";

            ObservableCollection<Holder> holders = new ObservableCollection<Holder>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetHolder;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    Holder holder = new Holder
                                    {
                                        Id = reader.GetInt32(0),
                                        FullNameHolder = reader.GetString(1)
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
                log.WriteLogAsync(eSql.Message, "GetHolderList");
            }
            return null;
        }

        public ObservableCollection<Employees> GetEmployees()
        {
            string GetEmployees = "SELECT dbo.Employees.id, dbo.Employees.last_name, dbo.Employees.first_name, dbo.Employees.middle_name, dbo.Employees.post, dbo.Employees.internal_number, dbo.Employees.mobile_number, dbo.Employees.status, dbo.Employees.department FROM dbo.Employees ";

            ObservableCollection<Employees> employees = new ObservableCollection<Employees>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetEmployees;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    Employees employee = new Employees
                                    {
                                        Id = reader.GetInt32(0),
                                        LastName = reader.GetString(1),
                                        FirstName = reader.GetString(2),
                                        MiddleName = reader.GetString(3),
                                        Post = reader.GetString(4),
                                        InternalNumber = reader.GetString(5),
                                        MobileNumber = reader.GetString(6),
                                        Status = reader.GetString(7),
                                        Department = reader.GetString(8)
                                    };
                                    employees.Add(employee);
                                }
                            }
                        }
                    }
                }
                return employees;
            }
            catch (Exception eSql)
            {
                log.WriteLogAsync(eSql.Message, "GetHolderList");
            }
            return null;
        }

        public ObservableCollection<IndividualEntrepreneur> GetIndividualEntrepreneur()
        {
            string GetHolder = "SELECT dbo.IndividualEntrepreneur.id, " +
                                    "dbo.IndividualEntrepreneur.last_name, " +
                                    "dbo.IndividualEntrepreneur.first_name," +
                                    "dbo.IndividualEntrepreneur.middle_name, " +
                                    "dbo.IndividualEntrepreneur.psrnie, dbo.IndividualEntrepreneur.tin, " +
                                    "('ИП ' + dbo.IndividualEntrepreneur.last_name + ' ' + dbo.IndividualEntrepreneur.first_name + ' ' + dbo.IndividualEntrepreneur.middle_name) as full_name " +
                                    "FROM dbo.IndividualEntrepreneur; ";

            ObservableCollection<IndividualEntrepreneur> individuals = new ObservableCollection<IndividualEntrepreneur>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetHolder;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    IndividualEntrepreneur individual = new IndividualEntrepreneur
                                    {
                                        Id = reader.GetInt32(0),
                                        LastName = reader.GetString(1),
                                        FirstName = reader.GetString(2),
                                        MiddleName = reader.GetString(3),
                                        PSRNIE = reader.GetString(4),
                                        TIN = reader.GetString(5),
                                        FullNameIndividualEntrepreneur = reader.GetString(6)
                                    };
                                    individuals.Add(individual);
                                }
                            }
                        }
                    }
                }
                return individuals;
            }
            catch (Exception eSql)
            {
                log.WriteLogAsync(eSql.Message, "GetIndividualEntrepreneur");
            }
            return null;
        }

        public ObservableCollection<CountersPage> GetCountersPagesList()
        {
            string GetCountersPage = "SELECT dbo.CountersPage.id, " +
                                       "dbo.CountersPage.printed_page_counter, " +
                                       "dbo.CountersPage.date, " +
                                       "dbo.CountersPage.condition, " +
                                       "dbo.CountersPage.id_printer, " +
                                       "(dbo.Printer.brand + ' ' + dbo.Printer.model + ' (' + dbo.Printer.vendor_code + ')') as printers " +
                                    "FROM dbo.CountersPage " +
                                    "INNER JOIN dbo.Printer ON dbo.Printer.id = dbo.CountersPage.id_printer; ";

            ObservableCollection<CountersPage> countersPages = new ObservableCollection<CountersPage>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetCountersPage;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    CountersPage countersPage = new CountersPage
                                    {
                                        Id = reader.GetInt32(0),
                                        PrintedPageCounter = reader.GetInt32(1),
                                        Date = reader.GetDateTime(2),
                                        Сondition = reader.GetString(3),
                                        IdPrinter = reader.GetInt32(4),
                                        Printer = reader.GetString(5)
                                    };
                                    countersPages.Add(countersPage);
                                }
                            }
                        }
                    }
                }
                return countersPages;
            }
            catch (Exception eSql)
            {
                log.WriteLogAsync(eSql.Message, "GetCountersPagesList");
            }
            return null;
        }

        public ObservableCollection<Documents> GetDocuments()
        {
            string GetDocuments = "SELECT dbo.Documents.id, " +
                    "dbo.Documents.name_document, " +
                    "dbo.Documents.file_binary " +
                    "FROM dbo.Documents ";

            ObservableCollection<Documents> documents = new ObservableCollection<Documents>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
                {
                    connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = connect.CreateCommand())
                        {
                            cmd.CommandText = GetDocuments;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Documents document = new Documents
                                    {
                                        Id = reader.GetInt32(0),
                                        NameDocument = reader.GetString(1)
                                    };
                                    document.FileBinary = GetDocument(document.Id);
                                    documents.Add(document);
                                }
                            }
                        }
                    }
                }
                return documents;
            }
            catch (Exception eSql)
            {
                log.WriteLogAsync(eSql.Message, "GetDocuments");
            }
            return null;
        }

        public ObservableCollection<Cartridge> GetCartridges()
        {
            string GetCartridgeQuery = "SELECT dbo.Cartrides.id, dbo.Cartrides.brand, dbo.Cartrides.model, dbo.Cartrides.vendor_code, dbo.Cartrides.status FROM dbo.Cartrides;";
            ObservableCollection<Cartridge> cartridges = new ObservableCollection<Cartridge>();
            try
            {
                using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
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
                log.WriteLogAsync(eSql.Message, "GetCartridges");
            }
            return null;
        }

        public string GetFileName { get; set; }

        public byte[] GetDocument(int documentId)
        {
            using (SqlConnection connect = new SqlConnection((App.Current as App).ConnectionString))
            {
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    cmd.CommandText = @" SELECT dbo.Documents.file_binary FROM dbo.Documents WHERE dbo.Documents.id = @Id";
                    cmd.Parameters.AddWithValue("@Id", documentId);
                    connect.Open();
                    return cmd.ExecuteScalar() as byte[];
                }
            }
        }

    }
}