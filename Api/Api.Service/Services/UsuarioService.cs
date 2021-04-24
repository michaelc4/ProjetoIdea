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

        public async Task<PagedResultPresenter<UsuarioPresenter>> GetPaged(int page, int pageSize)
        {
            IQueryable<UsuarioEntity> query = _repository.GetQuery();

            return _mapper.Map<PagedResultPresenter<UsuarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
        }
    }
}
