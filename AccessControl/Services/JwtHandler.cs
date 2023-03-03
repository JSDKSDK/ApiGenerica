using AccessControl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _options;
        private readonly ITokenManager _tokenManager;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;


        public JwtHandler(
                        IOptions<JwtOptions> options,
            ITokenManager tokenManager
           
            )
        {
           
            _options = options.Value;
            _tokenManager = tokenManager;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
        }

        public JsonWebToken Create(string usuario)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expiresAppSettinsg = _options.ExpiryMinutes;
            var issueTime = DateTime.Now;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddMinutes(expiresAppSettinsg).Subtract(utc0).TotalSeconds;

            var payload = new JwtPayload
            {
                {"sub", usuario},
                {"iss", _options.Issuer},
                {"iat", iat},
                {"exp", exp},
                {"unique_name", usuario},
            };

            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expires = exp
            };
        }
    
    }
}
