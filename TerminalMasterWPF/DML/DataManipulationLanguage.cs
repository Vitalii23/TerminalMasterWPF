using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Windows.Data;
using TerminalMasterWPF.Logging;
using TerminalMasterWPF.Model;

namespace TerminalMasterWPF.DML
{
    class DataManipulationLanguage<T> : IDataManipulationLanguage<T> where T : class
    {
        private DataContext dataContext = new DataContext((App.Current as App).ConnectionString);
        private LogFile log = new LogFile();

        public async void Add(T element)
        {
            try
            {
                dataContext.GetTable<T>().InsertOnSubmit(element);
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                await log.WriteLogAsync(e.Message, "Add");
            }
        }

        public bool Delete(T element)
        {
            try
            {
                dataContext.GetTable<T>().Attach(element);
                dataContext.GetTable<T>().DeleteOnSubmit(element);
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                log.WriteLogAsync(e.Message, "Delete");
            }
            return true;
        }

        public T Get(int value)
        {
            return Get(typeof(Int64), "ID", value);
        }

        public T Get(string value)
        {
            return Get(typeof(String), "ID", value);
        }

        private T Get(Type type, string v, object value)
        {
            T result = null;
            IQueryable<T> queryableData = dataContext.GetTable<T>().AsQueryable<T>();
            if (queryableData != null)
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "dataManipulationLanguage");
                Expression left = Expression.Property(parameterExpression, GetPropertyInfo(v));
                Expression right = Expression.Constant(value, type);
                Expression predicateBody = Expression.Equal(left, right);
                MethodCallExpression whereCallExpression = Expression.Call(typeof(Queryable), "Where",
                    new Type[] { queryableData.ElementType },
                    queryableData.Expression,
                    Expression.Lambda<Func<T, bool>>(predicateBody, new ParameterExpression[] { parameterExpression }));
                IQueryable<T> results = queryableData.Provider.CreateQuery<T>(whereCallExpression);
                foreach (T item in results)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public IList<T> List()
        {
            return dataContext.GetTable<T>().ToList();
        }

        public void OrderBy(T element, bool trigger)
        {
            //if (trigger)
            //{
            //    dataContext.GetTable<T>().OrderBy(element);
            //}
            //else
            //{
            //    dataContext.GetTable<T>().OrderByDescending(element);
            //}
        }

        public async void Update(T element)
        {
            try
            {
                dataContext.GetTable<T>().Attach(element);
                dataContext.Refresh(RefreshMode.KeepCurrentValues, element);
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                await log.WriteLogAsync(e.Message, "Update");
            }

        }

        private PropertyInfo GetPropertyInfo(string property)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo result = null;
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.Equals(properties))
                {
                    result = info;
                    break;
                }
            }
            return result;
        }

        public ObservableCollection<CashRegister> GetCashRegistersList()
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
                                    CashRegister cashRegister = new CashRegister();
                                    cashRegister.Id = reader.GetInt32(0);
                                    cashRegister.NameDevice = reader.GetString(1);
                                    cashRegister.Brand = reader.GetString(2);
                                    cashRegister.FactoryNumber = reader.GetString(3);
                                    cashRegister.SerialNumber = reader.GetString(4);
                                    cashRegister.PaymentNumber = reader.GetString(5);
                                    cashRegister.DateReception = reader.GetDateTime(6);
                                    cashRegister.DateEndFiscalMemory = reader.GetDateTime(7);
                                    cashRegister.DateKeyActivationFiscalDataOperator = reader.GetDateTime(8);
                                    cashRegister.Location = reader.GetString(9);
                                    cashRegister.IdEmployees = reader.GetInt32(10);
                                    cashRegister.HolderCashRegister = reader.GetString(11);
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
                                    SimCard simcard = new SimCard();
                                    simcard.Id = reader.GetInt32(0);
                                    simcard.NameTerminal = reader.GetString(1);
                                    simcard.Operator = reader.GetString(2);
                                    simcard.IdentNumber = reader.GetString(3);
                                    simcard.Brand = reader.GetString(4);
                                    simcard.TypeDevice = reader.GetString(5);
                                    simcard.TMS = reader.GetString(6);
                                    simcard.ICC = reader.GetString(7);
                                    simcard.Status = reader.GetString(8);
                                    simcard.IdIndividual = reader.GetInt32(9);
                                    simcard.IdCashRegister = reader.GetInt32(10);
                                    simcard.IndividualEntrepreneur = reader.GetString(11);
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
                                    Holder holder = new Holder();
                                    holder.Id = reader.GetInt32(0);
                                    holder.FullNameHolder = reader.GetString(1);
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
                log.WriteLogAsync(eSql.Message, "GetCashRegister");
            }
            return null;
        }
    }
}