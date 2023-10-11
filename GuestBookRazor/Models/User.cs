using System.ComponentModel.DataAnnotations;

namespace GuestBookRazor.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя пользователя: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Пароль: ")]
        public string Pwd { get; set; }

        public string? Salt { get; set; }
    }
}
