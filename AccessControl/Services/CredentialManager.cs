using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;

namespace AccessControl.Services
{
    public class CredentialManager : ICredentialManager
    {

        public CredentialManager(){}

        public bool addRegister(string key, string value)
        {
            return false;
        }

        public bool removeRegister(string key)
        {
            return true;
        }

        public bool registerExists(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                if (token!=null)
                {
                    var stream = token;
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stream);
                    if (jsonToken.ValidTo > DateTime.Now)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            return false;
        }

    }
}
