// Install the C# / .NET helper library from twilio.com/docs/csharp/install

using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Notifier.Infrastructure
{
    class SmsApi
    {
        public void SendMessage()
        {
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "AC582853f0b9b85f4ea894f3ebe57c4386";
            const string authToken = "daad737995ea244fb6f9fe0e9b508f00";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Join Earth's mightiest heroes. Like Kevin Bacon.",
                from: new Twilio.Types.PhoneNumber("+12055767675"),
                to: new Twilio.Types.PhoneNumber("+17809651451"),
                
            );

            Console.WriteLine(message.Sid);
        }
    }
}