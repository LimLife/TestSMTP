using Domain.Entity;

namespace Application.Interfaces
{
    public interface IRepository
    {
        public Task AddMessage(Message message);
        public Task<List<Message>> GetAllMessage();
    }
}
