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
    public class VoluntarioService : BaseService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto>, IVoluntarioService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto>
    {
        private IRepository<VoluntarioEntity> _repository;
        private IUsuarioRepository _repositoryUser;
        private IIdeiaRepository _repositoryIdeia;
        private IProblemaRepository _repositoryProblem;
        private IIdeiaAnexoRepository _repositoryIdeiaAttachment;
        private IProblemaAnexoRepository _repositoryProblemAttachment;
        private readonly IMapper _mapper;

        public VoluntarioService(IRepository<VoluntarioEntity> repository,
            IUsuarioRepository repositoryUser,
            IIdeiaRepository repositoryIdeia,
            IProblemaRepository repositoryProblem,
            IIdeiaAnexoRepository repositoryIdeiaAttachment,
            IProblemaAnexoRepository repositoryProblemAttachment,
            IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
            _repositoryIdeia = repositoryIdeia;
            _repositoryProblem = repositoryProblem;
            _repositoryIdeiaAttachment = repositoryIdeiaAttachment;
            _repositoryProblemAttachment = repositoryProblemAttachment;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<VoluntarioPresenter>> GetPaged(int page, int pageSize)
        {
            IQueryable<VoluntarioEntity> query = _repository.GetQuery();
            query = query.OrderByDescending(x => x.DataCriacao);

            var result = _mapper.Map<PagedResultPresenter<VoluntarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<VoluntarioPresenter>> GetPagedByUser(int page, int pageSize, Guid userId)
        {
            IQueryable<VoluntarioEntity> query = _repository.GetQuery();
            query = query.Where(x => x.UsuarioId == userId);
            query = query.OrderByDescending(x => x.DataCriacao);

            var result = _mapper.Map<PagedResultPresenter<VoluntarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<VoluntarioPresenter>> GetPagedByProblemOrIdeia(int page, int pageSize, Guid? problemId, Guid? ideaId)
        {
            IQueryable<VoluntarioEntity> query = _repository.GetQuery();

            if (problemId.HasValue)
            {
                query = query.Where(x => x.ProblemaId == problemId.Value);
            }

            if (ideaId.HasValue)
            {
                query = query.Where(x => x.IdeiaId == ideaId.Value);
            }

            query = query.OrderByDescending(x => x.DataCriacao);

            var result = _mapper.Map<PagedResultPresenter<VoluntarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetailProblemOrIdeia(result);
        }

        private async Task<PagedResultPresenter<VoluntarioPresenter>> GetPresenterDetail(PagedResultPresenter<VoluntarioPresenter> result)
        {
            if (result != null && result.Results != null)
            {
                foreach (var item in result.Results)
                {
                    if (item.IdeiaId.HasValue)
                    {
                        var ideiaEntity = await _repositoryIdeia.SelectAsync(item.IdeiaId.Value);
                        item.Ideia = _mapper.Map<IdeiaPresenter>(ideiaEntity);

                        var listAttachments = new List<IdeiaAnexoPresenter>();
                        var attachments = await _repositoryIdeiaAttachment.GetByProjectAsync(item.Id);
                        foreach (var attach in attachments)
                        {
                            listAttachments.Add(_mapper.Map<IdeiaAnexoPresenter>(attach));
                        }
                        item.Ideia.Anexos = listAttachments;
                    }

                    if (item.ProblemaId.HasValue)
                    {
                        var problemEntity = await _repositoryProblem.SelectAsync(item.ProblemaId.Value);
                        item.Problema = _mapper.Map<ProblemaPresenter>(problemEntity);

                        var listAttachments = new List<ProblemaAnexoPresenter>();
                        var attachments = await _repositoryProblemAttachment.GetByProjectAsync(item.Id);
                        foreach (var attach in attachments)
                        {
                            listAttachments.Add(_mapper.Map<ProblemaAnexoPresenter>(attach));
                        }
                        item.Problema.Anexos = listAttachments;
                    }
                }
            }

            return result;
        }

        private async Task<PagedResultPresenter<VoluntarioPresenter>> GetPresenterDetailProblemOrIdeia(PagedResultPresenter<VoluntarioPresenter> result)
        {
            if (result != null && result.Results != null)
            {
                foreach (var item in result.Results)
                {
                    var userEntity = await _repositoryUser.SelectAsync(item.UsuarioId);
                    item.Usuario = _mapper.Map<UsuarioPresenter>(userEntity);
                }
            }

            return result;
        }
    }
}
