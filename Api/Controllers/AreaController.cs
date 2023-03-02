using Microsoft.AspNetCore.Mvc;
using ModelsSonarApi.Models;

namespace Api.Controllers
{
    [Route("area")]
    public class AreaController : Controller
    {
        [HttpGet("01")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getArea()
        {
            var mensaje = "servicio funcionando correctamente";
                return Ok(mensaje);
        }
    }
}
