using BookStoreAPI.Models;
using BookStoreAPI.Repository;

namespace BookStoreAPI.UnitOfWork
{
    public class UnitOFWork
    {
        BookStoreContext db;
        GenericRepository<Book> booksRepository;
        GenericRepository<Order> ordersRepository;
        GenericRepository<OrderDetails> orderDetailsRepository;
        GenericRepository<Author> authersRepository;
        public UnitOFWork(BookStoreContext db)
        {
            this.db = db;
            //booksRepository = new GenericRepository<Book>(db);
            //ordersRepository = new GenericRepository<Order>(db);
            //orderDetailsRepository = new GenericRepository<OrderDetails>(db);
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

        } public GenericRepository<Order> OrdersRepositry
        {
            get
            {
                if (ordersRepository == null)
                {
                    ordersRepository = new GenericRepository<Order>(db);
                }
                return ordersRepository;
            }

        } 
        public GenericRepository<OrderDetails> OrderDetailsRepositry
        {
            get
            {
                if (orderDetailsRepository == null)
                {
                    orderDetailsRepository = new GenericRepository<OrderDetails>(db);
                }
                return orderDetailsRepository;
            }

        }  public GenericRepository<Author> AuthorsRepositry
        {
            get
            {
                if (authersRepository == null)
                {
                    authersRepository = new GenericRepository<Author>(db);
                }
                return authersRepository;
            }

        }
        public void savechanges()
        {
            db.SaveChanges();
        }
    }
}
