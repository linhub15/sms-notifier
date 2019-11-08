using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Dtos;
using Notifier.Core.UseCases;

namespace Notifier.Controllers
{
    [Route("api/subscriber")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private ISubscribe _subscribe;
        private IUnsubscribe _unsubscribe;
        public SubscriberController(
            ISubscribe subscribe,
            IUnsubscribe unsubscribe)
        {
            _subscribe = subscribe;
            _unsubscribe = unsubscribe;
        }

        [HttpPost]
        public IActionResult Subscribe([FromBody] SubscribeDto subscription)
        {
            var request = new SubscribeRequest()
            {
                CommunityId = subscription.CommunityTag,
                PhoneNumber = subscription.PhoneNumber
            };
            _subscribe.Handle(request);
            return Ok();
        }

        [HttpDelete]
        public IActionResult UnSubscribe([FromBody] SubscribeDto subscription)
        {
            var request = new UnsubscribeRequest()
            {
                CommunityId = subscription.CommunityTag,
                PhoneNumber = subscription.PhoneNumber
            };
            _unsubscribe.Handle(request);
            return Ok();
        }
    }
}