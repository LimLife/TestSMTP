using Application.Common;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        public Task Send(EmailModel model);
    }
}
