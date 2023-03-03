using Microsoft.AspNetCore.Mvc;
using ModelsApi.Models;
using ServicesApi.Services.InterfacesApi;

namespace Api.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IApiUsuarios _service;
        public UserController(IApiUsuarios service) => _service = service;

        [HttpGet("01")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ModelApiResponse getUsuariosAll()
        {
            ModelApiResponse ApiResponseArea = new ModelApiResponse();

           return ApiResponseArea = _service.getUsuarios();

        }
    }
}
