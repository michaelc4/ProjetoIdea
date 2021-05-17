using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Presenters;
using System;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IIdeiaService<T, TPresenter, TPostDto, TPutDto> : IBaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {
        Task<PagedResultPresenter<IdeiaPresenter>> GetPaged(int page, int pageSize, string ideaSearch, string reasonSearch, string shareSearch, string developmentSearch, string secretSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch);

        Task<PagedResultPresenter<IdeiaPresenter>> GetPagedByUser(int page, int pageSize, Guid userId, string ideaSearch, string reasonSearch, string shareSearch, string developmentSearch, string secretSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch);

        Task<PagedResultPresenter<IdeiaPresenter>> GetPagedInitialScreen(int page, int pageSize);

        Task<IdeiaPresenter> PutAvaliacao(IdeiaAvaliacaoPutDto dto);
    }
}
