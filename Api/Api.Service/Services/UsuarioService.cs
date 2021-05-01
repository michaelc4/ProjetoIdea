using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class UsuarioService : BaseService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto>, IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto>
    {
        private IRepository<UsuarioEntity> _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IRepository<UsuarioEntity> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<UsuarioPresenter>> GetPaged(int page, int pageSize, string nameSearch, string emailSearch, string foneSearch, bool? isAdminSearch)
        {
            IQueryable<UsuarioEntity> query = _repository.GetQuery();

            if (!string.IsNullOrEmpty(nameSearch))
            {
                query = query.Where(x => x.DesNome.Contains(nameSearch));
            }

            if (!string.IsNullOrEmpty(emailSearch))
            {
                query = query.Where(x => x.DesEmail.Contains(emailSearch));
            }

            if (!string.IsNullOrEmpty(foneSearch))
            {
                query = query.Where(x => x.DesTelefone.Contains(foneSearch));
            }

            if (isAdminSearch.HasValue)
            {
                query = query.Where(x => x.Admin == (isAdminSearch.Value ? 1 : 0));
            }

            return _mapper.Map<PagedResultPresenter<UsuarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
        }
    }
}
