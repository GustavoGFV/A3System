using A3System.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace A3System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private ITokenService _tokenService;

        public TokenController(ITokenService tokenServices) => _tokenService = tokenServices;

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gerar Bearer Token")]
        public IActionResult CreateToken(string id)
        {
            try
            {
                var token = _tokenService.GenerateToken(id, "user");
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }

    }
}
