using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Interfaces;
using MyPortfolio.DAL.Repositories;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.UnitofWork
{
    public class Uow : IUow
    {

        private readonly MainContext _db;

        public Uow(MainContext db)
        {
            _db = db;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_db);
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
