using Core.Models;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {


        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> GetAll(ISpecification<T> spec);
        Task<T> GetById(int id);
        Task<T> GetById(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task Remove(int id);
        Task<int> CountAsync(ISpecification<T> spec);


    }
}