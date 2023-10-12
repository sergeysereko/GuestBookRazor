using GuestBookRazor.Models;

namespace GuestBookRazor.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Message> Messages { get; }
        Task Save();
    }
}
