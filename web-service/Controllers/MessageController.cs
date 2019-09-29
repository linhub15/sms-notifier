using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Entities;
using Notifier.Core.Interfaces;
using Notifier.Infrastructure;

namespace Notifier.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly SmsApi _smsApi;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
            _smsApi = new SmsApi();
        }
        
        [HttpGet]
        public IActionResult Messages()
        {
            var messages = _messageService.Get();
            return Ok(messages);
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult Message(string id)
        {
            var message = _messageService.Get(id);
            return Ok(message);
        }

        [HttpPost]
        public IActionResult Create(Message message)
        {
            _messageService.Create(message);
            return Ok(message);
        }
        
        [HttpPost("send-message")]
        public IActionResult SendMessage()
        {
            _smsApi.SendMessage();
            return Ok();
        }
    }
}