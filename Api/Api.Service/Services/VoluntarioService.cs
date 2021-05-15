using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class VoluntarioService : BaseService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto>, IVoluntarioService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto>
    {
        private IRepository<VoluntarioEntity> _repository;
        private IIdeiaAnexoRepository _repositoryIdeiaAttachment;
        private IProblemaAnexoRepository _repositoryProblemAttachment;
        private readonly IMapper _mapper;

        public VoluntarioService(IRepository<VoluntarioEntity> repository,
            IIdeiaAnexoRepository repositoryIdeiaAttachment,
            IProblemaAnexoRepository repositoryProblemAttachment,
            IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _repositoryIdeiaAttachment = repositoryIdeiaAttachment;
            _repositoryProblemAttachment = repositoryProblemAttachment;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<VoluntarioPresenter>> GetPaged(int page, int pageSize)
        {
            IQueryable<VoluntarioEntity> query = _repository.GetQuery();
            query = query.OrderByDescending(x => x.DataCriacao);
            query = query
                .Include(x => x.Usuario)
                .Include(x => x.Ideia)
                .Include(x => x.Problema);

            var result = _mapper.Map<PagedResultPresenter<VoluntarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<VoluntarioPresenter>> GetPagedByUser(int page, int pageSize, Guid userId)
        {
            IQueryable<VoluntarioEntity> query = _repository.GetQuery();
            query = query.Where(x => x.UsuarioId == userId);
            query = query.OrderByDescending(x => x.DataCriacao);
            query = query
                .Include(x => x.Usuario)
                .Include(x => x.Ideia)
                .Include(x => x.Problema);

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
                query = query.Where(x => x.IdeiaId.Equals(ideaId.Value));
            }

            query = query.OrderByDescending(x => x.DataCriacao);

            query = query
                .Include(x => x.Usuario)
                .Include(x => x.Ideia)
                .Include(x => x.Problema);

            var result = _mapper.Map<PagedResultPresenter<VoluntarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return result;
        }

        private async Task<PagedResultPresenter<VoluntarioPresenter>> GetPresenterDetail(PagedResultPresenter<VoluntarioPresenter> result)
        {
            if (result != null && result.Results != null)
            {
                foreach (var item in result.Results)
                {
                    if (item.IdeiaId.HasValue)
                    {
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
    }
}
