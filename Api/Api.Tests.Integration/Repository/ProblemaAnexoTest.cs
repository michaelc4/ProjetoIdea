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
    public class ProblemaAnexoTest : BaseFixture
    {
        protected IProblemaAnexoRepository _problemaAnexoRepository;
        protected ProblemaAnexoBuilder _problemaAnexoBuilder;
        protected ProblemaAnexoEntity _problemaAnexoEntity;

        public ProblemaAnexoTest(TestFixture<Startup> fixture) : base(fixture)
        {
            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            var problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            var problemaBuilder = new ProblemaBuilder(problemaRepository, usuarioEntity);
            var problemaEntity = problemaBuilder.CreateInDataBase(problemaBuilder.InstanciarObjeto()).Result;

            _problemaAnexoRepository = _testServer.Services.GetService<IProblemaAnexoRepository>();
            _problemaAnexoBuilder = new ProblemaAnexoBuilder(_problemaAnexoRepository, problemaEntity);
            _problemaAnexoEntity = _problemaAnexoBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoEntity);

            await _problemaAnexoRepository.DeleteAsync(problemaAnexo.Id);

            var problemaAnexoSearch = await _problemaAnexoBuilder.Search(problemaAnexo.Id);
            Assert.Null(problemaAnexoSearch);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestExistsAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoEntity);

            var result = await _problemaAnexoRepository.ExistsAsync(problemaAnexo.Id);
            Assert.True(result);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var problemaAnexoList = await _problemaAnexoRepository.GetPaged(_problemaAnexoRepository.GetQuery(), 1, 10);
            Assert.NotNull(problemaAnexoList);
            Assert.Equal(4, problemaAnexoList.RowCount);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestInsertAsync()
        {
            await _problemaAnexoRepository.InsertAsync(_problemaAnexoEntity);

            var problemaAnexoSearch = await _problemaAnexoBuilder.Search(_problemaAnexoEntity.Id);
            Assert.NotNull(problemaAnexoSearch);
            Assert.Equal(_problemaAnexoEntity.DesNomeOriginal, problemaAnexoSearch.DesNomeOriginal);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestSelectOneAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var problemaAnexoSearch = await _problemaAnexoRepository.SelectAsync(problemaAnexo.Id);
            Assert.NotNull(problemaAnexoSearch);
            Assert.Equal(problemaAnexo.Id, problemaAnexoSearch.Id);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestSelectManyAsync()
        {
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var problemaAnexoSearch = (await _problemaAnexoRepository.SelectAsync()).ToList();
            Assert.NotNull(problemaAnexoSearch);
            Assert.Equal(4, problemaAnexoSearch.Count);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoEntity);
            problemaAnexo.DesNomeOriginal = _faker.Lorem.Paragraph();

            await _problemaAnexoRepository.UpdateAsync(problemaAnexo);

            var problemaAnexoSearch = await _problemaAnexoBuilder.Search(_problemaAnexoEntity.Id);
            Assert.NotNull(problemaAnexoSearch);
            Assert.Equal(problemaAnexo.DesNomeOriginal, problemaAnexoSearch.DesNomeOriginal);

            await ResetDatabase();
        }
    }
}
