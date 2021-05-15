using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Util;
using Api.Tests.Integration.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Service
{
    public class IdeiaAnexoTest : BaseFixture
    {
        protected IIdeiaAnexoService<IdeiaAnexoEntity, IdeiaAnexoPresenter, IdeiaAnexoPostDto, IdeiaAnexoPutDto> _ideiaAnexoService;
        protected IdeiaAnexoBuilder _ideiaAnexoBuilder;
        protected IdeiaAnexoEntity _ideiaAnexoEntity;

        public IdeiaAnexoTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _ideiaAnexoService = _testServer.Services.GetService<IIdeiaAnexoService<IdeiaAnexoEntity, IdeiaAnexoPresenter, IdeiaAnexoPostDto, IdeiaAnexoPutDto>>();

            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            var ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            var ideiaEntity = ideiaBuilder.CreateInDataBase(ideiaBuilder.InstanciarObjeto()).Result;

            var ideiaAnexoRepository = _testServer.Services.GetService<IIdeiaAnexoRepository>();
            _ideiaAnexoBuilder = new IdeiaAnexoBuilder(ideiaAnexoRepository, ideiaEntity);
            _ideiaAnexoEntity = _ideiaAnexoBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoEntity);

            await _ideiaAnexoService.Delete(ideiaAnexo.Id);

            var ideiaAnexoSearch = await _ideiaAnexoBuilder.Search(ideiaAnexo.Id);
            Assert.Null(ideiaAnexoSearch);
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var ideiaAnexoSearch = (await _ideiaAnexoService.GetAll()).ToList();
            Assert.NotNull(ideiaAnexoSearch);
            Assert.Equal(4, ideiaAnexoSearch.Count);
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var ideiaAnexoSearch = await _ideiaAnexoService.Get(ideiaAnexo.Id);
            Assert.NotNull(ideiaAnexoSearch);
            Assert.Equal(ideiaAnexo.DesNomeOriginal, ideiaAnexoSearch.DesNomeOriginal);
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var userList = await _ideiaAnexoService.GetPaged(1, 10);
            Assert.NotNull(userList);
            Assert.Equal(4, userList.RowCount);
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var ideiaAnexoDto = new IdeiaAnexoPostDto();
            Reflection.CopyProperties(_ideiaAnexoEntity, ideiaAnexoDto);

            var ideiaAnexoPresenter = await _ideiaAnexoService.Post(ideiaAnexoDto);

            Assert.NotNull(ideiaAnexoPresenter);

            var ideiaAnexoSearch = await _ideiaAnexoBuilder.Search(Guid.Parse(ideiaAnexoPresenter.Id));
            Assert.NotNull(ideiaAnexoSearch);
            Assert.Equal(_ideiaAnexoEntity.DesNomeOriginal, ideiaAnexoSearch.DesNomeOriginal);
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoEntity);

            var ideiaAnexoDto = new IdeiaAnexoPutDto();
            Reflection.CopyProperties(ideiaAnexo, ideiaAnexoDto);
            ideiaAnexoDto.Id = ideiaAnexo.Id.ToString();
            ideiaAnexoDto.DesNomeOriginal = _faker.System.FileName();

            await _ideiaAnexoService.Put(ideiaAnexoDto);

            var ideiaAnexoSearch = await _ideiaAnexoBuilder.Search(ideiaAnexo.Id);
            Assert.NotNull(ideiaAnexoSearch);
            Assert.Equal(ideiaAnexoDto.DesNomeOriginal, ideiaAnexoSearch.DesNomeOriginal);
        }
    }
}
