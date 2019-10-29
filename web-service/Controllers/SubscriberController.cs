using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Dtos;
using Notifier.Core.Interfaces;

namespace Notifier.Controllers
{
    [Route("api/subscriber")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _communityService;
        public SubscriberController(ISubscriberService communityService)
        {
            _communityService = communityService;
        }

        [HttpPost]
        public IActionResult Subscribe([FromBody] SubscribeDto subscription)
        {
            _communityService.AddSubscriber(subscription.PhoneNumber, subscription.CommunityTag);
            return Ok();
        }

        [HttpDelete]
        public IActionResult UnSubscribe([FromBody] SubscribeDto subscription)
        {
            _communityService.RemoveSubscriber(subscription);
            return Ok();
        }
    }
}