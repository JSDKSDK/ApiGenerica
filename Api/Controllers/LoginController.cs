using Microsoft.AspNetCore.Mvc;
using ModelsApi.Models;
using ModelsSonarApi.Models;
using ServicesApi.Services.InterfacesApi;
using System.Net;

namespace Api.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly IApiService _service;
        public LoginController(IApiService service) => _service = service;


        [HttpPost("01")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public  IActionResult auth([FromBody] ModelLogin usuario)
        {
            IActionResult? Status =null;
            if (usuario == null)
            {
                Status = Unauthorized();
            }
            else
            {
                var token = _service.auth(usuario);
                if ((token != Constants.CredencialesErroneas) && (token != Constants.ErrorConexion))
                {
                    Response.Headers.Add("Authorization", $"Bearer {token}");
                    Status = Ok();
                }
                else
                {
                    Status= Unauthorized();
                }
            }
            return Status;
        }
    }
}
