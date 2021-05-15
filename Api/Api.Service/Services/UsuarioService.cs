using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Util;
using AutoMapper;
using System;
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
                query = query.Where(x => x.DesNome.Contains(nameSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(emailSearch))
            {
                query = query.Where(x => x.DesEmail.Contains(emailSearch.Trim()));
            }

            if (!string.IsNullOrEmpty(foneSearch))
            {
                query = query.Where(x => x.DesTelefone.Contains(foneSearch.Trim()));
            }

            if (isAdminSearch.HasValue)
            {
                query = query.Where(x => x.Admin == (isAdminSearch.Value ? 1 : 0));
            }

            return _mapper.Map<PagedResultPresenter<UsuarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
        }

        public override async Task<UsuarioPresenter> Post(UsuarioPostDto dto)
        {
            if (!string.IsNullOrEmpty(dto.DesSenha))
            {
                const int WorkFactor = 14;
                dto.DesSenha = BCrypt.Net.BCrypt.HashPassword(dto.DesSenha, WorkFactor);
            }

            if (!string.IsNullOrEmpty(dto.DesImagem))
            {
                dto.DesImagem = ConvertUrlToBase64.ImageResize(dto.DesImagem);
            }
            else
            {
                dto.DesImagem = null;
            }

            var dtoResult = await _repository.InsertAsync(_mapper.Map<UsuarioEntity>(dto));
            return _mapper.Map<UsuarioPresenter>(dtoResult);
        }

        public override async Task<UsuarioPresenter> Put(UsuarioPutDto dto)
        {
            var obj = await _repository.SelectAsync(Guid.Parse(dto.Id));

            if (!string.IsNullOrEmpty(dto.DesSenha))
            {
                const int WorkFactor = 14;
                dto.DesSenha = BCrypt.Net.BCrypt.HashPassword(dto.DesSenha, WorkFactor);
            }
            else
            {
                dto.DesSenha = obj.DesSenha;
            }

            if (!string.IsNullOrEmpty(dto.DesImagem))
            {
                dto.DesImagem = ConvertUrlToBase64.ImageResize(dto.DesImagem);
            }
            else
            {
                dto.DesImagem = null;
            }

            Reflection.CopyProperties(dto, obj);

            var dtoResult = await _repository.UpdateAsync(obj);
            return _mapper.Map<UsuarioPresenter>(dtoResult);
        }
    }
}
