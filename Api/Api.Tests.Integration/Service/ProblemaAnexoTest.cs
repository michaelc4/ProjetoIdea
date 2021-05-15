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
    public class ProblemaAnexoTest : BaseFixture
    {
        protected IProblemaAnexoService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto> _problemaAnexoService;
        protected ProblemaAnexoBuilder _problemaAnexoBuilder;
        protected ProblemaAnexoEntity _problemaAnexoEntity;

        public ProblemaAnexoTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _problemaAnexoService = _testServer.Services.GetService<IProblemaAnexoService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto>>();

            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            var problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            var problemaBuilder = new ProblemaBuilder(problemaRepository, usuarioEntity);
            var problemaEntity = problemaBuilder.CreateInDataBase(problemaBuilder.InstanciarObjeto()).Result;

            var problemaAnexoRepository = _testServer.Services.GetService<IProblemaAnexoRepository>();
            _problemaAnexoBuilder = new ProblemaAnexoBuilder(problemaAnexoRepository, problemaEntity);
            _problemaAnexoEntity = _problemaAnexoBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoEntity);

            await _problemaAnexoService.Delete(problemaAnexo.Id);

            var problemaAnexoSearch = await _problemaAnexoBuilder.Search(problemaAnexo.Id);
            Assert.Null(problemaAnexoSearch);
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var problemaAnexoSearch = (await _problemaAnexoService.GetAll()).ToList();
            Assert.NotNull(problemaAnexoSearch);
            Assert.Equal(4, problemaAnexoSearch.Count);
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var problemaAnexoSearch = await _problemaAnexoService.Get(problemaAnexo.Id);
            Assert.NotNull(problemaAnexoSearch);
            Assert.Equal(problemaAnexo.DesNomeOriginal, problemaAnexoSearch.DesNomeOriginal);
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var userList = await _problemaAnexoService.GetPaged(1, 10);
            Assert.NotNull(userList);
            Assert.Equal(4, userList.RowCount);
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var problemaAnexoDto = new ProblemaAnexoPostDto();
            Reflection.CopyProperties(_problemaAnexoEntity, problemaAnexoDto);

            var problemaAnexoPresenter = await _problemaAnexoService.Post(problemaAnexoDto);

            Assert.NotNull(problemaAnexoPresenter);

            var problemaAnexoSearch = await _problemaAnexoBuilder.Search(Guid.Parse(problemaAnexoPresenter.Id));
            Assert.NotNull(problemaAnexoSearch);
            Assert.Equal(_problemaAnexoEntity.DesNomeOriginal, problemaAnexoSearch.DesNomeOriginal);
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoEntity);

            var problemaAnexoDto = new ProblemaAnexoPutDto();
            Reflection.CopyProperties(problemaAnexo, problemaAnexoDto);
            problemaAnexoDto.Id = problemaAnexo.Id.ToString();
            problemaAnexoDto.DesNomeOriginal = _faker.System.FileName();

            await _problemaAnexoService.Put(problemaAnexoDto);

            var problemaAnexoSearch = await _problemaAnexoBuilder.Search(problemaAnexo.Id);
            Assert.NotNull(problemaAnexoSearch);
            Assert.Equal(problemaAnexoDto.DesNomeOriginal, problemaAnexoSearch.DesNomeOriginal);
        }
    }
}
