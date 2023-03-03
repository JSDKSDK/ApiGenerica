using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi.Models.Usuarios
{
    public class UserDetailModel
    {
        public decimal? idUsuario { get; set; }
        public string? nombre { get; set; }
        public string? Paterno { get; set; }
        public string? Materno { get; set; }
        public string? noEmpleado { get; set; }
        public decimal? idArea { get; set; }
        public string? area { get; set; }
        public decimal? idPerfil { get; set; }
        public string? perfil { get; set; }
        public string? noEmpleadoCreacion { get; set; }
        public string? usuarioCreacion { get; set; }
        public string? noEmpleadoModificacion { get; set; }
        public string? usuarioModificacion { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public Int16? activo { get; set; }
    }
}
