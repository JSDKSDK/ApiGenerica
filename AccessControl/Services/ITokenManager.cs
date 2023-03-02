using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Services
{
    public interface ITokenManager
    {
        bool IsActive();
        //bool Deactivate();
        //bool Activate(string token);
        string[] GetAllowedPaths();
    }
}
