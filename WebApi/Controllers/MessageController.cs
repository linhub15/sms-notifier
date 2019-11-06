using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Entities;
using Notifier.Core.UseCases;
using System;

namespace Notifier.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IUseCaseInteractor<GetMessageRequest, GetMessageResponse> _getMessage;
        private IUseCaseInteractor<ListMessagesRequest, ListMessagesResponse> _listMessages;
        private IUseCaseInteractor<SendMessageRequest, SendMessageResponse> _sendMessage;
        private IUseCaseInteractor<ScheduleMessageRequest, ScheduleMessageResponse> _scheduleMessage;
        private IUseCaseInteractor<ModifyMessageRequest, ModifyMessageResponse> _modifyMessage;
        private IUseCaseInteractor<UnscheduleMessageRequest, UnscheduleMessageResponse> _unscheduleMessage;

        public MessageController(
            IUseCaseInteractor<GetMessageRequest, GetMessageResponse> getMessage,
            IUseCaseInteractor<ListMessagesRequest, ListMessagesResponse> listMessages,
            IUseCaseInteractor<SendMessageRequest, SendMessageResponse> sendMessage,
            IUseCaseInteractor<ScheduleMessageRequest, ScheduleMessageResponse> scheduleMessage,
            IUseCaseInteractor<ModifyMessageRequest, ModifyMessageResponse> modifyMessage,
            IUseCaseInteractor<UnscheduleMessageRequest, UnscheduleMessageResponse> unscheduleMessage)
        {
            _getMessage = getMessage;
            _listMessages = listMessages;
            _sendMessage = sendMessage;
            _scheduleMessage = scheduleMessage;
            _modifyMessage = modifyMessage;
            _unscheduleMessage = unscheduleMessage;
        }

        [HttpGet]
        public IActionResult Messages()
        {
            var response = _listMessages.Handle(new ListMessagesRequest());
            return Ok(response.Messages);
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult Message(string id)
        {
            var request = new GetMessageRequest()
            {
                MessageId = id
            };
            var response = _getMessage.Handle(request);
            return Ok(response.Message);
        }

        [HttpPost("send")]
        public IActionResult Send(Message message)
        {
            var request = new SendMessageRequest()
            {
                Message = message
            };
            var response = _sendMessage.Handle(request);
            // WIP
            return Ok(message);
        }

        [HttpPost("schedule")]
        public IActionResult Schedule(Message message)
        {
            var request = new ScheduleMessageRequest()
            {
                CommunityId = message.CommunityId,
                DateTimeToSend = (DateTime)message.DateTimeToSend,
                MessageContent = message.Content
            };
            var response = _scheduleMessage.Handle(request);
            return Ok();
        }

        [HttpPost("{id}/unschedule")]
        public IActionResult Unschedule(string id)
        {
            var request = new UnscheduleMessageRequest()
            {
                MessageId = id
            };
            var response = _unscheduleMessage.Handle(request);
            return Ok();
        }

        [HttpPut("{id}/content")]
        public IActionResult UpdateContent(string id, [FromBody] string content)
        {
            var request = new ModifyMessageRequest()
            {
                MessageId = id,
                NewMessageContent = content
            };
            var response = _modifyMessage.Handle(request);
            return Ok();
        }
    }
}