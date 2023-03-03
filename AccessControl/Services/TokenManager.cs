using AccessControl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly ICredentialManager _manager;
        private readonly IHttpContextAccessor _accessor;
        private readonly IOptions<AccessOptions> _accessOptions;

    public TokenManager(
            ICredentialManager manager,
            IHttpContextAccessor accessor,
        IOptions<AccessOptions> accessOptions
            )
        {
            _manager = manager;
            _accessor = accessor;
            _accessOptions = accessOptions;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);
        }
        //public bool Deactivate() => _manager.removeRegister(getCurrentIp());

        public bool IsActive() => _manager.registerExists(getCurrentToken());

        //public bool Activate(string token) => _manager.addRegister(getCurrentIp(), token);

        public string[] GetAllowedPaths() => _accessOptions.Value.AllowedPaths;

        private string getCurrentToken()
        {
            var authorizationHeader = _accessor.HttpContext.Request.Headers["Authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        //private string getCurrentIp() => _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

    }
}

   

