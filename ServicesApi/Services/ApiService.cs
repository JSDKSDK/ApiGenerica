using AccessControl.Models;
using AccessControl.Services;
using ModelsSonarApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApi.Services
{
    public class ApiService : IApiService
    {
        private readonly IJwtHandler _jwt;

        public ApiService(IJwtHandler jwt)
        {
            _jwt = jwt;
        }

        public string auth(ModelLogin usuario)
        {
            JsonWebToken token = _jwt.Create(usuario.Usuario);
           // string token = "sdfsdf94154xdxxxrslyuefkeufn";
            return token.AccessToken.ToString();
        }
    }
}
