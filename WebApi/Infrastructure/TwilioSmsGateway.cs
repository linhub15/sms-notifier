using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;
using Twilio.Rest.Api.V2010.Account;

namespace Notifier.Infrastructure
{
    public class TwilioSmsGateway : ISmsGateway
    {
        public TwilioSmsGateway(IConfiguration configuration)
        {
            string accountId = configuration.GetValue<string>("Twilio:AccountId");
            string authToken = configuration.GetValue<string>("Twilio:AuthToken");
            Twilio.TwilioClient.Init(accountId, authToken);
        }
        public Task SendMessageAsync(Message message, string toPhoneNumber)
        {
            var resource = MessageResource.Create(
                body: message.Content,
                from: new Twilio.Types.PhoneNumber("+12055767675"),
                to: new Twilio.Types.PhoneNumber(toPhoneNumber)
            );
            return Task.CompletedTask;
        }
    }
}