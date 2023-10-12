using System.ComponentModel.DataAnnotations;

namespace GuestBookRazor.Models
{
    public class User
    {
        public User()
        {
            this.Message = new HashSet<Message>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя пользователя: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Пароль: ")]
        public string Pwd { get; set; }

        public string? Salt { get; set; }

        public ICollection<Message> Message { get; set; }
    }
}
