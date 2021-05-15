using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Util;
using Api.Tests.Integration.Builders;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Controller
{
    public class LikeTest : BaseFixture
    {
        protected ILikeService<LikeEntity, LikePresenter, LikePostDto, LikePutDto> _likeService;
        protected IUsuarioRepository _userRepository;
        protected LikeBuilder _likeIdeiaBuilder;
        protected LikeBuilder _likeProblemaBuilder;
        protected LikeEntity _likeIdeiaEntity;
        protected LikeEntity _likeProblemaEntity;

        public LikeTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _likeService = _testServer.Services.GetService<ILikeService<LikeEntity, LikePresenter, LikePostDto, LikePutDto>>();

            _userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(_userRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            var ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            var ideiaEntity = ideiaBuilder.CreateInDataBase(ideiaBuilder.InstanciarObjeto()).Result;

            var problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            var problemaBuilder = new ProblemaBuilder(problemaRepository, usuarioEntity);
            var problemaEntity = problemaBuilder.CreateInDataBase(problemaBuilder.InstanciarObjeto()).Result;

            var likeRepository = _testServer.Services.GetService<ILikeRepository>();
            _likeIdeiaBuilder = new LikeBuilder(likeRepository, ideiaEntity, null);
            _likeIdeiaEntity = _likeIdeiaBuilder.InstanciarObjeto();
            _likeProblemaBuilder = new LikeBuilder(likeRepository, null, problemaEntity);
            _likeProblemaEntity = _likeProblemaBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestIdeiaPostAsync()
        {
            var likeDto = new LikePostDto();
            Reflection.CopyProperties(_likeIdeiaEntity, likeDto);
            likeDto.Key = "ac5cc8c6-e508-4354-bb12-634a6390900b";

            var request = new
            {
                Url = "/api/like/post",
                Body = likeDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaPostAsync()
        {
            var likeDto = new LikePostDto();
            Reflection.CopyProperties(_likeProblemaEntity, likeDto);
            likeDto.Key = "ac5cc8c6-e508-4354-bb12-634a6390900b";

            var request = new
            {
                Url = "/api/like/post",
                Body = likeDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }
    }
}
