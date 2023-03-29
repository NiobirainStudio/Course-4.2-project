using CourseProject.Data.DisplayModels;
using CourseProject.Data.Models;

namespace CourseProject.Data.DBService
{
    public class DB_Manager : IDB
    {
        private readonly DataDBContext _db;
        
        public DB_Manager(DataDBContext db)
        {
            _db = db;
        }


        // Create new elements
        public void CreateAuthor(string name)
        {
            _db.Authors.Add(new Author { Name = name });
            _db.SaveChanges();
        }

        public void CreateGenre(string name)
        {
            _db.Genres.Add(new Genre { Name = name });
            _db.SaveChanges();
        }

        public void CreateBook(string name, int[] genreIds, string description, string img, int authorId)
        {
            try
            {
                // Add the book itself
                _db.Books.Add(new Book
                {
                    Title = name,
                    Description = description,
                    Img = img,
                    Status = Status.Ongoing
                });
                _db.SaveChanges();

                // Find the book's id
                int bookId =
                    (from book in _db.Books
                     where book.Img.Equals(img)
                     select book.BookId).FirstOrDefault();

                // Set the user as author
                _db.BookAndAuthors.Add(new BookAndAuthor
                {
                    AuthorId = authorId,
                    BookId = bookId
                });

                // Add book genres
                foreach (int genreId in genreIds)
                {
                    _db.BookAndGenres.Add(new BookAndGenre
                    {
                        GenreId = genreId,
                        BookId = bookId
                    });
                }

                _db.SaveChanges();

                return;
            } catch (Exception ex)
            {
                // Something went wrong
                return;
            }
        }

        public int GetYourEntry(int bookId, string entryIp)
        {
            var entry = (from userEntry in _db.UserEntries
                         where userEntry.EntryIp == entryIp
                         where userEntry.BookId == bookId
                         select userEntry).FirstOrDefault();
            
            if(entry != null)
            {
                return entry.Rating;
            }
            return -1;
        }

        public void CreateEntry(int bookId, string entryIp, int rating)
        {
            if (rating != 0 && rating != 1) return;

            var obj = (from userEntry in _db.UserEntries
                       where userEntry.BookId == bookId
                       where userEntry.EntryIp == entryIp
                       select userEntry).FirstOrDefault();

            if (obj != null)
            {
                _db.UserEntries.Remove(obj);
                _db.SaveChanges();
            }


            try
            {
                _db.UserEntries.Add(new UserEntry
                {
                    BookId = bookId,
                    EntryIp = entryIp,
                    Rating = rating
                });

                _db.SaveChanges();
            } catch (Exception e)
            {
                // Unknown book id
            }
        }



        public List<DisplayAuthor> GetDisplayBookAuthors(int bookId)
        {
            return (from book in _db.Books

                    join book_and_authors in _db.BookAndAuthors
                    on book.BookId equals book_and_authors.BookId

                    join author in _db.Authors
                    on book_and_authors.AuthorId equals author.AuthorId

                    where book.BookId == bookId
                    select new DisplayAuthor { 
                        AuthorId = author.AuthorId,
                        Name = author.Name
                    }).ToList();
        }

        public List<DisplayGenre> GetDisplayBookGenres(int bookId)
        {
            return (from book in _db.Books

                    join book_and_genre in _db.BookAndGenres
                    on book.BookId equals book_and_genre.BookId

                    join genre in _db.Genres
                    on book_and_genre.GenreId equals genre.GenreId

                    where book.BookId == bookId
                    select new DisplayGenre {
                        GenreId = genre.GenreId,
                        Name = genre.Name
                    }).ToList();
        }


        public DisplayBook GetDisplayBookById(int bookId, string entryIp)
        {
            var book = _db.Books.First(b => b.BookId == bookId);

            if (book == null) return null;

            return new DisplayBook
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                Authors = GetDisplayBookAuthors(book.BookId),
                Genres = GetDisplayBookGenres(book.BookId),
                Img = book.Img,
                MyRating = GetYourEntry(book.BookId, entryIp),
                Rating = GetBookRating(book.BookId),
                Status = book.Status
            };
        }

        public List<DisplayChapter> GetDisplayChapters(int bookId)
        {
            return (from chapter in _db.Chapters
                    where bookId == chapter.BookId
                    select new DisplayChapter
                    {
                        BookId = bookId,
                        BookChapterId = chapter.BookChapterId,
                        Title = chapter.Title,
                    }).ToList();
        }



