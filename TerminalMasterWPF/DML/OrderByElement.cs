using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.Model.People;

namespace TerminalMasterWPF.DML
{
    class OrderByElement
    {
        private LogFile logFile = new LogFile();
        private DataContext dataContext;

        public ObservableCollection<Cartridge> GetOrderByCartridges(string sort, string element)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<Cartridge> query = null;
                if (sort.Equals("Ascending"))
                {
                    // query = dataContext.GetTable<Cartridge>().OrderBy(cartridge => cartridge.);
                }

                if (sort.Equals("Descending"))
                {
                    // query = dataContext.GetTable<Cartridge>().OrderByDescending(cartridge => cartridge.);
                }


                ObservableCollection<Cartridge> cartridges = new ObservableCollection<Cartridge>(query);
                return cartridges;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetOrderByCartridges");
            }
            return null;
        }

        public ObservableCollection<CashRegister> GetOrderByCashRegister(string sort, string element)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                ObservableCollection<CashRegister> cashRegisters = null;
                if (sort.Equals("Ascending"))
                {
                    if (element.Equals("holder"))
                    {
                        var query = from cash in dataContext.GetTable<CashRegister>()
                                    join holder in dataContext.GetTable<Holder>() on cash.IdHolder equals holder.Id
                                    join user in dataContext.GetTable<User>() on cash.IdUser equals user.Id
                                    orderby holder.LastName
                                    select new
                                    {
                                        cash,
                                        holder,
                                        user
                                    };
                        cashRegisters = new ObservableCollection<CashRegister>((IEnumerable<CashRegister>)query);
                    }
                    else if (element.Equals("user"))
                    {
                        var query = from cash in dataContext.GetTable<CashRegister>()
                                    join holder in dataContext.GetTable<Holder>() on cash.IdHolder equals holder.Id
                                    join user in dataContext.GetTable<User>() on cash.IdUser equals user.Id
                                    orderby user.LastName
                                    select new
                                    {
                                        cash,
                                        holder,
                                        user
                                    };
                        cashRegisters = new ObservableCollection<CashRegister>((IEnumerable<CashRegister>)query);
                    }
                    else
                    {
                        //var query = from cash in dataContext.GetTable<CashRegister>()
                        //            join holder in dataContext.GetTable<Holder>() on cash.IdHolder equals holder.Id
                        //            join user in dataContext.GetTable<User>() on cash.IdUser equals user.Id
                        //            orderby holder.
                        //            select new
                        //            {
                        //                cash,
                        //                holder,
                        //                user
                        //            };
                        //cashRegisters = new ObservableCollection<CashRegister>((IEnumerable<CashRegister>)query);
                    }

                }

                if (sort.Equals("Descending"))
                {
                    if (element.Equals("holder"))
                    {
                        var query = from cash in dataContext.GetTable<CashRegister>()
                                    join holder in dataContext.GetTable<Holder>() on cash.IdHolder equals holder.Id
                                    join user in dataContext.GetTable<User>() on cash.IdUser equals user.Id
                                    orderby holder.LastName descending
                                    select new
                                    {
                                        cash,
                                        holder,
                                        user
                                    };
                        cashRegisters = new ObservableCollection<CashRegister>((IEnumerable<CashRegister>)query);
                    }
                    else if (element.Equals("user"))
                    {
                        var query = from cash in dataContext.GetTable<CashRegister>()
                                    join holder in dataContext.GetTable<Holder>() on cash.IdHolder equals holder.Id
                                    join user in dataContext.GetTable<User>() on cash.IdUser equals user.Id
                                    orderby user.LastName descending
                                    select new
                                    {
                                        cash,
                                        holder,
                                        user
                                    };
                        cashRegisters = new ObservableCollection<CashRegister>((IEnumerable<CashRegister>)query);
                    }
                    else
                    {
                        //var query = from cash in dataContext.GetTable<CashRegister>()
                        //            join holder in dataContext.GetTable<Holder>() on cash.IdHolder equals holder.Id
                        //            join user in dataContext.GetTable<User>() on cash.IdUser equals user.Id
                        //            orderby user. descending
                        //            select new
                        //            {
                        //                cash,
                        //                holder,
                        //                user
                        //            };
                        //cashRegisters = new ObservableCollection<CashRegister>((IEnumerable<CashRegister>)query);
                    }
                }
                return cashRegisters;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetOrderByCashRegister");
            }
            return null;
        }

        public ObservableCollection<CountersPage> GetOrderByCountersPage(string sort, string element)

        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<CountersPage> query = null;
                if (sort.Equals("Ascending"))
                {
                    //var query = from counter in dataContext.GetTable<CountersPage>()
                    //            join printer in dataContext.GetTable<Printer>() on counter.IdPrinter equals printer.Id
                    //            orderby element
                    //            select new
                    //            {
                    //                counter,
                    //                printer
                    //            };
                }

                if (sort.Equals("Descending"))
                {
                    //var query = from counter in dataContext.GetTable<CountersPage>()
                    //            join printer in dataContext.GetTable<Printer>() on counter.IdPrinter equals printer.Id
                    //            orderby element descending
                    //            select new
                    //            {
                    //                counter,
                    //                printer
                    //            };
                }


                ObservableCollection<CountersPage> countersPages = new ObservableCollection<CountersPage>(query);

                return countersPages;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetOrderByCartridges");
            }
            return null;
        }

        public ObservableCollection<Holder> GetOrderByHolder(string sort, string element)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<Holder> query = null;
                if (sort.Equals("Ascending"))
                {
                    //query = dataContext.GetTable<Holder>().OrderBy(holder => holder.);
                }

                if (sort.Equals("Descending"))
                {
                    //query = dataContext.GetTable<Holder>().OrderByDescending(holder => holder.);
                }

                ObservableCollection<Holder> holders = new ObservableCollection<Holder>();

                return holders;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetOrderByHolder");
            }
            return null;
        }

        public ObservableCollection<IndividualEntrepreneur> GetOrderByIndividual(string sort, string element)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<IndividualEntrepreneur> query = null;
                if (sort.Equals("Ascending"))
                {
                    //query = dataContext.GetTable<IndividualEntrepreneur>().OrderBy(ind => ind.);
                }

                if (sort.Equals("Descending"))
                {
                    //query = dataContext.GetTable<IndividualEntrepreneur>().OrderByDescending(ind => ind.);
                }

                var individuals = new ObservableCollection<IndividualEntrepreneur>(query);

                return individuals;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetOrderByIndividual");
            }
            return null;
        }

        public ObservableCollection<PhoneBook> GetOrderByPhoneBook(string sort, string element)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<PhoneBook> query = null;
                if (sort.Equals("Ascending"))
                {
                    //query = dataContext.GetTable<PhoneBook>().OrderBy(phone => phone.);
                }

                if (sort.Equals("Descending"))
                {
                    //query = dataContext.GetTable<PhoneBook>().OrderByDescending(phone => phone.);
                }

                ObservableCollection<PhoneBook> phoneBooks = new ObservableCollection<PhoneBook>(query);
                return phoneBooks;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetOrderByPhoneBook");
            }
            return null;
        }

        public ObservableCollection<Printer> GetOrderByPrinter(string sort, string element)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<Printer> query = null;
                if (sort.Equals("Ascending"))
                {
                    //query = dataContext.GetTable<Printer>().OrderBy(printer => printer.);
                }

                if (sort.Equals("Descending"))
                {
                    //query = dataContext.GetTable<Printer>().OrderByDescending(printer => printer.);
                }

                ObservableCollection<Printer> printers = new ObservableCollection<Printer>(query);
                return printers;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine(eSql.Message);
                logFile.WriteLogAsync(eSql.Message, "GetOrderByPrinter");
            }
            return null;
        }

        public ObservableCollection<SimCard> GetOrderBySimCard(string sort, string element)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                ObservableCollection<SimCard> simCards = null;
                if (sort.Equals("Ascending"))
                {
                    var query = from simcard in dataContext.GetTable<SimCard>()
                                join ind in dataContext.GetTable<IndividualEntrepreneur>() on simcard.IdIndividual equals ind.Id
                                join cash in dataContext.GetTable<CashRegister>() on simcard.IdCashRegister equals cash.Id
                                orderby element
                                select new
                                {
                                    simcard,
                                    ind,
                                    cash
                                };
                    simCards = new ObservableCollection<SimCard>((IEnumerable<SimCard>)query);
                }

                if (sort.Equals("Descending"))
                {
                    var query = from simcard in dataContext.GetTable<SimCard>()
                                join ind in dataContext.GetTable<IndividualEntrepreneur>() on simcard.IdIndividual equals ind.Id
                                join cash in dataContext.GetTable<CashRegister>() on simcard.IdCashRegister equals cash.Id
                                orderby element descending
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
                logFile.WriteLogAsync(eSql.Message, "GetOrderBySimCard");
            }
            return null;
        }

        public ObservableCollection<User> GetOrderByUser(string sort, string element)
        {
            try
            {
                {
                    dataContext = new DataContext((App.Current as App).ConnectionString);
                    IEnumerable<User> query = null;
                    if (sort.Equals("Ascending"))
                    {
                        //query = dataContext.GetTable<User>().OrderBy(user => user.);
                    }

                    if (sort.Equals("Descending"))
                    {
                        //query = dataContext.GetTable<User>().OrderByDescending(user => user.);
                    }

                    ObservableCollection<User> users = new ObservableCollection<User>();
                    return users;
                }
            }

            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetOrderByUser");
            }
            return null;
        }

        public ObservableCollection<Waybill> GetOrderByWaybill(string sort, string element)
        {
            try
            {
                dataContext = new DataContext((App.Current as App).ConnectionString);
                IEnumerable<Waybill> query = null;
                if (sort.Equals("Ascending"))
                {
                    if (element.Equals("holder"))
                    {
                        //query = dataContext.GetTable<Waybill>().OrderBy(Waybill => Waybill.LastName);
                    }
                    else
                    {
                        //query = dataContext.GetTable<Waybill>().OrderBy(Waybill => Waybill.);
                    }

                }

                if (sort.Equals("Descending"))
                {
                    if (element.Equals("holder"))
                    {
                        //query = dataContext.GetTable<Waybill>().OrderByDescending(Waybill => Waybill.LastName);

                    }
                    else
                    {
                        //query = dataContext.GetTable<Waybill>().OrderByDescending(Waybill => Waybill.);
                    }
                }

                ObservableCollection<Waybill> waybills = new ObservableCollection<Waybill>();
                return waybills;
            }
            catch (Exception eSql)
            {
                logFile.WriteLogAsync(eSql.Message, "GetOrderByWaybill");
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
