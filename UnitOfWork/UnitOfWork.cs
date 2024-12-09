using BookStoreAPI.Models;
using BookStoreAPI.Repository;

namespace BookStoreAPI.UnitOfWork
{
    public class UnitOFWork
    {
        BookStoreContext db;
        GenericRepository<Book> booksRepository;
        public UnitOFWork(BookStoreContext db)
        {
            this.db = db;
            booksRepository = new GenericRepository<Book>(db);
        }

        public GenericRepository<Book> BooksRepositry
        {
            get
            {
                if (booksRepository == null)
                {
                    booksRepository = new GenericRepository<Book>(db);
                }
                return booksRepository;
            }

        }
        public void savechanges()
        {
            db.SaveChanges();
        }
    }
}
