using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class ProblemaAnexoService : BaseService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto>, IProblemaAnexoService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto>
    {
        private IRepository<ProblemaAnexoEntity> _repository;
        private readonly IMapper _mapper;

        public ProblemaAnexoService(IRepository<ProblemaAnexoEntity> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<ProblemaAnexoPresenter>> GetPaged(int page, int pageSize)
        {
            IQueryable<ProblemaAnexoEntity> query = _repository.GetQuery();

            return _mapper.Map<PagedResultPresenter<ProblemaAnexoPresenter>>(await _repository.GetPaged(query, page, pageSize));
        }
    }
}
