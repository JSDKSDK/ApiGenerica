using Microsoft.AspNetCore.Mvc;
using ModelsSonarApi.Models;
using ServicesApi.Services;

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
            if (usuario == null)
            {

                return Unauthorized();
            }
            else
            {

                var token = _service.auth(usuario);
                
                Response.Headers.Add("Authorization", $"Bearer {token}");
                return Ok();
            }
            // return Ok(usuario);
        }
    }
}
