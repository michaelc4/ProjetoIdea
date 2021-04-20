using Api.Domain.Presenters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<bool> Delete(Guid id);
        Task<UsuarioPresenter> Get(Guid id);
        Task<IEnumerable<UsuarioPresenter>> GetAll();
        Task<PagedResultPresenter<UsuarioPresenter>> GetPaged(int page, int pageSize);
        Task<UsuarioPresenter> Put(UsuarioPutDto usuario);
    }
}
