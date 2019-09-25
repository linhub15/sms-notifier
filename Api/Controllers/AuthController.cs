using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Dtos;
using Notifier.Core.Interfaces;

namespace Notifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthenticator _authenticator;
        public AuthController(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }
        
        [HttpPost("phone-number")]
        public IActionResult PostPhoneNumber([FromBody] string phoneNumber)
        {
            _authenticator.RequestOneTimeCode(phoneNumber);
            return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateDto dto)
        {
            string token = "header.payload.signature";
            return Ok(token);
        }
    }
}