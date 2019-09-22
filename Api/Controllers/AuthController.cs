using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Dtos;

namespace Notifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("it works");
        }
        
        [HttpPost("phone-number")]
        public IActionResult PostPhoneNumber([FromBody] string phoneNumber)
        {
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