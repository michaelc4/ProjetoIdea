using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IBaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {
        Task<bool> Delete(Guid id);
        Task<TPresenter> Get(Guid id);
        Task<IEnumerable<TPresenter>> GetAll();
        Task<TPresenter> Post(TPostDto dto);
        Task<TPresenter> Put(TPutDto dto);
    }
}
