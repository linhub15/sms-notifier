using Notifier.Core.Dtos;
using Notifier.Core.Interfaces;

namespace Notifier.Infrastructure.Security
{
    public class Authenticator : IAuthenticator
    {
        public string RequestOneTimeCode(string phoneNumber)
        {
            return "";
        }

        public string Authenticate(AuthenticateDto dto)
        {
            return "";
        }

    }
}