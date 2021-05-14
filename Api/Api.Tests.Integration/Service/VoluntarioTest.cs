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
    public class VoluntarioTest : BaseFixture
    {
        protected IVoluntarioService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto> _voluntarioService;
        protected VoluntarioBuilder _voluntarioIdeiaBuilder;
        protected VoluntarioBuilder _voluntarioProblemaBuilder;
        protected UsuarioBuilder _usuarioBuilder;
        protected VoluntarioEntity _voluntarioIdeiaEntity;
        protected VoluntarioEntity _voluntarioProblemaEntity;
        protected UsuarioEntity _usuarioVoluntario;
        protected IdeiaEntity _ideiaEntity;
        protected ProblemaEntity _problemaEntity;

        public VoluntarioTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _voluntarioService = _testServer.Services.GetService<IVoluntarioService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto>>();

            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            _usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto()).Result;
            _usuarioVoluntario = _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto()).Result;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            var ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            _ideiaEntity = ideiaBuilder.CreateInDataBase(ideiaBuilder.InstanciarObjeto()).Result;

            var problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            var problemaBuilder = new ProblemaBuilder(problemaRepository, usuarioEntity);
            _problemaEntity = problemaBuilder.CreateInDataBase(problemaBuilder.InstanciarObjeto()).Result;

            var voluntarioRepository = _testServer.Services.GetService<IVoluntarioRepository>();
            _voluntarioIdeiaBuilder = new VoluntarioBuilder(voluntarioRepository, _usuarioVoluntario, _ideiaEntity, null);
            _voluntarioIdeiaEntity = _voluntarioIdeiaBuilder.InstanciarObjeto();
            _voluntarioProblemaBuilder = new VoluntarioBuilder(voluntarioRepository, _usuarioVoluntario, null, _problemaEntity);
            _voluntarioProblemaEntity = _voluntarioProblemaBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestIdeiaDeleteAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaEntity);

            await _voluntarioService.Delete(voluntario.Id);

            var voluntarioSearch = await _voluntarioIdeiaBuilder.Search(voluntario.Id);
            Assert.Null(voluntarioSearch);
        }

        [Fact]
        public async Task TestIdeiaGetAllAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var voluntarioSearch = (await _voluntarioService.GetAll()).ToList();
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(4, voluntarioSearch.Count);
        }

        [Fact]
        public async Task TestIdeiaGetAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var voluntarioSearch = await _voluntarioService.Get(voluntario.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(voluntario.UsuarioId, voluntarioSearch.UsuarioId);
        }

        [Fact]
        public async Task TestIdeiaGetPagedAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var voluntarioList = await _voluntarioService.GetPaged(1, 10);
            Assert.NotNull(voluntarioList);
            Assert.Equal(4, voluntarioList.RowCount);

            foreach (var voluntario in voluntarioList.Results)
            {
                Assert.NotNull(voluntario.Ideia);
                Assert.Null(voluntario.Problema);
            }
        }

        [Fact]
        public async Task TestIdeiaGetPagedByUserAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var voluntarioList = await _voluntarioService.GetPagedByUser(1, 10, _usuarioVoluntario.Id);
            Assert.NotNull(voluntarioList);
            Assert.Equal(4, voluntarioList.RowCount);

            foreach (var voluntario in voluntarioList.Results)
            {
                Assert.NotNull(voluntario.Ideia);
                Assert.Null(voluntario.Problema);
            }
        }

        [Fact]
        public async Task TestIdeiaGetPagedByProblemOrIdeiaAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var voluntarioList = await _voluntarioService.GetPagedByProblemOrIdeia(1, 10, null, _ideiaEntity.Id);
            Assert.NotNull(voluntarioList);
            Assert.Equal(4, voluntarioList.RowCount);

            foreach (var voluntario in voluntarioList.Results)
            {
                Assert.NotNull(voluntario.Usuario);
            }
        }

        [Fact]
        public async Task TestIdeiaPostAsync()
        {
            var voluntarioDto = new VoluntarioPostDto();
            Reflection.CopyProperties(_voluntarioIdeiaEntity, voluntarioDto);

            var voluntarioPresenter = await _voluntarioService.Post(voluntarioDto);

            Assert.NotNull(voluntarioPresenter);

            var voluntarioSearch = await _voluntarioIdeiaBuilder.Search(Guid.Parse(voluntarioPresenter.Id));
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(_voluntarioIdeiaEntity.UsuarioId, voluntarioSearch.UsuarioId);
        }

        [Fact]
        public async Task TestIdeiaPutAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaEntity);
            var usuarioVoluntarioNew = await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var voluntarioDto = new VoluntarioPutDto();
            Reflection.CopyProperties(voluntario, voluntarioDto);
            voluntarioDto.Id = voluntario.Id.ToString();
            voluntarioDto.UsuarioId = usuarioVoluntarioNew.Id;

            await _voluntarioService.Put(voluntarioDto);

            var voluntarioSearch = await _voluntarioIdeiaBuilder.Search(voluntario.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(voluntarioDto.UsuarioId, voluntarioSearch.UsuarioId);
        }

        [Fact]
        public async Task TestProblemaDeleteAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaEntity);

            await _voluntarioService.Delete(voluntario.Id);

            var voluntarioSearch = await _voluntarioProblemaBuilder.Search(voluntario.Id);
            Assert.Null(voluntarioSearch);
        }

        [Fact]
        public async Task TestProblemaGetAllAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var voluntarioSearch = (await _voluntarioService.GetAll()).ToList();
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(4, voluntarioSearch.Count);
        }

        [Fact]
        public async Task TestProblemaGetAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var voluntarioSearch = await _voluntarioService.Get(voluntario.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(voluntario.UsuarioId, voluntarioSearch.UsuarioId);
        }

        [Fact]
        public async Task TestProblemaGetPagedAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var voluntarioList = await _voluntarioService.GetPaged(1, 10);
            Assert.NotNull(voluntarioList);
            Assert.Equal(4, voluntarioList.RowCount);

            foreach (var voluntario in voluntarioList.Results)
            {
                Assert.NotNull(voluntario.Problema);
                Assert.Null(voluntario.Ideia);
            }
        }

        [Fact]
        public async Task TestProblemaGetPagedByUserAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var voluntarioList = await _voluntarioService.GetPagedByUser(1, 10, _usuarioVoluntario.Id);
            Assert.NotNull(voluntarioList);
            Assert.Equal(4, voluntarioList.RowCount);

            foreach (var voluntario in voluntarioList.Results)
            {
                Assert.NotNull(voluntario.Problema);
                Assert.Null(voluntario.Ideia);
            }
        }

        [Fact]
        public async Task TestProblemaGetPagedByProblemOrIdeiaAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var voluntarioList = await _voluntarioService.GetPagedByProblemOrIdeia(1, 10, _problemaEntity.Id, null);
            Assert.NotNull(voluntarioList);
            Assert.Equal(4, voluntarioList.RowCount);

            foreach (var voluntario in voluntarioList.Results)
            {
                Assert.NotNull(voluntario.Usuario);
            }
        }

        [Fact]
        public async Task TestProblemaPostAsync()
        {
            var voluntarioDto = new VoluntarioPostDto();
            Reflection.CopyProperties(_voluntarioProblemaEntity, voluntarioDto);

            var voluntarioPresenter = await _voluntarioService.Post(voluntarioDto);

            Assert.NotNull(voluntarioPresenter);

            var voluntarioSearch = await _voluntarioProblemaBuilder.Search(Guid.Parse(voluntarioPresenter.Id));
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(_voluntarioProblemaEntity.UsuarioId, voluntarioSearch.UsuarioId);
        }

        [Fact]
        public async Task TestProblemaPutAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaEntity);
            var usuarioVoluntarioNew = await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var voluntarioDto = new VoluntarioPutDto();
            Reflection.CopyProperties(voluntario, voluntarioDto);
            voluntarioDto.Id = voluntario.Id.ToString();
            voluntarioDto.UsuarioId = usuarioVoluntarioNew.Id;

            await _voluntarioService.Put(voluntarioDto);

            var voluntarioSearch = await _voluntarioProblemaBuilder.Search(voluntario.Id);
            Assert.NotNull(voluntarioSearch);
            Assert.Equal(voluntarioDto.UsuarioId, voluntarioSearch.UsuarioId);
        }
    }
}
