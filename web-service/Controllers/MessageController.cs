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
        private readonly IMessageSender _messageSender;
        private readonly IMessageScheduler _messageScheduler;
        public MessageController(
            IMessageService messageService,
            IMessageSender messageSender,
            IMessageScheduler messageScheduler)
        {
            _messageService = messageService;
            _messageSender = messageSender;
            _messageScheduler = messageScheduler;
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
            _messageScheduler.Schedule(
                message,
                () => _messageSender.SendMessage(message));

            return Ok(message);
        }
        
        [HttpPost("send-message")]
        public IActionResult SendMessage()
        {
            _messageSender.SendMessage();
            return Ok();
        }
    }
}