using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class ProblemaService : BaseService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto>, IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto>
    {
        private IRepository<ProblemaEntity> _repository;
        private IUsuarioRepository _repositoryUser;
        private IProblemaAnexoRepository _repositoryAttachment;
        private readonly IMapper _mapper;

        public ProblemaService(IRepository<ProblemaEntity> repository, IUsuarioRepository repositoryUser, IProblemaAnexoRepository repositoryAttachment, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
            _repositoryAttachment = repositoryAttachment;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<ProblemaPresenter>> GetPaged(int page, int pageSize)
        {
            IQueryable<ProblemaEntity> query = _repository.GetQuery();
            query = query.OrderBy(x => x.IndAprovado);

            var result = _mapper.Map<PagedResultPresenter<ProblemaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<ProblemaPresenter>> GetPagedByUser(int page, int pageSize, Guid userId)
        {
            IQueryable<ProblemaEntity> query = _repository.GetQuery();
            query = query.Where(x => x.UsuarioId == userId);
            query = query.OrderBy(x => x.IndAprovado);

            var result = _mapper.Map<PagedResultPresenter<ProblemaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public override async Task<ProblemaPresenter> Post(ProblemaPostDto dto)
        {
            ProblemaEntity dtoResult = await _repository.InsertAsync(_mapper.Map<ProblemaEntity>(dto));

            if (dto.Anexos != null && dto.Anexos.Count > 0)
            {
                foreach (var attach in dto.Anexos)
                {
                    attach.ProblemaId = dtoResult.Id;
                    await _repositoryAttachment.InsertAsync(_mapper.Map<ProblemaAnexoEntity>(dto));
                }
            }

            return _mapper.Map<ProblemaPresenter>(dtoResult);
        }

        public override async Task<ProblemaPresenter> Put(ProblemaPutDto dto)
        {
            ProblemaEntity dtoResult = await _repository.UpdateAsync(_mapper.Map<ProblemaEntity>(dto));

            var attachments = await _repositoryAttachment.GetByProjectAsync(dtoResult.Id.ToString());
            if (attachments != null && attachments.Count() > 0)
            {
                foreach (var attach in attachments)
                {
                    await _repositoryAttachment.DeleteAsync(attach.Id);
                }
            }

            if (dto.Anexos != null && dto.Anexos.Count > 0)
            {
                foreach (var attach in dto.Anexos)
                {
                    attach.ProblemaId = dtoResult.Id;
                    await _repositoryAttachment.InsertAsync(_mapper.Map<ProblemaAnexoEntity>(dto));
                }
            }

            return _mapper.Map<ProblemaPresenter>(dtoResult);
        }

        private async Task<PagedResultPresenter<ProblemaPresenter>> GetPresenterDetail(PagedResultPresenter<ProblemaPresenter> result)
        {
            if (result != null && result.Results != null)
            {
                foreach (var item in result.Results)
                {
                    var userEntity = await _repositoryUser.SelectAsync(item.UsuarioId);
                    item.Usuario = _mapper.Map<UsuarioPresenter>(userEntity);

                    var listAttachments = new List<ProblemaAnexoPresenter>();
                    var attachments = await _repositoryAttachment.GetByProjectAsync(item.Id);
                    foreach (var attach in attachments)
                    {
                        listAttachments.Add(_mapper.Map<ProblemaAnexoPresenter>(attach));
                    }
                    item.Anexos = listAttachments;
                }
            }

            return result;
        }
    }
}
