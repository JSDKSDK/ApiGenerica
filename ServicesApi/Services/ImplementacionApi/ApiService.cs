using AccessControl.Models;
using AccessControl.Services;
using DAOApi;
using DeviceDetectorNET;
using ModelsApi.Models;
using ModelsSonarApi.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using ServicesApi.Services.InterfacesApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApi.Services.ImplementacionApi
{
    public class ApiService : IApiService
    {
        private readonly IJwtHandler _jwt;
        private readonly ConexionOracle _conexionOracle;

        public ApiService(IJwtHandler jwt, ConexionOracle conexion)
        {
            _jwt = jwt;
            _conexionOracle = conexion;

        }

        public string auth(ModelLogin usuario)
        {
            JsonWebToken token = new JsonWebToken();
            try
            {
                if (_conexionOracle.openConnection())
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {

                        List<OracleParameter> parameters = new List<OracleParameter>();
                        parameters.Add(new OracleParameter("PA_NOEMPLEADO", OracleDbType.Varchar2, usuario.Usuario, ParameterDirection.Input));
                        parameters.Add(new OracleParameter("PA_CONTRASENIA", OracleDbType.Varchar2, usuario.Password, ParameterDirection.Input));
                        parameters.Add(new OracleParameter
                        {
                            ParameterName = "VL_POCURSOR",
                            Direction = ParameterDirection.Output,
                            OracleDbType = OracleDbType.RefCursor
                        });
                        parameters.Add(new OracleParameter
                        {
                            ParameterName = "VL_PIORESPONSECODE",
                            Direction = ParameterDirection.Output,
                            OracleDbType = OracleDbType.Int32
                        });

                        parameters.Add(new OracleParameter
                        {
                            ParameterName = "VL_PCORESPONSEMESSAG",
                            Size = 32767,
                            Direction = ParameterDirection.Output,
                            OracleDbType = OracleDbType.Varchar2
                        });

                        OracleDataReader reader = _conexionOracle.ExcuteStoreProcedureAny("PA_USUARIO.SP_VALIDATEUSER",parameters);

                        if (reader.Read())
                        {
                            token = _jwt.Create(usuario.Usuario);
                            return token.AccessToken.ToString();
                        }
                        else
                        {
                            return Constants.CredencialesErroneas;
                        }

                    }
                }
                else
                {
                    return Constants.ErrorConexion;
                }

            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
