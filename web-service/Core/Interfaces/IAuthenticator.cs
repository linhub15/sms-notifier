using Notifier.Core.Dtos;

namespace Notifier.Core.Interfaces
{
    public interface IAuthenticator
    {
        void RequestOneTimeCode(string phoneNumber);
        string Authenticate(AuthenticateDto dto);
    }
}