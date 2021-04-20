using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IRepository<UsuarioEntity> _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IRepository<UsuarioEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UsuarioPresenter> Get(Guid id)
        {
            return _mapper.Map<UsuarioPresenter>(await _repository.SelectAsync(id));
        }

        public async Task<IEnumerable<UsuarioPresenter>> GetAll()
        {
            return _mapper.Map<IEnumerable<UsuarioPresenter>>(await _repository.SelectAsync());
        }

        public async Task<PagedResultPresenter<UsuarioPresenter>> GetPaged(int page, int pageSize)
        {
            IQueryable<UsuarioEntity> query = _repository.GetQuery();

            return _mapper.Map<PagedResultPresenter<UsuarioPresenter>>(await _repository.GetPaged(query, page, pageSize));
        }

        public async Task<UsuarioPresenter> Put(UsuarioPutDto usuario)
        {
            UsuarioEntity usuarioEntity = await _repository.UpdateAsync(_mapper.Map<UsuarioEntity>(usuario));
            return _mapper.Map<UsuarioPresenter>(usuarioEntity);
        }
    }
}
