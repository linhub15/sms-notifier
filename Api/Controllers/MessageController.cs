using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Dtos;

namespace Notifier.Api.Controllers
{
    [ApiController]
    public class MessageController
    {
        [HttpGet("messages")]
        public IActionResult Messages()
        {
            // Get's messages for the given subject token 'sub'
            return Ok()
        }

        [HttpGet("messages/{id:guid}")]
        public IActionResult OneMessage(guid id)
        {
            // get's a single message based on message id
            return Ok()
        }
    }
}