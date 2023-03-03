using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAOApi
{
    public static class OracleDataReaderExtensions
    {
        public static List<T> MapToList<T>(this OracleDataReader reader) where T : new()
        {
            List<T> list = new List<T>();
            T? obj = default;
            while (reader.Read())
            {
                obj = new T();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(reader[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, reader[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            reader.Close();
         return list;
        }
    }
}
