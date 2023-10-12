using GuestBookRazor.Models;

namespace GuestBookRazor.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> Get(string name);
        Task Create(User item);
        Task<User> Get(int id);
    }
}
