using Api.Domain.Entities;
using Api.Domain.Presenters;
using System;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IVoluntarioService<T, TPresenter, TPostDto, TPutDto> : IBaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {
        Task<PagedResultPresenter<VoluntarioPresenter>> GetPaged(int page, int pageSize);

        Task<PagedResultPresenter<VoluntarioPresenter>> GetPagedByUser(int page, int pageSize, Guid userId);

        Task<PagedResultPresenter<VoluntarioPresenter>> GetPagedByProblemOrIdeia(int page, int pageSize, Guid? problemId, Guid? ideaId);
    }
}
