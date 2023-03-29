using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.Models
{
    public class Chapter
    {
        [Required]
        public int BookChapterId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        // Foreign key
        [Required]
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }
    }
}
