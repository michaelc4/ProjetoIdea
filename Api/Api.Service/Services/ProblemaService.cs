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
    public class ProblemaService : BaseService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto>, IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto>
    {
        private IRepository<ProblemaEntity> _repository;
        private IProblemaAnexoRepository _repositoryAttachment;
        private ILikeRepository _repositoryLike;
        private IVoluntarioRepository _repositoryVoluntary;
        private readonly IMapper _mapper;

        public ProblemaService(IRepository<ProblemaEntity> repository, IProblemaAnexoRepository repositoryAttachment, ILikeRepository repositoryLike, IVoluntarioRepository repositoryVoluntary, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _repositoryAttachment = repositoryAttachment;
            _repositoryLike = repositoryLike;
            _repositoryVoluntary = repositoryVoluntary;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<ProblemaPresenter>> GetPaged(int page, int pageSize, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            IQueryable<ProblemaEntity> query = _repository.GetQuery();

            if (!string.IsNullOrEmpty(problemSearch))
            {
                query = query.Where(x => x.DesProblema.Contains(problemSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(benefitTypeSearch))
            {
                query = query.Where(x => x.IndTipoBeneficio == benefitTypeSearch.Trim());
            }

            if (!string.IsNullOrEmpty(solutionTypeSearch))
            {
                query = query.Where(x => x.IndTipoSolucao == solutionTypeSearch.Trim());
            }

            if (!string.IsNullOrEmpty(approvedSearch))
            {
                query = query.Where(x => x.IndAprovado == approvedSearch.Trim());
            }

            if (DateTime.TryParse(registrationDateIniSearch, out DateTime dateIni))
            {
                query = query.Where(x => x.DataCriacao >= dateIni);
            }

            if (DateTime.TryParse(registrationDateEndSearch, out DateTime dateEnd))
            {
                query = query.Where(x => x.DataCriacao <= dateEnd);
            }

            query = query.OrderBy(x => x.IndAprovado).ThenByDescending(x => x.DataCriacao);
            query = query.Include(x => x.Usuario);

            var result = _mapper.Map<PagedResultPresenter<ProblemaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<ProblemaPresenter>> GetPagedByUser(int page, int pageSize, Guid userId, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            IQueryable<ProblemaEntity> query = _repository.GetQuery();
            query = query.Where(x => x.UsuarioId == userId);

            if (!string.IsNullOrEmpty(problemSearch))
            {
                query = query.Where(x => x.DesProblema.Contains(problemSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(benefitTypeSearch))
            {
                query = query.Where(x => x.IndTipoBeneficio == benefitTypeSearch.Trim());
            }

            if (!string.IsNullOrEmpty(solutionTypeSearch))
            {
                query = query.Where(x => x.IndTipoSolucao == solutionTypeSearch.Trim());
            }

            if (!string.IsNullOrEmpty(approvedSearch))
            {
                query = query.Where(x => x.IndAprovado == approvedSearch.Trim());
            }

            if (DateTime.TryParse(registrationDateIniSearch, out DateTime dateIni))
            {
                query = query.Where(x => x.DataCriacao >= dateIni);
            }

            if (DateTime.TryParse(registrationDateEndSearch, out DateTime dateEnd))
            {
                query = query.Where(x => x.DataCriacao <= dateEnd);
            }

            query = query.OrderBy(x => x.IndAprovado).ThenByDescending(x => x.DataCriacao);
            query = query.Include(x => x.Usuario);

            var result = _mapper.Map<PagedResultPresenter<ProblemaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<ProblemaPresenter>> GetPagedInitialScreen(int page, int pageSize, Guid? userId, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            IQueryable<ProblemaEntity> query = _repository.GetQuery();

            query = query.Where(x => x.IndAprovado == "1");

            if (!string.IsNullOrEmpty(problemSearch))
            {
                query = query.Where(x => x.DesProblema.Contains(problemSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(benefitTypeSearch))
            {
                query = query.Where(x => x.IndTipoBeneficio == benefitTypeSearch.Trim());
            }

            if (!string.IsNullOrEmpty(solutionTypeSearch))
            {
                query = query.Where(x => x.IndTipoSolucao == solutionTypeSearch.Trim());
            }

            if (DateTime.TryParse(registrationDateIniSearch, out DateTime dateIni))
            {
                query = query.Where(x => x.DataCriacao >= dateIni);
            }

            if (DateTime.TryParse(registrationDateEndSearch, out DateTime dateEnd))
            {
                query = query.Where(x => x.DataCriacao <= dateEnd);
            }

            query = query.OrderByDescending(x => x.DataCriacao);

            var result = _mapper.Map<PagedResultPresenter<ProblemaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetailInitial(result, userId);
        }

        public override async Task<ProblemaPresenter> Post(ProblemaPostDto dto)
        {
            var entity = _mapper.Map<ProblemaEntity>(dto);
            entity.IndAprovado = "0";
            entity.IndAtivo = "1";

            ProblemaEntity dtoResult = await _repository.InsertAsync(entity);

            if (dto.Anexos != null && dto.Anexos.Count > 0)
            {
                foreach (var attach in dto.Anexos)
                {
                    attach.ProblemaId = dtoResult.Id;
                    await _repositoryAttachment.InsertAsync(_mapper.Map<ProblemaAnexoEntity>(attach));
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
                    await _repositoryAttachment.InsertAsync(_mapper.Map<ProblemaAnexoEntity>(attach));
                }
            }

            return _mapper.Map<ProblemaPresenter>(dtoResult);
        }

        public async Task<ProblemaPresenter> PutAvaliacao(ProblemaAvaliacaoPutDto dto)
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
                    await _repositoryAttachment.InsertAsync(_mapper.Map<ProblemaAnexoEntity>(attach));
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
                    var listAttachments = new List<ProblemaAnexoPresenter>();
                    var attachments = await _repositoryAttachment.GetByProjectAsync(item.Id);
                    foreach (var attach in attachments)
                    {
                        listAttachments.Add(_mapper.Map<ProblemaAnexoPresenter>(attach));
                    }
                    item.Anexos = listAttachments;

                    item.NumLikes = 0;
                    var likeQuery = await _repositoryLike.GetQuery().Where(x => x.ProblemaId.ToString() == item.Id).ToListAsync();
                    if (likeQuery != null && likeQuery.Count > 0)
                    {
                        item.NumLikes = likeQuery.Count;
                    }
                }
            }

            return result;
        }

        private async Task<PagedResultPresenter<ProblemaPresenter>> GetPresenterDetailInitial(PagedResultPresenter<ProblemaPresenter> result, Guid? userId)
        {
            if (result != null && result.Results != null)
            {
                foreach (var item in result.Results)
                {
                    item.Usuario = null;

                    var listAttachments = new List<ProblemaAnexoPresenter>();
                    var attachments = await _repositoryAttachment.GetByProjectAsync(item.Id);
                    foreach (var attach in attachments)
                    {
                        listAttachments.Add(_mapper.Map<ProblemaAnexoPresenter>(attach));
                    }
                    item.Anexos = listAttachments;

                    item.NumLikes = 0;
                    var likeQuery = await _repositoryLike.GetQuery().Where(x => x.ProblemaId.ToString() == item.Id).ToListAsync();
                    if (likeQuery != null && likeQuery.Count > 0)
                    {
                        item.NumLikes = likeQuery.Count;
                    }

                    item.VoluntarioId = null;
                    if (userId.HasValue)
                    {
                        var voluntaryQuery = await _repositoryVoluntary.GetQuery().Where(x => x.ProblemaId.ToString() == item.Id && x.UsuarioId == userId.Value).FirstOrDefaultAsync();
                        if (voluntaryQuery != null)
                        {
                            item.VoluntarioId = voluntaryQuery.Id.ToString();
                        }
                    }
                }
            }

            return result;
        }
    }
}
