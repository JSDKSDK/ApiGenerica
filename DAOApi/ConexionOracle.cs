using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOApi
{
    public class ConexionOracle
    {
        private readonly string connectionString = null;
        private OracleConnection connection = null;
        OracleTransaction transaction = null;
        private readonly string ESQUEMA_DB = ConfigurationManager.AppSettings["BDConnection"];
        private readonly IConfiguration _configuration;

        private readonly IOptions<ModelConexion> _accessOptions;


        public String EsquemaBaseDatos { get { return ESQUEMA_DB; } }
        internal OracleTransaction OracleTransaction { get { return this.transaction; } }

        public ConexionOracle(IOptions<ModelConexion> accessOptions)
        {
            //conexion

            //var conexionSection = _configuration.GetSection("ConnectionStrings");
            //var conexionOptions = new ModelConexion();
            _accessOptions = accessOptions;
            this.connectionString = _accessOptions.Value.cadena;

        }

        public bool openConnection()
        {
            try
            {
                if (this.connection != null && this.connection.State == System.Data.ConnectionState.Open)
                {
                    throw new InvalidOperationException("Sólo se permite una conexión abierta.");
                }
                this.connection = new OracleConnection(this.connectionString);
                this.connection.Open();
                return true;
            }
            catch (Exception ex) 
            {

                this.connection.Close();
                this.connection.Open();
                return false;
            }
        }
        public void CloseConnection()
        {
            if (this.connection != null && this.connection.State == System.Data.ConnectionState.Open)
            {
                this.connection.Close();
            }
        }
        public void StartTransaction()
        {

            if (this.connection == null || this.connection.State != System.Data.ConnectionState.Open) { throw new InvalidOperationException("No se puede inicializar la transacción, se requiere de una conexión activa."); }

            this.transaction = this.connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        public void CommitTransaction()
        {

            if (this.connection == null || this.connection.State != System.Data.ConnectionState.Open) { throw new InvalidOperationException("No existe una conexión activa para la transacción."); }
            if (this.transaction == null) { throw new InvalidOperationException("No se ha iniciado ninguna transación."); }

            this.transaction.Commit();
            this.transaction.Dispose();
            this.transaction = null;
        }

        public void RollbackTransaction()
        {
            if (this.connection == null || this.connection.State != System.Data.ConnectionState.Open) { throw new InvalidOperationException("No existe una conexión activa para la transacción."); }
            if (this.transaction == null) { throw new InvalidOperationException("No se ha iniciado ninguna transación."); }

            this.transaction.Rollback();
            this.transaction.Dispose();
            this.transaction = null;
        }

        public OracleDataReader GetDataReader(string sql, List<OracleParameter> parameters)
        {

            OracleCommand oracleCommand = new OracleCommand();
            OracleDataReader reader;

            oracleCommand.Connection = this.connection;
            oracleCommand.Transaction = this.transaction;
            oracleCommand.CommandText = sql;

            this.AssignParametersGenerico(oracleCommand, parameters);

            reader = oracleCommand.ExecuteReader();

            return reader;
        }

        public int? ExcuteNonQuery(string sql, List<OracleParameter> parameters)
        {

            OracleCommand oracleCommand = new OracleCommand();
            int rowEffected = -1;

            oracleCommand.Connection = this.connection;
            oracleCommand.Transaction = this.transaction;
            oracleCommand.CommandText = sql;

            this.AssignParametersGenerico(oracleCommand, parameters);

            rowEffected = oracleCommand.ExecuteNonQuery();

            return rowEffected;
        }

        public DateTime ObtenerFechaServidor()
        {

            OracleDataReader oracleDataReader = null;
            DateTime fechaActual = DateTime.Now;

            oracleDataReader = this.GetDataReader("SELECT TO_CHAR (SYSDATE, 'YYYY-MM-DD HH24:MI:SS') INTO FechaActual FROM DUAL", null);

            if (oracleDataReader.HasRows)
            {
                while (oracleDataReader.Read())
                {
                    fechaActual = (DateTime)oracleDataReader["FechaActual"];
                }
            }

            if (!oracleDataReader.IsClosed)
            {
                oracleDataReader.Close();
            }

            oracleDataReader.Dispose();

            return fechaActual;
        }
        private void AssignParametersGenerico(OracleCommand oracleCommand, List<OracleParameter> parameters)
        {

            if (parameters != null && parameters.Count > 0)
            {
                foreach (OracleParameter parameter in parameters)
                {
                    oracleCommand.Parameters.Add(parameter);
                }

            }
        }


        public string ExcuteStoreProcedures(string nombreSP, int opcion, string Nombre)
        {
            OracleCommand oracleCommand = new OracleCommand(nombreSP, this.connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            OracleParameter oparam0 = oracleCommand.Parameters.Add("var_opcion", OracleDbType.Int64);
            oparam0.Value = opcion;
            oparam0.Direction = ParameterDirection.Input;

            OracleParameter oparam1 = oracleCommand.Parameters.Add("var_nombre", OracleDbType.Varchar2);
            oparam1.Value = Nombre;
            oparam1.Direction = ParameterDirection.Input;

            OracleParameter oparam3 = oracleCommand.Parameters.Add("var_idTabla", OracleDbType.Int32);
            oparam3.Direction = ParameterDirection.Output;

            OracleParameter oparam4 = oracleCommand.Parameters.Add("var_msj", OracleDbType.Varchar2);
            oparam4.Direction = ParameterDirection.Output;
            oracleCommand.ExecuteNonQuery();

            string id_Tabla = oracleCommand.Parameters["var_idTabla"].Value.ToString();
            return id_Tabla;
        }

        public OracleDataReader ExcuteStoreProcedure_Table(string nombreSP, int opcion, List<OracleParameter> parameters)
        {
            OracleCommand oracleCommand = new OracleCommand(nombreSP, this.connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            this.AssignParametersGenerico(oracleCommand, parameters);
            return oracleCommand.ExecuteReader();
        }

    }
}
