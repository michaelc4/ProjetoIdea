using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Tests.Integration.Builders;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Repository
{
    public class LikeTest : BaseFixture
    {
        protected ILikeRepository _likeRepository;
        protected LikeBuilder _likeIdeiaBuilder;
        protected LikeBuilder _likeProblemaBuilder;
        protected LikeEntity _likeIdeiaEntity;
        protected LikeEntity _likeProblemaEntity;

        public LikeTest(TestFixture<Startup> fixture) : base(fixture)
        {
            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            var ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            var ideiaEntity = ideiaBuilder.CreateInDataBase(ideiaBuilder.InstanciarObjeto()).Result;

            var problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            var problemaBuilder = new ProblemaBuilder(problemaRepository, usuarioEntity);
            var problemaEntity = problemaBuilder.CreateInDataBase(problemaBuilder.InstanciarObjeto()).Result;

            _likeRepository = _testServer.Services.GetService<ILikeRepository>();
            _likeIdeiaBuilder = new LikeBuilder(_likeRepository, ideiaEntity, null);
            _likeIdeiaEntity = _likeIdeiaBuilder.InstanciarObjeto();
            _likeProblemaBuilder = new LikeBuilder(_likeRepository, null, problemaEntity);
            _likeProblemaEntity = _likeProblemaBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestIdeiaDeleteAsync()
        {
            var like = await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaEntity);

            await _likeRepository.DeleteAsync(like.Id);

            var likeSearch = await _likeIdeiaBuilder.Search(like.Id);
            Assert.Null(likeSearch);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaExistsAsync()
        {
            var like = await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaEntity);

            var result = await _likeRepository.ExistsAsync(like.Id);
            Assert.True(result);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaGetPagedAsync()
        {
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());

            var likeList = await _likeRepository.GetPaged(_likeRepository.GetQuery(), 1, 10);
            Assert.NotNull(likeList);
            Assert.Equal(4, likeList.RowCount);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaInsertAsync()
        {
            await _likeRepository.InsertAsync(_likeIdeiaEntity);

            var likeSearch = await _likeIdeiaBuilder.Search(_likeIdeiaEntity.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(_likeIdeiaEntity.IpUsr, likeSearch.IpUsr);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaSelectOneAsync()
        {
            var like = await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());

            var likeSearch = await _likeRepository.SelectAsync(like.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(like.Id, likeSearch.Id);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaSelectManyAsync()
        {
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());
            await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaBuilder.InstanciarObjeto());

            var likeSearch = (await _likeRepository.SelectAsync()).ToList();
            Assert.NotNull(likeSearch);
            Assert.Equal(4, likeSearch.Count);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaUpdateAsync()
        {
            var like = await _likeIdeiaBuilder.CreateInDataBase(_likeIdeiaEntity);
            like.IpUsr = _faker.Internet.Ip();

            await _likeRepository.UpdateAsync(like);

            var likeSearch = await _likeIdeiaBuilder.Search(_likeIdeiaEntity.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(like.IpUsr, likeSearch.IpUsr);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaDeleteAsync()
        {
            var like = await _likeProblemaBuilder.CreateInDataBase(_likeProblemaEntity);

            await _likeRepository.DeleteAsync(like.Id);

            var likeSearch = await _likeProblemaBuilder.Search(like.Id);
            Assert.Null(likeSearch);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaExistsAsync()
        {
            var like = await _likeProblemaBuilder.CreateInDataBase(_likeProblemaEntity);

            var result = await _likeRepository.ExistsAsync(like.Id);
            Assert.True(result);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaGetPagedAsync()
        {
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());

            var likeList = await _likeRepository.GetPaged(_likeRepository.GetQuery(), 1, 10);
            Assert.NotNull(likeList);
            Assert.Equal(4, likeList.RowCount);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaInsertAsync()
        {
            await _likeRepository.InsertAsync(_likeProblemaEntity);

            var likeSearch = await _likeProblemaBuilder.Search(_likeProblemaEntity.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(_likeProblemaEntity.IpUsr, likeSearch.IpUsr);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaSelectOneAsync()
        {
            var like = await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());

            var likeSearch = await _likeRepository.SelectAsync(like.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(like.Id, likeSearch.Id);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaSelectManyAsync()
        {
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());
            await _likeProblemaBuilder.CreateInDataBase(_likeProblemaBuilder.InstanciarObjeto());

            var likeSearch = (await _likeRepository.SelectAsync()).ToList();
            Assert.NotNull(likeSearch);
            Assert.Equal(4, likeSearch.Count);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaUpdateAsync()
        {
            var like = await _likeProblemaBuilder.CreateInDataBase(_likeProblemaEntity);
            like.IpUsr = _faker.Internet.Ip();

            await _likeRepository.UpdateAsync(like);

            var likeSearch = await _likeProblemaBuilder.Search(_likeProblemaEntity.Id);
            Assert.NotNull(likeSearch);
            Assert.Equal(like.IpUsr, likeSearch.IpUsr);

            await ResetDatabase();
        }
    }
}
