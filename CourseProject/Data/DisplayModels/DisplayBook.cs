using CourseProject.Data.Models;

namespace CourseProject.Data.DisplayModels
{
    public class DisplayBook
    {
        public int BookId { get; set; }

        // All alternative names of this book
        public string Title { get; set; }

        public string Description { get; set; }

        // Author list
        public List<DisplayAuthor> Authors { get; set; }

        // Genre list
        public List<DisplayGenre> Genres { get; set; }

        // Display image
        public string Img { get; set; }

        // My book rating
        public int MyRating { get; set; }

        // Book rating
        public int[] Rating { get; set; }

        // Completion status
        public Status Status { get; set; }
    }
}
