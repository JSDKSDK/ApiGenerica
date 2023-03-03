using ModelsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApi.Services.InterfacesApi
{
    public interface IApiUsuarios
    {
        #region Get
        public ModelApiResponse getUsuarios();
        #endregion
    }
}
