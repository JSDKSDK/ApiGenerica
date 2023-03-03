using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi.Models
{
    public class ModelApiResponse
    {
        public string Message { get; set; } = "Peticion Exitosa";
        public int StatusCode { get; set; }
        public Object Data { get; set; }
    }
}

