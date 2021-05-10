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
    public class VoluntarioTest : BaseFixture
    {
        protected IVoluntarioRepository _voluntarioRepository;
        protected VoluntarioBuilder _voluntarioIdeiaBuilder;
        protected VoluntarioBuilder _voluntarioProblemaBuilder;
        protected VoluntarioEntity _voluntarioIdeiaEntity;
        protected VoluntarioEntity _voluntarioProblemaEntity;
        protected UsuarioEntity _usuarioEntityVoluntarioTest;

        public VoluntarioTest(TestFixture<Startup> fixture) : base(fixture)
        {
            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;
            var usuarioEntityVoluntario = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;
            _usuarioEntityVoluntarioTest = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            var ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            var ideiaEntity = ideiaBuilder.CreateInDataBase(ideiaBuilder.InstanciarObjeto()).Result;

            var problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            var problemaBuilder = new ProblemaBuilder(problemaRepository, usuarioEntity);
            var problemaEntity = problemaBuilder.CreateInDataBase(problemaBuilder.InstanciarObjeto()).Result;

            _voluntarioRepository = _testServer.Services.GetService<IVoluntarioRepository>();
            _voluntarioIdeiaBuilder = new VoluntarioBuilder(_voluntarioRepository, usuarioEntityVoluntario, ideiaEntity, null);
            _voluntarioIdeiaEntity = _voluntarioIdeiaBuilder.InstanciarObjeto();
            _voluntarioProblemaBuilder = new VoluntarioBuilder(_voluntarioRepository, usuarioEntityVoluntario, null, problemaEntity);
            _voluntarioProblemaEntity = _voluntarioProblemaBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestIdeiaDeleteAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaEntity);

            await _voluntarioRepository.DeleteAsync(voluntario.Id);

            var voluntarioSearch = await _voluntarioIdeiaBuilder.Search(voluntario.Id);
            Assert.Null(voluntarioSearch);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaExistsAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaEntity);

            var result = await _voluntarioRepository.ExistsAsync(voluntario.Id);
            Assert.True(result);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaGetPagedAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var voluntarioList = await _voluntarioRepository.GetPaged(_voluntarioRepository.GetQuery(), 1, 10);
            Assert.NotNull(voluntarioList);
            Assert.Equal(4, voluntarioList.RowCount);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaInsertAsync()
        {
            await _voluntarioRepository.InsertAsync(_voluntarioIdeiaEntity);

            var voluntarioSearch = await _voluntarioIdeiaBuilder.Search(_voluntarioIdeiaEntity.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(_voluntarioIdeiaEntity.UsuarioId, voluntarioSearch.UsuarioId);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaSelectOneAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var voluntarioSearch = await _voluntarioRepository.SelectAsync(voluntario.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(voluntario.Id, voluntarioSearch.Id);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaSelectManyAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var voluntarioSearch = (await _voluntarioRepository.SelectAsync()).ToList();
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(4, voluntarioSearch.Count);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestIdeiaUpdateAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaEntity);
            voluntario.UsuarioId = _usuarioEntityVoluntarioTest.Id;

            await _voluntarioRepository.UpdateAsync(voluntario);

            var voluntarioSearch = await _voluntarioIdeiaBuilder.Search(_voluntarioIdeiaEntity.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(voluntario.UsuarioId, voluntarioSearch.UsuarioId);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaDeleteAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaEntity);

            await _voluntarioRepository.DeleteAsync(voluntario.Id);

            var voluntarioSearch = await _voluntarioProblemaBuilder.Search(voluntario.Id);
            Assert.Null(voluntarioSearch);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaExistsAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaEntity);

            var result = await _voluntarioRepository.ExistsAsync(voluntario.Id);
            Assert.True(result);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaGetPagedAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var voluntarioList = await _voluntarioRepository.GetPaged(_voluntarioRepository.GetQuery(), 1, 10);
            Assert.NotNull(voluntarioList);
            Assert.Equal(4, voluntarioList.RowCount);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaInsertAsync()
        {
            await _voluntarioRepository.InsertAsync(_voluntarioProblemaEntity);

            var voluntarioSearch = await _voluntarioProblemaBuilder.Search(_voluntarioProblemaEntity.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(_voluntarioProblemaEntity.UsuarioId, voluntarioSearch.UsuarioId);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaSelectOneAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var voluntarioSearch = await _voluntarioRepository.SelectAsync(voluntario.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(voluntario.Id, voluntarioSearch.Id);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaSelectManyAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var voluntarioSearch = (await _voluntarioRepository.SelectAsync()).ToList();
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(4, voluntarioSearch.Count);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestProblemaUpdateAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaEntity);
            voluntario.UsuarioId = _usuarioEntityVoluntarioTest.Id;

            await _voluntarioRepository.UpdateAsync(voluntario);

            var voluntarioSearch = await _voluntarioProblemaBuilder.Search(_voluntarioProblemaEntity.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(voluntario.UsuarioId, voluntarioSearch.UsuarioId);

            await ResetDatabase();
        }
    }
}
