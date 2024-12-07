using BookStoreAPI.Models;
using BookStoreAPI.Repository;

namespace BookStoreAPI.UnitOfWork
{
    public class UnitOfWork
    {
        BookStoreContext db;
        // GenericRepository<>;
        public UnitOfWork( BookStoreContext db)
        {
            this.db = db;   
        }

        public void savechanges()
        {
            db.SaveChanges();
        }
    }
}
