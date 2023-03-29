using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Data.Models
{
    public enum Status
    {
        Ongoing,
        Completed
    }

    [Index(nameof(Img), IsUnique = true)]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        // Completion status
        [Required]
        public Status Status { get; set; }
    }
}
