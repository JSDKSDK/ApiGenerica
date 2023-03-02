using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("status")]
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("00")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string Status()
        {

            return "Servicio Funcionando Correctamente";
        }
    }
}
