using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Dtos;
using Notifier.Core.Interfaces;

namespace Notifier.Controllers
{
    [Route("api/auth")]
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

        [HttpPost("credentials")]
        public IActionResult Authenticate([FromBody] CredentialsDto dto)
        {
            string token = "header.payload.signature";
            return Ok(token);
        }
    }
}