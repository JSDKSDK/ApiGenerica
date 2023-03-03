using AccessControl.Services;
using DAOApi;
using ModelsApi.Models;
using ModelsApi.Models.Usuarios;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using ServicesApi.Services.InterfacesApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;

namespace ServicesApi.Services.ImplementacionApi
{
    public class ApiUsuarios : IApiUsuarios
    {
        private readonly IJwtHandler _jwt;
        private readonly ConexionOracle _conexionOracle;
        private ListaDeOracleParameters _lista;

        public ApiUsuarios(IJwtHandler jwt, ConexionOracle conexion, ListaDeOracleParameters lista)
        {
            _jwt = jwt;
            _conexionOracle = conexion;
            _lista = lista;
        }

        public ModelApiResponse getUsuarios()
        {
            ModelApiResponse ApiResponseUsuarios = new ModelApiResponse();
            List<UserDetailModel > UserDetail = new List<UserDetailModel>();
            try
            {
                if (_conexionOracle.openConnection())
                {
                    _lista.Parameters.Insert(0, new OracleParameter("PA_ACTIVO", OracleDbType.Int16, 1, ParameterDirection.Input));

                    OracleDataReader UserDatailReader = _conexionOracle.ExcuteStoreProcedureAny(OraclePackage.PACKAGE_PA_USUARIOS + ".SP_LISTAUSUARIO", _lista.Parameters);

                    UserDetail = UserDatailReader.MapToList<UserDetailModel>();

                }

                if (UserDetail.Count>0)
                {
                    ApiResponseUsuarios.StatusCode = 0;
                    ApiResponseUsuarios.Data = UserDetail;
                }
                else
                {
                    ApiResponseUsuarios.Message = Constants.PeticionSinDatos;
                }
                _conexionOracle.CloseConnection();

            }
            catch (Exception ex)
            {
                ApiResponseUsuarios.Message += ex.ToString();
                return ApiResponseUsuarios;
               
            }

            return ApiResponseUsuarios;
        }
    }
}