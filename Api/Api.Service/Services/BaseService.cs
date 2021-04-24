using Api.Domain.Entities;
using Api.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public abstract class BaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {
        private IRepository<T> _repository;
        private readonly IMapper _mapper;

        public BaseService(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public virtual async Task<TPresenter> Get(Guid id)
        {
            return _mapper.Map<TPresenter>(await _repository.SelectAsync(id));
        }

        public virtual async Task<IEnumerable<TPresenter>> GetAll()
        {
            return _mapper.Map<IEnumerable<TPresenter>>(await _repository.SelectAsync());
        }

        public virtual async Task<TPresenter> Post(TPostDto dto)
        {
            T dtoResult = await _repository.InsertAsync(_mapper.Map<T>(dto));
            return _mapper.Map<TPresenter>(dtoResult);
        }

        public virtual async Task<TPresenter> Put(TPutDto dto)
        {
            T dtoResult = await _repository.UpdateAsync(_mapper.Map<T>(dto));
            return _mapper.Map<TPresenter>(dtoResult);
        }
    }
}
