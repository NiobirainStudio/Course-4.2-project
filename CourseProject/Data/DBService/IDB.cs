using CourseProject.Data.Models;
using CourseProject.Data.DisplayModels;

namespace CourseProject.Data.DBService
{
    public interface IDB
    {
        // Create new elements
        void CreateAuthor(string name);
        void CreateGenre(string name);
        public void CreateBook(string name, int[] genreIds, string description, string img, int authorId);

        int GetYourEntry(int bookId, string entryIp);
        void CreateEntry(int bookId, string entryIp, int rating);


        // Get elements for one book
        List<DisplayAuthor> GetDisplayBookAuthors(int bookId);
        List<DisplayGenre> GetDisplayBookGenres(int bookId);
        DisplayBook GetDisplayBookById(int bookId, string entryIp);
        List<DisplayChapter> GetDisplayChapters(int bookId);
        DisplayChapter GetDisplayChapter(int bookId, int chapterId);
        int[] GetBookRating(int bookId);


        // Get general data
        List<DisplayGenre> GetDisplayGenres();
        List<DisplayBook> GetDisplayBooks(string entryIp);

        List<DisplayBook> GetBooksByName(string searchTerm, string entryIp);
        List<DisplayBook> GetBooksByGenre(int genreId, string entryIp);
        List<DisplayBook> GetBooksByAuthor(int authorId, string entryIp);


        // Get basic data
        string GetAuthorById(int authorId);
        string GetGenreById(int genreId);
    }
}
