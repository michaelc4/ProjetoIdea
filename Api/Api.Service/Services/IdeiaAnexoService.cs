using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class IdeiaAnexoService : BaseService<IdeiaAnexoEntity, IdeiaAnexoPresenter, IdeiaAnexoPostDto, IdeiaAnexoPutDto>, IIdeiaAnexoService<IdeiaAnexoEntity, IdeiaAnexoPresenter, IdeiaAnexoPostDto, IdeiaAnexoPutDto>
    {
        private IRepository<IdeiaAnexoEntity> _repository;
        private readonly IMapper _mapper;

        public IdeiaAnexoService(IRepository<IdeiaAnexoEntity> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<IdeiaAnexoPresenter>> GetPaged(int page, int pageSize)
        {
            IQueryable<IdeiaAnexoEntity> query = _repository.GetQuery();

            return _mapper.Map<PagedResultPresenter<IdeiaAnexoPresenter>>(await _repository.GetPaged(query, page, pageSize));
        }
    }
}
