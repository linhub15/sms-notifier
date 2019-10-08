using System;
using Notifier.Core.Dtos;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure.Security
{
    public class Authenticator : IAuthenticator
    {
        public void RequestOneTimeCode(string phoneNumber)
        {
            // 1. Check phone number white list
            if (phoneNumber != "7809651451")
            {
                throw new Exception($"phoneNumber:${phoneNumber} is not on the white list");
            }
            // 2. call twilio api
        }

        public string Authenticate(CredentialsDto dto)
        {
            return "";
        }

    }
}