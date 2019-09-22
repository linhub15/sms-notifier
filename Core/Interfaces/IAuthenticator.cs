using Notifier.Core.Dtos;

namespace Notifier.Core.Interfaces
{
    public interface IAuthenticator
    {
        string RequestOneTimeCode(string phoneNumber);
        string Authenticate(AuthenticateDto dto);
    }
}