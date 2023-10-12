using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GuestBookRazor.Models;
using GuestBookRazor.Interfaces;

namespace GuestBookRazor.Pages
{
    public class IndexModel : PageModel
    {
        public IList<Message> Message { get; set; } = default!;

        public async Task OnGetAsync([FromServices] IUnitOfWork repo)
        {
            Message = await repo.Messages.GetAll();
        }
      
    }
}