using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Services
{
    public interface ICredentialManager
    {
        bool addRegister(string key, string value);
        bool removeRegister(string key);
        bool registerExists(string value);
    }
}
