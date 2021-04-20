using Api.Domain.Entities;
using Api.Domain.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<PagedResultPresenter<T>> GetPaged(IQueryable<T> query, int page, int pageSize);
        IQueryable<T> GetQuery();
        Task<T> InsertAsync(T item);
        Task<T> SelectAsync(Guid id);
        Task<IEnumerable<T>> SelectAsync();
        Task<T> UpdateAsync(T item);
    }
}
