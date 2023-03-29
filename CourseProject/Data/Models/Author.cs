using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
