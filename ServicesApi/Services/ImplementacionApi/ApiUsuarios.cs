using ModelsApi.Models;
using ServicesApi.Services.InterfacesApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApi.Services.ImplementacionApi
{
    public class ApiUsuarios : IApiUsuarios
    {
        public ModelApiResponse getUsuarios()
        {
            ModelApiResponse ApiResponseArea = new ModelApiResponse();

            return ApiResponseArea;
        }
    }
}
