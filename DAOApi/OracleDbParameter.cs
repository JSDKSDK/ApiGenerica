using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOApi
{
    public class OracleDbParameter
    {
        public OracleDbParameter(string parameterName, object value, OracleDbType type)
        {
            ParameterName = parameterName;
            Value = value;
            Type = type;
        }

        public string ParameterName { get; set; }
        public object Value { get; set; }
        public Oracle.ManagedDataAccess.Client.OracleDbType Type { get; set; }
    }
}
