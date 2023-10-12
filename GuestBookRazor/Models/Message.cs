using System.ComponentModel.DataAnnotations;

namespace GuestBookRazor.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Текст сообщения: ")]
        public string Message_text { get; set; }

        public DateTime MessageDate { get; set; }

        public int Id_User { get; set; }

        public User? User { get; set; }
    }
}
