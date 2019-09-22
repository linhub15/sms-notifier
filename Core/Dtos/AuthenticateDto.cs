namespace Notifier.Core.Dtos
{
    public class AuthenticateDto
    {
        public string phoneNumber { get; set; }
        public string oneTimeCode { get; set; }
    }
}