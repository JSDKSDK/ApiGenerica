using AccessControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Services
{
    public class AccessService : IAccessService
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly ITokenManager _manager;

        public AccessService(IJwtHandler jwtHandler, ITokenManager manager)
        {
            _jwtHandler = jwtHandler;
            _manager = manager;
        }

        public JsonWebToken ActiveToken(string username)
        {
            var user = new { Username = username };
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }
            var jwt = _jwtHandler.Create(user.Username);
            return jwt;
        }

        public void RevokeToken()
        {
            //_manager.Deactivate();
        }
    }
}
