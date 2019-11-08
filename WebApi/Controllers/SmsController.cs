using Microsoft.AspNetCore.Mvc;
using Notifier.Core.UseCases;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace Notifier.Controllers
{
    [Route("api/sms")]
    [ApiController]
    public class SmsController : TwilioController
    {
        private ISubscribe _subscribe;
        private IUnsubscribe _unsubscribe;
        public SmsController(
            ISubscribe subscribe,
            IUnsubscribe unsubscribe)
        {
            _subscribe = subscribe;
            _unsubscribe = unsubscribe;
        }
        [HttpPost]
        public TwiMLResult Index()
        {
            var smsSid = Request.Form["SmsSid"];
            var fromPhoneNumber = Request.Form["From"];
            var body = Request.Form["Body"];

            var communityId = "fmdc";

            var response = new MessagingResponse();
            if (body.ToString().ToLower() == "follow")
            {
                var request = new SubscribeRequest()
                {
                    CommunityId = communityId,
                    PhoneNumber = fromPhoneNumber
                };
                _subscribe.Handle(request);
                response.Message($"You're following 'fmdc'. \n\nText 'unfollow' to stop getting texts.");

            }
            else if (body.ToString().ToLower() == "unfollow")
            {
                var request = new UnsubscribeRequest()
                {
                    CommunityId = communityId,
                    PhoneNumber = fromPhoneNumber
                };
                _unsubscribe.Handle(request);
                response.Message("You've unfollowed 'fmdc'.");
            }
            else
            {
                response.Message("Text 'fmdc' to subscribe. Text 'unsubscribe' to unsubscribe");
            }
            return TwiML(response);
        }
    }
}