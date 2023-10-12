using GuestBookRazor.Interfaces;
using GuestBookRazor.Models;

namespace GuestBookRazor.Repository
{
    public class ContextUnitOfWork : IUnitOfWork
    {
        private GuestBookContext db;
        private UserRepository userRepository;
        private MessageRepository messageRepository;

        public ContextUnitOfWork(GuestBookContext context)
        {
            db = context;
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (messageRepository == null)
                    messageRepository = new MessageRepository(db);
                return messageRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
