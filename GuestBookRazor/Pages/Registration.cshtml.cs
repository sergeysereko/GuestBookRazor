using GuestBookRazor.Interfaces;
using GuestBookRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;

namespace GuestBookRazor.Pages
{
    public class RegistrationModel : PageModel
    {

        private readonly GuestBookContext _context;

        public RegistrationModel(GuestBookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Registration registration { get; set; }


       
        public async Task<IActionResult> OnPost(Registration registration, [FromServices] IUnitOfWork repo)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = registration.Login;
                user.Pwd = registration.Password;

                var passPlusSult = await GetSaltAndPass(registration.Password);

                user.Pwd = passPlusSult.Password;
                user.Salt = passPlusSult.Salt;
          
                await repo.Users.Create(user);
                await repo.Save();
                return RedirectToPage("Login");
                
            }

            return Page();
        }

        public async Task<(string Salt, string Password)> GetSaltAndPass(string pass)
        {

            byte[] saltbuf = new byte[16];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();

            //переводим пароль в байт-массив  
            byte[] password = Encoding.Unicode.GetBytes(salt + pass);

            //создаем объект для получения средств шифрования  
            var md5 = MD5.Create();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = md5.ComputeHash(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
            {
                hash.Append(string.Format("{0:X2}", byteHash[i]));
            }
            string passsalt = hash.ToString();

            return (salt, passsalt);
        }


    }
}
