using System.ComponentModel.DataAnnotations;

namespace AspNetCoreCursovaya.helpingClasses
{
    public class CommentModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        public string CommentatorName { get; set; }

        [Required(ErrorMessage = "Введите текст комментария")]
        public string CommentatorText { get; set; }

        public int NewsId { get; set; }
        public int PageIndex { get; set; }
    }
}
