using AccessControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Services
{
    public interface IAccessService
    {
        JsonWebToken ActiveToken(string username);
        void RevokeToken();
    }
}
