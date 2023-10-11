using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuestBookRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestBookRazor.Models
{  
    public class GuestBookContext : DbContext
    {
      public GuestBookContext(DbContextOptions<GuestBookContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
    
}


