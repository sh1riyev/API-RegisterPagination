
using System;
using System.Linq.Expressions;
using Domain.Common;

namespace Service.Services.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        Task<T> GetBy(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task Create(T entity);
        Task Delete(T entiy);
        Task Update(T entity);
        Task<bool> IsExist(Expression<Func<T, bool>> predicate = null);
    }
}
