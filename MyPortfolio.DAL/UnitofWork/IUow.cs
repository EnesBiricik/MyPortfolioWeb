using MyPortfolio.DAL.Interfaces;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.UnitOfWork
{
    public interface IUow
    {

        IRepository<T> GetRepository<T>() where T : BaseEntity;

        Task SaveChanges();

    }
}
