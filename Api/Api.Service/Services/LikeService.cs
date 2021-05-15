using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LikeService : BaseService<LikeEntity, LikePresenter, LikePostDto, LikePutDto>, ILikeService<LikeEntity, LikePresenter, LikePostDto, LikePutDto>
    {
        private IRepository<LikeEntity> _repository;
        private IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LikeService(IRepository<LikeEntity> repository, IConfiguration configuration, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public override async Task<LikePresenter> Post(LikePostDto dto)
        {
            string key = _configuration.GetValue<string>("Key");
            if (!string.IsNullOrEmpty(dto.Key) && BCrypt.Net.BCrypt.Verify(dto.Key, key))
            {
                var entity = _mapper.Map<LikeEntity>(dto);
                return _mapper.Map<LikePresenter>(await _repository.InsertAsync(entity));
            }

            return null;
        }
    }
}
