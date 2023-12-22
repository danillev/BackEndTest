using System.Threading.Tasks;

namespace BackEndTest.Interfaces
{
    public interface IGenericRepository<T> : IDisposable
                        where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Save();


    }
}
