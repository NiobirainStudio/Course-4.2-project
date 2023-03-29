using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.Models
{
    public class BookAndGenre
    {
        // Foreign keys, composite keys
        [Required]
        public int GenreId { get; set; }
        [Required]
        public Genre Genre { get; set; }

        [Required]
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }
    }
}
