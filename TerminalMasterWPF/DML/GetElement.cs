using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.Model.People;

namespace TerminalMasterWPF.ViewModel
{
    class GetElement
    {
        private LogFile logFile = new LogFile();
        private DataContext dataContext;

        public ObservableCollection<Cartridge> GetCartridges(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<Cartridge> query = null;
                if (selection.Equals("ALL"))
                {
                    query = dataContext.GetTable<Cartridge>();
                }

                if (selection.Equals("ONE"))
                {
                    query = dataContext.GetTable<Cartridge>().Where(cartride => cartride.Id == id);
                }

                ObservableCollection<Cartridge> cartridges = new ObservableCollection<Cartridge>(query);

                return cartridges;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine(eSql.Message);
                logFile.WriteLogAsync(eSql.Message, "GetCartridges");
            }
            return null;
        }

        public ObservableCollection<CashRegister> GetCashRegister(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                ObservableCollection<CashRegister> cashRegisters = null;
                if (selection.Equals("ALL"))
                {
                    var query = from cash in dataContext.GetTable<CashRegister>()
                                join holder in dataContext.GetTable<Holder>() on cash.IdHolder equals holder.Id
                                join user in dataContext.GetTable<User>() on cash.IdUser equals user.Id
                                select new
                                {
                                    cash,
                                    holder,
                                    user
                                };
                    cashRegisters = new ObservableCollection<CashRegister>((IEnumerable<CashRegister>)query);
                }

                if (selection.Equals("ONE"))
                {
                    var query = from cash in dataContext.GetTable<CashRegister>()
                                where cash.Id == id
                                join holder in dataContext.GetTable<Holder>() on cash.IdHolder equals holder.Id
                                join user in dataContext.GetTable<User>() on cash.IdUser equals user.Id
                                select new
                                {
                                    cash,
                                    holder,
                                    user
                                };
                    cashRegisters = new ObservableCollection<CashRegister>((IEnumerable<CashRegister>)query);
                }

                return cashRegisters;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetCashRegister");
            }
            return null;
        }
        
        public ObservableCollection<CountersPage> GetCountersPage(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<CountersPage> query = null;
                if (selection.Equals("ALL"))
                {
                    query = dataContext.GetTable<CountersPage>();
                }

                if (selection.Equals("ONE"))
                {
                    query = dataContext.GetTable<CountersPage>().Where(counter => counter.Id == id);
                }

                ObservableCollection<CountersPage> countersPages = new ObservableCollection<CountersPage>(query);

                return countersPages;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetCountersPage");
            }
            return null;
        }
        
        public ObservableCollection<Holder> GetHolder(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<Holder> query = null;
                if (selection.Equals("ALL"))
                {
                    query = dataContext.GetTable<Holder>();
                }

                if (selection.Equals("ONE"))
                {
                    query = dataContext.GetTable<Holder>().Where(holder => holder.Id == id);
                }

                ObservableCollection<Holder> holders = new ObservableCollection<Holder>(query);
                return holders;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetHolder");
            }
            return null;
        }

        public ObservableCollection<IndividualEntrepreneur> GetIndividual(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<IndividualEntrepreneur> query = null;
                if (selection.Equals("ALL"))
                {
                    query = dataContext.GetTable<IndividualEntrepreneur>();
                }

                if (selection.Equals("ONE"))
                {
                    query = dataContext.GetTable<IndividualEntrepreneur>().Where(ind => ind.Id == id);
                }

                ObservableCollection<IndividualEntrepreneur> individuals = new ObservableCollection<IndividualEntrepreneur>(query);

                return individuals;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetIndividual");
            }
            return null;
        }
        
        public ObservableCollection<PhoneBook> GetPhoneBook(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<PhoneBook> query = null;
                if (selection.Equals("ALL"))
                {
                    query = dataContext.GetTable<PhoneBook>();
                }

                if (selection.Equals("ONE"))
                {
                    query = dataContext.GetTable<PhoneBook>().Where(phone => phone.Id == id);
                }

                ObservableCollection<PhoneBook> phoneBooks = new ObservableCollection<PhoneBook>(query);
                return phoneBooks;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetPhoneBook");
            }
            return null;
        }

        public ObservableCollection<Printer> GetPrinter(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<Printer> query = null;
                if (selection.Equals("ALL"))
                {
                    query = dataContext.GetTable<Printer>();
                }

                if (selection.Equals("ONE"))
                {
                    query = dataContext.GetTable<Printer>().Where(printer => printer.Id == id);
                }

                ObservableCollection<Printer> printers = new ObservableCollection<Printer>(query);
                return printers;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetPrinter");
            }
            return null;
        }

        public ObservableCollection<SimCard> GetSimCard(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                ObservableCollection<SimCard> simCards = null;
                if (selection.Equals("ALL"))
                {
                    var query = from simcard in dataContext.GetTable<SimCard>()
                                join ind in dataContext.GetTable<IndividualEntrepreneur>() on simcard.IdIndividual equals ind.Id
                                join cash in dataContext.GetTable<CashRegister>() on simcard.IdCashRegister equals cash.Id
                                select new
                                {
                                    simcard,
                                    ind,
                                    cash
                                };
                    simCards = new ObservableCollection<SimCard>((IEnumerable<SimCard>)query);
                }

                if (selection.Equals("ONE"))
                {
                    var query = from simcard in dataContext.GetTable<SimCard>()
                                where simcard.Id == id
                                join ind in dataContext.GetTable<IndividualEntrepreneur>() on simcard.IdIndividual equals ind.Id
                                join cash in dataContext.GetTable<CashRegister>() on simcard.IdCashRegister equals cash.Id
                                select new
                                {
                                    simcard,
                                    ind,
                                    cash
                                };
                    simCards = new ObservableCollection<SimCard>((IEnumerable<SimCard>)query);
                }

                return simCards;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetSimCard");
            }
            return null;
        }

        public ObservableCollection<User> GetUser(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<User> query = null;
                if (selection.Equals("ALL"))
                {
                    query = dataContext.GetTable<User>();
                }

                if (selection.Equals("ONE"))
                {
                    query = dataContext.GetTable<User>().Where(user => user.Id == id);
                }

                ObservableCollection<User> users = new ObservableCollection<User>(query);
                return users;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetUser");
            }
            return null;
        }

        public ObservableCollection<Waybill> GetWaybill(string selection, int id)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<Waybill> query = null;
                if (selection.Equals("ALL"))
                {
                    query = dataContext.GetTable<Waybill>();
                }

                if (selection.Equals("ONE"))
                {
                    query = dataContext.GetTable<Waybill>().Where(waybill => waybill.Id == id);
                }

                ObservableCollection<Waybill> waybills = new ObservableCollection<Waybill>(query);

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
