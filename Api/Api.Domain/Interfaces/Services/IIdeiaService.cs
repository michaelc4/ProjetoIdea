using Api.Domain.Entities;
using Api.Domain.Presenters;
using System;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IIdeiaService<T, TPresenter, TPostDto, TPutDto> : IBaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {
        Task<PagedResultPresenter<IdeiaPresenter>> GetPaged(int page, int pageSize);

        Task<PagedResultPresenter<IdeiaPresenter>> GetPagedByUser(int page, int pageSize, Guid userId);
    }
}
