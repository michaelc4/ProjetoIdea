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
    public class IdeiaTest : BaseFixture
    {
        protected IIdeiaRepository _ideiaRepository;
        protected IdeiaBuilder _ideiaBuilder;
        protected IdeiaEntity _ideiaEntity;

        public IdeiaTest(TestFixture<Startup> fixture) : base(fixture)
        {
            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            _ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            _ideiaBuilder = new IdeiaBuilder(_ideiaRepository, usuarioEntity);
            _ideiaEntity = _ideiaBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {           
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaEntity);

            await _ideiaRepository.DeleteAsync(ideia.Id);

            var ideiaSearch = await _ideiaBuilder.Search(ideia.Id);
            Assert.Null(ideiaSearch);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestExistsAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaEntity);

            var result = await _ideiaRepository.ExistsAsync(ideia.Id);
            Assert.True(result);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var ideiaList = await _ideiaRepository.GetPaged(_ideiaRepository.GetQuery(), 1, 10);
            Assert.NotNull(ideiaList);
            Assert.Equal(4, ideiaList.RowCount);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestInsertAsync()
        {
            await _ideiaRepository.InsertAsync(_ideiaEntity);

            var ideiaSearch = await _ideiaBuilder.Search(_ideiaEntity.Id);
            Assert.NotNull(ideiaSearch);
            Assert.Equal(_ideiaEntity.DesIdeia, ideiaSearch.DesIdeia);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestSelectOneAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var ideiaSearch = await _ideiaRepository.SelectAsync(ideia.Id);
            Assert.NotNull(ideiaSearch);
            Assert.Equal(ideia.Id, ideiaSearch.Id);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestSelectManyAsync()
        {
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var ideiaSearch = (await _ideiaRepository.SelectAsync()).ToList();
            Assert.NotNull(ideiaSearch);
            Assert.Equal(4, ideiaSearch.Count);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaEntity);
            ideia.DesIdeia = _faker.Lorem.Paragraph();

            await _ideiaRepository.UpdateAsync(ideia);

            var ideiaSearch = await _ideiaBuilder.Search(_ideiaEntity.Id);
            Assert.NotNull(ideiaSearch);
            Assert.Equal(ideia.DesIdeia, ideiaSearch.DesIdeia);

            await ResetDatabase();
        }
    }
}
