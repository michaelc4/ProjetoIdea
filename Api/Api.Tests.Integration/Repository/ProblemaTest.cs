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
    public class ProblemaTest : BaseFixture
    {
        protected IProblemaRepository _problemaRepository;
        protected ProblemaBuilder _problemaBuilder;
        protected ProblemaEntity _problemaEntity;

        public ProblemaTest(TestFixture<Startup> fixture) : base(fixture)
        {
            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            _problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            _problemaBuilder = new ProblemaBuilder(_problemaRepository, usuarioEntity);
            _problemaEntity = _problemaBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaEntity);

            await _problemaRepository.DeleteAsync(problema.Id);

            var problemaSearch = await _problemaBuilder.Search(problema.Id);
            Assert.Null(problemaSearch);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestExistsAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaEntity);

            var result = await _problemaRepository.ExistsAsync(problema.Id);
            Assert.True(result);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var problemaList = await _problemaRepository.GetPaged(_problemaRepository.GetQuery(), 1, 10);
            Assert.NotNull(problemaList);
            Assert.Equal(4, problemaList.RowCount);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestInsertAsync()
        {
            await _problemaRepository.InsertAsync(_problemaEntity);

            var problemaSearch = await _problemaBuilder.Search(_problemaEntity.Id);
            Assert.NotNull(problemaSearch);
            Assert.Equal(_problemaEntity.DesProblema, problemaSearch.DesProblema);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestSelectOneAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var problemaSearch = await _problemaRepository.SelectAsync(problema.Id);
            Assert.NotNull(problemaSearch);
            Assert.Equal(problema.Id, problemaSearch.Id);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestSelectManyAsync()
        {
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var problemaSearch = (await _problemaRepository.SelectAsync()).ToList();
            Assert.NotNull(problemaSearch);
            Assert.Equal(4, problemaSearch.Count);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaEntity);
            problema.DesProblema = _faker.Lorem.Paragraph();

            await _problemaRepository.UpdateAsync(problema);

            var problemaSearch = await _problemaBuilder.Search(_problemaEntity.Id);
            Assert.NotNull(problemaSearch);
            Assert.Equal(problema.DesProblema, problemaSearch.DesProblema);

            await ResetDatabase();
        }
    }
}
