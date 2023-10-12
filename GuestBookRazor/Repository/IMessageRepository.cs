using GuestBookRazor.Models;

namespace GuestBookRazor.Repository
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAll();
        Task<Message> Get(string name);
        Task Create(Message item);
        Task<Message> Get(int id);
    }
}
