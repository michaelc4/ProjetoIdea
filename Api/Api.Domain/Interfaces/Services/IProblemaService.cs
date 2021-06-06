using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Presenters;
using System;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IProblemaService<T, TPresenter, TPostDto, TPutDto> : IBaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {
        Task<PagedResultPresenter<ProblemaPresenter>> GetPaged(int page, int pageSize, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch);

        Task<PagedResultPresenter<ProblemaPresenter>> GetPagedByUser(int page, int pageSize, Guid userId, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch);

        Task<PagedResultPresenter<ProblemaPresenter>> GetPagedInitialScreen(int page, int pageSize, Guid? userId, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string registrationDateIniSearch, string registrationDateEndSearch);

        Task<ProblemaPresenter> PutAvaliacao(ProblemaAvaliacaoPutDto dto);
    }
}
