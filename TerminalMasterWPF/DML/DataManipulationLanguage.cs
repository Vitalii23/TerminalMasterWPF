using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TerminalMasterWPF.Logging;

namespace TerminalMasterWPF.DML
{
    class DataManipulationLanguage<T> : IDataManipulationLanguage<T> where T : class
    {
        private DataContext dataContext = new DataContext((App.Current as App).ConnectionString);
        private LogFile log = new LogFile();

        public void Add(T element)
        {
            try
            {
                dataContext.GetTable<T>().InsertOnSubmit(element);
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                log.WriteLogAsync(e.Message, "Add");
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

        public void Update(T element)
        {
            try
            {
                dataContext.GetTable<T>().Attach(element);
                dataContext.Refresh(RefreshMode.KeepCurrentValues, element);
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                log.WriteLogAsync(e.Message, "Update");
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
    }
}
