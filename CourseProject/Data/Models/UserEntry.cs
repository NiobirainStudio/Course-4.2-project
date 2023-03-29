using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.Models
{
    public class UserEntry
    {
        [Required]
        public string EntryIp { get; set; }

        [Required]
        public int Rating { get; set; } 

        // External key
        [Required]
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }
    }
}
