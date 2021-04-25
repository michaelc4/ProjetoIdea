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
    public class IdeiaService : BaseService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto>, IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto>
    {
        private IRepository<IdeiaEntity> _repository;
        private IUsuarioRepository _repositoryUser;
        private IIdeiaAnexoRepository _repositoryAttachment;
        private readonly IMapper _mapper;

        public IdeiaService(IRepository<IdeiaEntity> repository, IUsuarioRepository repositoryUser, IIdeiaAnexoRepository repositoryAttachment, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
            _repositoryAttachment = repositoryAttachment;
            _mapper = mapper;
        }

        public async Task<PagedResultPresenter<IdeiaPresenter>> GetPaged(int page, int pageSize)
        {
            IQueryable<IdeiaEntity> query = _repository.GetQuery();
            query = query.OrderBy(x => x.IndAprovado);

            var result = _mapper.Map<PagedResultPresenter<IdeiaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
        }

        public async Task<PagedResultPresenter<IdeiaPresenter>> GetPagedByUser(int page, int pageSize, Guid userId)
        {
            IQueryable<IdeiaEntity> query = _repository.GetQuery();
            query = query.Where(x => x.UsuarioId == userId);
            query = query.OrderBy(x => x.IndAprovado);

            var result = _mapper.Map<PagedResultPresenter<IdeiaPresenter>>(await _repository.GetPaged(query, page, pageSize));
            return await GetPresenterDetail(result);
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
                    await _repositoryAttachment.InsertAsync(_mapper.Map<IdeiaAnexoEntity>(dto));
                }
            }

            return _mapper.Map<IdeiaPresenter>(dtoResult);
        }

        public override async Task<IdeiaPresenter> Put(IdeiaPutDto dto)
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
                    await _repositoryAttachment.InsertAsync(_mapper.Map<IdeiaAnexoEntity>(dto));
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
                    var userEntity = await _repositoryUser.SelectAsync(item.UsuarioId);
                    item.Usuario = _mapper.Map<UsuarioPresenter>(userEntity);

                    var listAttachments = new List<IdeiaAnexoPresenter>();
                    var attachments = await _repositoryAttachment.GetByProjectAsync(item.Id);
                    foreach (var attach in attachments)
                    {
                        listAttachments.Add(_mapper.Map<IdeiaAnexoPresenter>(attach));
                    }
                    item.Anexos = listAttachments;
                }
            }

            return result;
        }
    }
}
