using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace Notifier.Controllers
{
    [Route("api/sms")]
    [ApiController]
    public class SmsController : TwilioController
    {
        [HttpPost]
        public TwiMLResult Index()
        {
            var smsSid = Request.Form["SmsSid"];
            var from = Request.Form["From"];
            var body = Request.Form["Body"];

            var response = new MessagingResponse();
            response.Message($"from: {from}; body: {body}");
            return TwiML(response);
        }
    }
}