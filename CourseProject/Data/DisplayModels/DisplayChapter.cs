namespace CourseProject.Data.DisplayModels
{
    public class DisplayChapter
    {
        public int BookId { get; set; }
        public int BookChapterId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int ChapterCount { get; set; }
        public string BookTitle { get; set; }
    }
}
