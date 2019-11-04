using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Dtos;
using Notifier.Core.Interfaces;

namespace Notifier.Controllers
{
    [Route("api/subscriber")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;
        public SubscriberController(ISubscriberService communityService)
        {
            _subscriberService = communityService;
        }

        [HttpPost]
        public IActionResult Subscribe([FromBody] SubscribeDto subscription)
        {
            _subscriberService.AddSubscriber(subscription);
            return Ok();
        }

        [HttpDelete]
        public IActionResult UnSubscribe([FromBody] SubscribeDto subscription)
        {
            _subscriberService.RemoveSubscriber(subscription);
            return Ok();
        }
    }
}