using Domain.Entity;

namespace Application.Interfaces
{
    public interface ILogger
    {
        public Task Log(Message message, string invalidEmail = "", string state = "");
    }
}
