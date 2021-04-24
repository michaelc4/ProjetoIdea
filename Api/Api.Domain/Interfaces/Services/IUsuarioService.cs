using Api.Domain.Entities;
using Api.Domain.Presenters;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IUsuarioService<T, TPresenter, TPostDto, TPutDto> : IBaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {
        Task<PagedResultPresenter<UsuarioPresenter>> GetPaged(int page, int pageSize);
    }
}
