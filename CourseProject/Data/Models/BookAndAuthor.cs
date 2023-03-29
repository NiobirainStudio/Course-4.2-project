using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.Models
{
    public class BookAndAuthor
    {
        // Foreign keys, composite keys
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public Author Author { get; set; }

        [Required]
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }
    }
}
