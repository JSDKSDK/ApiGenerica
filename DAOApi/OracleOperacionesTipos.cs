using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOApi
{
    public class OracleOperacionesTipos
    {
    }
    public class ListaDeOracleParameters
    {
        public List<OracleParameter> Parameters { get; set; }

        public ListaDeOracleParameters()
        {
            Parameters = new List<OracleParameter>
            {
                new OracleParameter { ParameterName = "VL_POCURSOR", Direction = ParameterDirection.Output, OracleDbType = OracleDbType.RefCursor },
                new OracleParameter { ParameterName = "VL_PIORESPONSECODE", Direction = ParameterDirection.Output, OracleDbType = OracleDbType.Int32 },
                new OracleParameter { ParameterName = "VL_PCORESPONSEMESSAG", Size = 32767, Direction = ParameterDirection.Output, OracleDbType = OracleDbType.Varchar2 }
            };
        }
    }
}
