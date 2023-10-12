using GuestBookRazor.Models;

namespace GuestBookRazor.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(string name);
        Task Create(T item);
        Task<T> Get(int id);
    }
}
