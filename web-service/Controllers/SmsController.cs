using Microsoft.AspNetCore.Mvc;
using Notifier.Core.Dtos;
using Notifier.Core.Interfaces;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace Notifier.Controllers
{
    [Route("api/sms")]
    [ApiController]
    public class SmsController : TwilioController
    {
        private ISubscriberService _subscriberService;
        public SmsController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }
        [HttpPost]
        public TwiMLResult Index()
        {
            var smsSid = Request.Form["SmsSid"];
            var fromPhoneNumber = Request.Form["From"];
            var body = Request.Form["Body"];
            
            var subscriber = new SubscribeDto() 
            {
                CommunityTag = "fmdc",
                PhoneNumber = fromPhoneNumber
            };

            var response = new MessagingResponse();
            if (body.ToString().ToLower() == "follow")
            {
                _subscriberService.AddSubscriber(subscriber);
                response.Message($"You're following 'fmdc'. \n\nText 'unfollow' to stop getting texts.");

            }
            else if (body.ToString().ToLower() == "unfollow")
            {
                _subscriberService.RemoveSubscriber(subscriber);
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