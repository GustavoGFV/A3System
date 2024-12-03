using A3System.Dbo.Dto.Login;
using A3System.Interface;
using Microsoft.AspNetCore.Mvc;

namespace A3System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto request)
        {
            try
            {
                var result = await _loginService.LoginAsync(request);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }
    }
}