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
    public class IdeiaAnexoTest : BaseFixture
    {
        protected IIdeiaAnexoRepository _ideiaAnexoRepository;
        protected IdeiaAnexoBuilder _ideiaAnexoBuilder;
        protected IdeiaAnexoEntity _ideiaAnexoEntity;

        public IdeiaAnexoTest(TestFixture<Startup> fixture) : base(fixture)
        {
            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            var ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            var ideiaEntity = ideiaBuilder.CreateInDataBase(ideiaBuilder.InstanciarObjeto()).Result;

            _ideiaAnexoRepository = _testServer.Services.GetService<IIdeiaAnexoRepository>();
            _ideiaAnexoBuilder = new IdeiaAnexoBuilder(_ideiaAnexoRepository, ideiaEntity);
            _ideiaAnexoEntity = _ideiaAnexoBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoEntity);

            await _ideiaAnexoRepository.DeleteAsync(ideiaAnexo.Id);

            var ideiaAnexoSearch = await _ideiaAnexoBuilder.Search(ideiaAnexo.Id);
            Assert.Null(ideiaAnexoSearch);
        }

        [Fact]
        public async Task TestExistsAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoEntity);

            var result = await _ideiaAnexoRepository.ExistsAsync(ideiaAnexo.Id);
            Assert.True(result);
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var ideiaAnexoList = await _ideiaAnexoRepository.GetPaged(_ideiaAnexoRepository.GetQuery(), 1, 10);
            Assert.NotNull(ideiaAnexoList);
            Assert.Equal(4, ideiaAnexoList.RowCount);
        }

        [Fact]
        public async Task TestInsertAsync()
        {
            await _ideiaAnexoRepository.InsertAsync(_ideiaAnexoEntity);

            var ideiaAnexoSearch = await _ideiaAnexoBuilder.Search(_ideiaAnexoEntity.Id);
            Assert.NotNull(ideiaAnexoSearch);
            Assert.Equal(_ideiaAnexoEntity.DesNomeOriginal, ideiaAnexoSearch.DesNomeOriginal);
        }

        [Fact]
        public async Task TestSelectOneAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var ideiaAnexoSearch = await _ideiaAnexoRepository.SelectAsync(ideiaAnexo.Id);
            Assert.NotNull(ideiaAnexoSearch);
            Assert.Equal(ideiaAnexo.Id, ideiaAnexoSearch.Id);
        }

        [Fact]
        public async Task TestSelectManyAsync()
        {
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var ideiaAnexoSearch = (await _ideiaAnexoRepository.SelectAsync()).ToList();
            Assert.NotNull(ideiaAnexoSearch);
            Assert.Equal(4, ideiaAnexoSearch.Count);
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoEntity);
            ideiaAnexo.DesNomeOriginal = _faker.Lorem.Paragraph();

            await _ideiaAnexoRepository.UpdateAsync(ideiaAnexo);

            var ideiaAnexoSearch = await _ideiaAnexoBuilder.Search(_ideiaAnexoEntity.Id);
            Assert.NotNull(ideiaAnexoSearch);
            Assert.Equal(ideiaAnexo.DesNomeOriginal, ideiaAnexoSearch.DesNomeOriginal);
        }
    }
}
