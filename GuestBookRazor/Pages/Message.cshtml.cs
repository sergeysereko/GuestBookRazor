using GuestBookRazor.Interfaces;
using GuestBookRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazor.Pages
{
    public class MessageModel : PageModel
    {

        private readonly GuestBookContext _context;

        public MessageModel(GuestBookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Message message { get; set; }

        public async Task<IActionResult> OnPost(Message message, [FromServices] IUnitOfWork repo)
        {
            string login = HttpContext.Session.GetString("login");
            if (!string.IsNullOrEmpty(login))
            {
                var user = await repo.Users.Get(login);
                if (user != null)
                {
                    int userId = user.Id;
                    message.MessageDate = DateTime.Now;
                    message.Id_User = userId;
                    message.User = user;
                }
            }

            if (ModelState.IsValid)
            {
                await repo.Messages.Create(message);
                await repo.Save();
                return RedirectToPage("Index");
            }

            return Page();
        }

    }
}
