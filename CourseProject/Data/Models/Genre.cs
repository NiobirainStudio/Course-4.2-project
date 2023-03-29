using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
