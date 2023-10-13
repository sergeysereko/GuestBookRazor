using GuestBookRazor.Interfaces;
using GuestBookRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace GuestBookRazor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly GuestBookContext _context;
        
        public LoginModel(GuestBookContext context)
        {
            _context = context;
        }

       


        [BindProperty]
        public Login logon { get; set; }
        public void OnGet()
        {
        }

        //public async Task OnPost(Login logon, [FromServices] IUnitOfWork repo) 
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var users = await repo.Users.GetAll();
        //        if (users.Count() == 0)
        //        {
        //            ModelState.AddModelError("", "Wrong login or password!");
        //           return Page();
        //        }
        //        var usersf = users.Where(a => a.Name == logon.UserName);
        //        if (usersf.ToList().Count == 0)
        //        {
        //            ModelState.AddModelError("", "Wrong login or password!");
        //           return View(logon);
        //        }
        //        var user = usersf.First();
        //        string? salt = user.Salt;

        //        string passw = logon.Password;
        //        var hash = await GetHashCode(salt, passw);

        //        if (user.Pwd != hash.ToString())
        //        {
        //            ModelState.AddModelError("", "Wrong login or password!");
        //           return View(logon);
        //        }
        //        HttpContext.Session.SetString("login", user.Name);

        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(logon);
        //}


        public async Task<IActionResult> OnPost(Login logon, [FromServices] IUnitOfWork repo)
        {
            if (ModelState.IsValid)
            {
                var users = await repo.Users.GetAll();
                if (users.Count() == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return Page();
                }
                var usersf = users.Where(a => a.Name == logon.UserName);
                if (usersf.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return Page();
                }
                var user = usersf.First();
                string? salt = user.Salt;

                string passw = logon.Password;
                var hash = await GetHashCode(salt, passw);

                if (user.Pwd != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return Page();
                }
                HttpContext.Session.SetString("login", user.Name);

                return RedirectToPage("Index"); 
            }

            return Page();
        }



        private async Task<StringBuilder> GetHashCode(string salt, string pasw)
        {
            //переводим пароль в байт-массив  
            byte[] password = Encoding.Unicode.GetBytes(salt + pasw);

            //создаем объект для получения средств шифрования  
            var md5 = MD5.Create();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = md5.ComputeHash(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
            {
                hash.Append(string.Format("{0:X2}", byteHash[i]));
            }

            return hash;
        }

    }
}