        public List<DisplayGenre> GetDisplayGenres()
        {
            return (from genre in _db.Genres select new DisplayGenre { 
                GenreId = genre.GenreId, 
                Name = genre.Name
            }).ToList();
        }

        public DisplayChapter GetDisplayChapter(int bookId, int chapterId)
        {
            var chapterCount = (from chapter in _db.Chapters
                                where bookId == chapter.BookId
                                select chapter).Count();

            string title = (from book in _db.Books
                            where bookId == book.BookId
                            select book.Title).First();

            return (from chapter in _db.Chapters where chapter.BookId == bookId && chapter.BookChapterId == chapterId select new DisplayChapter {
                BookId = chapter.BookId,
                BookChapterId = chapter.BookChapterId,
                Title = chapter.Title,
                Text = chapter.Text,
                BookTitle = title,
                ChapterCount = chapterCount
            }).First();
        }

        public int[] GetBookRating(int bookId)
        {
            return new int[] {
            (from rating in _db.UserEntries
             where rating.BookId == bookId
             where rating.Rating == 0
             select rating).Count(),
            (from rating in _db.UserEntries
             where rating.BookId == bookId
             where rating.Rating == 1
             select rating).Count()};
        }





        public List<DisplayBook> GetDisplayBooks(string entryIp)
        {
            var books = (from book in _db.Books select book);
            List<DisplayBook> displayBooks = new List<DisplayBook>();

            foreach (var book in books)
            {
                displayBooks.Add(new DisplayBook
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    Authors = GetDisplayBookAuthors(book.BookId),
                    Genres = GetDisplayBookGenres(book.BookId),
                    Img = book.Img,
                    MyRating = GetYourEntry(book.BookId, entryIp),
                    Rating = GetBookRating(book.BookId),
                    Status = book.Status
                });
            }

            return displayBooks;
        }


        public List<DisplayBook> GetBooksByName(string searchTerm, string entryIp)
        {
            var books = (from book in _db.Books
                         where book.Title.ToLower().Contains(searchTerm.Trim().ToLower())
                         select book);

            List<DisplayBook> displayBooks = new List<DisplayBook>();

            foreach (var book in books)
            {
                displayBooks.Add(new DisplayBook
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    Authors = GetDisplayBookAuthors(book.BookId),
                    Genres = GetDisplayBookGenres(book.BookId),
                    Img = book.Img,
                    MyRating = GetYourEntry(book.BookId, entryIp),
                    Rating = GetBookRating(book.BookId),
                    Status = book.Status
                });
            }

            return displayBooks;
        }

        public List<DisplayBook> GetBooksByGenre(int genreId, string entryIp)
		{
            var books = (from book in _db.Books
                         join book_and_genre in _db.BookAndGenres
                         on book.BookId equals book_and_genre.BookId
                         where book_and_genre.GenreId == genreId
                         select book);

            List<DisplayBook> displayBooks = new List<DisplayBook>();

            foreach (var book in books)
            {
                displayBooks.Add(new DisplayBook
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    Authors = GetDisplayBookAuthors(book.BookId),
                    Genres = GetDisplayBookGenres(book.BookId),
                    Img = book.Img,
                    MyRating = GetYourEntry(book.BookId, entryIp),
                    Rating = GetBookRating(book.BookId),
                    Status = book.Status
                });
            }

            return displayBooks;
        }

        public List<DisplayBook> GetBooksByAuthor(int authorId, string entryIp)
		{
            var books = (from book in _db.Books
                         join book_and_author in _db.BookAndAuthors
                         on book.BookId equals book_and_author.BookId
                         where book_and_author.AuthorId == authorId
                         select book);

            List<DisplayBook> displayBooks = new List<DisplayBook>();

            foreach (var book in books)
            {
                displayBooks.Add(new DisplayBook
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    Authors = GetDisplayBookAuthors(book.BookId),
                    Genres = GetDisplayBookGenres(book.BookId),
                    Img = book.Img,
                    MyRating = GetYourEntry(book.BookId, entryIp),
                    Rating = GetBookRating(book.BookId),
                    Status = book.Status
                });
            }

            return displayBooks;
        }




        public string GetAuthorById(int authorId)
		{
            return (from author in _db.Authors
                    where author.AuthorId == authorId
                    select author.Name).First();
		}

        public string GetGenreById(int genreId)
		{
            return (from genre in _db.Genres
                    where genre.GenreId == genreId
                    select genre.Name).First();
        }
    }
}
