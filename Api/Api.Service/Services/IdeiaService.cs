﻿using Api.Domain.Dtos;
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
    public class IdeiaService : BaseService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto>, IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto>
    {
        private IRepository<IdeiaEntity> _repository;
        private IIdeiaAnexoRepository _repositoryAttachment;
        private ILikeRepository _repositoryLike;
        private IVoluntarioRepository _repositoryVoluntary;
        private readonly IMapper _mapper;

        public IdeiaService(IRepository<IdeiaEntity> repository, IIdeiaAnexoRepository repositoryAttachment, ILikeRepository repositoryLike, IVoluntarioRepository repositoryVoluntary, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _repositoryAttachment = repositoryAttachment;
            _repositoryLike = repositoryLike;
            _repositoryVoluntary = repositoryVoluntary;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<IdeiaPresenter>> GetPaged(int page, int pageSize, string ideaSearch, string reasonSearch, string shareSearch, string developmentSearch, string secretSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            IQueryable<IdeiaEntity> query = _repository.GetQuery();

            if (!string.IsNullOrEmpty(ideaSearch))
            {
                query = query.Where(x => x.DesIdeia.Contains(ideaSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(reasonSearch))
            {
                query = query.Where(x => x.DesMotivoInvestir.Contains(reasonSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(shareSearch))
            {
                query = query.Where(x => x.IndInteresseCompartilhar == shareSearch.Trim());
            }

            if (!string.IsNullOrEmpty(developmentSearch))
            {
                query = query.Where(x => x.IndNivelDesenvolvimento == developmentSearch.Trim());
            }

            if (!string.IsNullOrEmpty(secretSearch))
            {
                query = query.Where(x => x.IndNivelSigilo == secretSearch.Trim());
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

            var result = _mapper.Map<PagedResultPresenter<IdeiaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<IdeiaPresenter>> GetPagedByUser(int page, int pageSize, Guid userId, string ideaSearch, string reasonSearch, string shareSearch, string developmentSearch, string secretSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            IQueryable<IdeiaEntity> query = _repository.GetQuery();
            query = query.Where(x => x.UsuarioId == userId);

            if (!string.IsNullOrEmpty(ideaSearch))
            {
                query = query.Where(x => x.DesIdeia.Contains(ideaSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(reasonSearch))
            {
                query = query.Where(x => x.DesMotivoInvestir.Contains(reasonSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(shareSearch))
            {
                query = query.Where(x => x.IndInteresseCompartilhar == shareSearch.Trim());
            }

            if (!string.IsNullOrEmpty(developmentSearch))
            {
                query = query.Where(x => x.IndNivelDesenvolvimento == developmentSearch.Trim());
            }

            if (!string.IsNullOrEmpty(secretSearch))
            {
                query = query.Where(x => x.IndNivelSigilo == secretSearch.Trim());
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

            query = query.OrderByDescending(x => x.IndAprovado).ThenByDescending(x => x.DataCriacao);
            query = query.Include(x => x.Usuario);

            var result = _mapper.Map<PagedResultPresenter<IdeiaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<IdeiaPresenter>> GetPagedInitialScreen(int page, int pageSize, Guid? userId, string ideaSearch, string reasonSearch, string shareSearch, string developmentSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            IQueryable<IdeiaEntity> query = _repository.GetQuery();

            query = query.Where(x => x.IndNivelSigilo == "1");
            query = query.Where(x => x.IndAprovado == "1");

            if (!string.IsNullOrEmpty(ideaSearch))
            {
                query = query.Where(x => x.DesIdeia.Contains(ideaSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(reasonSearch))
            {
                query = query.Where(x => x.DesMotivoInvestir.Contains(reasonSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(shareSearch))
            {
                query = query.Where(x => x.IndInteresseCompartilhar == shareSearch.Trim());
            }

            if (!string.IsNullOrEmpty(developmentSearch))
            {
                query = query.Where(x => x.IndNivelDesenvolvimento == developmentSearch.Trim());
            }

            if (DateTime.TryParse(registrationDateIniSearch, out DateTime dateIni))
            {
                query = query.Where(x => x.DataCriacao >= dateIni);
            }

            if (DateTime.TryParse(registrationDateEndSearch, out DateTime dateEnd))
            {
                query = query.Where(x => x.DataCriacao <= dateEnd);
            }

            query = query.OrderByDescending(x => x.NumPontuacaoGeral).ThenByDescending(x => x.DataCriacao);

            var result = _mapper.Map<PagedResultPresenter<IdeiaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetailInitial(result, userId);
        }

        public override async Task<IdeiaPresenter> Post(IdeiaPostDto dto)
        {
            var entity = _mapper.Map<IdeiaEntity>(dto);
            entity.IndAprovado = "0";
            entity.IndAtivo = "1";

            IdeiaEntity dtoResult = await _repository.InsertAsync(entity);

            if (dto.Anexos != null && dto.Anexos.Count > 0)
            {
                foreach (var attach in dto.Anexos)
                {
                    attach.IdeiaId = dtoResult.Id;
                    await _repositoryAttachment.InsertAsync(_mapper.Map<IdeiaAnexoEntity>(attach));
                }
            }

            return _mapper.Map<IdeiaPresenter>(dtoResult);
        }

        public override async Task<IdeiaPresenter> Put(IdeiaPutDto dto)
        {
            var item = await _repository.SelectAsync(Guid.Parse(dto.Id));
            if (item.UsuarioId == dto.UsuarioId)
            {
                IdeiaEntity dtoResult = await _repository.UpdateAsync(_mapper.Map<IdeiaEntity>(dto));

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
                        attach.IdeiaId = dtoResult.Id;
                        await _repositoryAttachment.InsertAsync(_mapper.Map<IdeiaAnexoEntity>(attach));
                    }
                }

                return _mapper.Map<IdeiaPresenter>(dtoResult);
            }

            return null;
        }

        public async Task<IdeiaPresenter> PutAvaliacao(IdeiaAvaliacaoPutDto dto)
        {
            IdeiaEntity dtoResult = await _repository.UpdateAsync(_mapper.Map<IdeiaEntity>(dto));

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
                    attach.IdeiaId = dtoResult.Id;
                    await _repositoryAttachment.InsertAsync(_mapper.Map<IdeiaAnexoEntity>(attach));
                }
            }

            return _mapper.Map<IdeiaPresenter>(dtoResult);
        }

        private async Task<PagedResultPresenter<IdeiaPresenter>> GetPresenterDetail(PagedResultPresenter<IdeiaPresenter> result)
        {
            if (result != null && result.Results != null)
            {
                foreach (var item in result.Results)
                {
                    var listAttachments = new List<IdeiaAnexoPresenter>();
                    var attachments = await _repositoryAttachment.GetByProjectAsync(item.Id);
                    foreach (var attach in attachments)
                    {
                        listAttachments.Add(_mapper.Map<IdeiaAnexoPresenter>(attach));
                    }
                    item.Anexos = listAttachments;

                    item.NumLikes = 0;
                    var likeQuery = await _repositoryLike.GetQuery().Where(x => x.IdeiaId.ToString() == item.Id).ToListAsync();
                    if (likeQuery != null && likeQuery.Count > 0)
                    {
                        item.NumLikes = likeQuery.Count;
                    }
                }
            }

            return result;
        }

        private async Task<PagedResultPresenter<IdeiaPresenter>> GetPresenterDetailInitial(PagedResultPresenter<IdeiaPresenter> result, Guid? userId)
        {
            if (result != null && result.Results != null)
            {
                foreach (var item in result.Results)
                {
                    item.Usuario = null;
                    item.UsuarioId = Guid.NewGuid();

                    var listAttachments = new List<IdeiaAnexoPresenter>();
                    var attachments = await _repositoryAttachment.GetByProjectAsync(item.Id);
                    foreach (var attach in attachments)
                    {
                        listAttachments.Add(_mapper.Map<IdeiaAnexoPresenter>(attach));
                    }
                    item.Anexos = listAttachments;

                    item.NumLikes = 0;
                    var likeQuery = await _repositoryLike.GetQuery().Where(x => x.IdeiaId.ToString() == item.Id).ToListAsync();
                    if (likeQuery != null && likeQuery.Count > 0)
                    {
                        item.NumLikes = likeQuery.Count;
                    }

                    item.VoluntarioId = null;
                    if (userId.HasValue)
                    {
                        var voluntaryQuery = await _repositoryVoluntary.GetQuery().Where(x => x.IdeiaId.ToString() == item.Id && x.UsuarioId == userId.Value).FirstOrDefaultAsync();
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
