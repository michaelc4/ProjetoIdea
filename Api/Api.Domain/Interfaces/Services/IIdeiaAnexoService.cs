using Api.Domain.Entities;
using Api.Domain.Presenters;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IIdeiaAnexoService<T, TPresenter, TPostDto, TPutDto> : IBaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {
        Task<PagedResultPresenter<IdeiaAnexoPresenter>> GetPaged(int page, int pageSize);
    }
}
