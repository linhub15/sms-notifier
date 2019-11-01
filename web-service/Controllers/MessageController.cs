using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Models;
using Notifier.Core.Interfaces;
using Notifier.Core.Dtos;

namespace Notifier.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
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
            _messageService.Schedule(message);
            return Ok(message);
        }

        [HttpPut]
        public IActionResult UpdateContent(UpdateContentDto dto)
        {
            var message = _messageService
                .UpdateContent(dto.MessageId, dto.MessageContent);
            if (message == null)
            {
                return UnprocessableEntity("The message was already sent to the subscribers, you can't modify the content!");
            }
            return Ok(message);
        }
    }
}