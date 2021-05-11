using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Util;
using Api.Tests.Integration.Builders;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Service
{
    public class LikeTest : BaseFixture
    {
        protected ILikeService<LikeEntity, LikePresenter, LikePostDto, LikePutDto> _likeService;
        protected LikeBuilder _likeIdeiaBuilder;
        protected LikeBuilder _likeProblemaBuilder;
        protected LikeEntity _likeIdeiaEntity;
        protected LikeEntity _likeProblemaEntity;

        public LikeTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _likeService = _testServer.Services.GetService<ILikeService<LikeEntity, LikePresenter, LikePostDto, LikePutDto>>();

            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
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
        public async Task TestIdeiaDeleteAsync()
        {
            var like = await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaEntity);

            await _likeService.Delete(like.Id);

            var likeSearch = await _likeIdeiaBuilder.Search(like.Id);
            Assert.Null(likeSearch);
        }

        [Fact]
        public async Task TestIdeiaGetAllAsync()
        {
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());

            var likeSearch = (await _likeService.GetAll()).ToList();
            Assert.NotNull(likeSearch);
            Assert.Equal(4, likeSearch.Count);
        }

        [Fact]
        public async Task TestIdeiaGetAsync()
        {
            var like = await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());

            var likeSearch = await _likeService.Get(like.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(like.IpUsr, likeSearch.IpUsr);
        }

        [Fact]
        public async Task TestIdeiaPostAsync()
        {
            var likeDto = new LikePostDto();
            Reflection.CopyProperties(_likeIdeiaEntity, likeDto);
            likeDto.Key = "ac5cc8c6-e508-4354-bb12-634a6390900b";

            var likePresenter = await _likeService.Post(likeDto);

            Assert.NotNull(likePresenter);

            var likeSearch = await _likeIdeiaBuilder.Search(Guid.Parse(likePresenter.Id));
            Assert.NotNull(likeSearch);
            Assert.Equal(_likeIdeiaEntity.IpUsr, likeSearch.IpUsr);
        }

        [Fact]
        public async Task TestIdeiaPutAsync()
        {
            var like = await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaEntity);

            var likeDto = new LikePutDto();
            Reflection.CopyProperties(like, likeDto);
            likeDto.Id = like.Id.ToString();
            likeDto.IpUsr = _faker.Internet.Ip();

            await _likeService.Put(likeDto);

            var likeSearch = await _likeIdeiaBuilder.Search(like.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(likeDto.IpUsr, likeSearch.IpUsr);
        }

        [Fact]
        public async Task TestProblemaDeleteAsync()
        {
            var like = await _likeProblemaBuilder.CreateInDataBase(_likeProblemaEntity);

            await _likeService.Delete(like.Id);

            var likeSearch = await _likeProblemaBuilder.Search(like.Id);
            Assert.Null(likeSearch);
        }

        [Fact]
        public async Task TestProblemaGetAllAsync()
        {
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());

            var likeSearch = (await _likeService.GetAll()).ToList();
            Assert.NotNull(likeSearch);
            Assert.Equal(4, likeSearch.Count);
        }

        [Fact]
        public async Task TestProblemaGetAsync()
        {
            var like = await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());

            var likeSearch = await _likeService.Get(like.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(like.IpUsr, likeSearch.IpUsr);
        }

        [Fact]
        public async Task TestProblemaPostAsync()
        {
            var likeDto = new LikePostDto();
            Reflection.CopyProperties(_likeProblemaEntity, likeDto);
            likeDto.Key = "ac5cc8c6-e508-4354-bb12-634a6390900b";

            var likePresenter = await _likeService.Post(likeDto);

            Assert.NotNull(likePresenter);

            var likeSearch = await _likeProblemaBuilder.Search(Guid.Parse(likePresenter.Id));
            Assert.NotNull(likeSearch);
            Assert.Equal(_likeProblemaEntity.IpUsr, likeSearch.IpUsr);
        }

        [Fact]
        public async Task TestProblemaPutAsync()
        {
            var like = await _likeProblemaBuilder.CreateInDataBase(_likeProblemaEntity);

            var likeDto = new LikePutDto();
            Reflection.CopyProperties(like, likeDto);
            likeDto.Id = like.Id.ToString();
            likeDto.IpUsr = _faker.Internet.Ip();

            await _likeService.Put(likeDto);

            var likeSearch = await _likeProblemaBuilder.Search(like.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(likeDto.IpUsr, likeSearch.IpUsr);
        }
    }
}
