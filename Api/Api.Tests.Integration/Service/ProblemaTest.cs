using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Util;
using Api.Tests.Integration.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Service
{
    public class ProblemaTest : BaseFixture
    {
        protected IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto> _problemaService;
        protected IProblemaAnexoRepository _problemaAnexoRepository;
        protected ProblemaBuilder _problemaBuilder;
        protected ProblemaEntity _problemaEntity;
        protected ProblemaAnexoBuilder _problemaAnexoBuilder;
        protected Guid _userId;

        public ProblemaTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _problemaService = _testServer.Services.GetService<IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto>>();

            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            _userId = usuarioEntity.Id;

            var problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            _problemaBuilder = new ProblemaBuilder(problemaRepository, usuarioEntity);
            _problemaEntity = _problemaBuilder.InstanciarObjeto();

            _problemaAnexoRepository = _testServer.Services.GetService<IProblemaAnexoRepository>();
            _problemaAnexoBuilder = new ProblemaAnexoBuilder(_problemaAnexoRepository, _problemaEntity);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaEntity);

            await _problemaService.Delete(problema.Id);

            var problemaSearch = await _problemaBuilder.Search(problema.Id);
            Assert.Null(problemaSearch);
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var problemaSearch = (await _problemaService.GetAll()).ToList();
            Assert.NotNull(problemaSearch);
            Assert.Equal(4, problemaSearch.Count);
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var problemaSearch = await _problemaService.Get(problema.Id);
            Assert.NotNull(problemaSearch);
            Assert.Equal(problema.Id.ToString(), problemaSearch.Id);
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            var problemaTest = await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var problemaList = await _problemaService.GetPaged(1, 10, null, null, null, null, null, null);
            Assert.NotNull(problemaList);
            Assert.Equal(4, problemaList.RowCount);

            var problemaListOne = await _problemaService.GetPaged(1, 10, problemaTest.DesProblema, problemaTest.IndTipoBeneficio, problemaTest.IndTipoSolucao, null, null, null);
            Assert.NotNull(problemaListOne);
            Assert.Equal(1, problemaListOne.RowCount);
        }

        [Fact]
        public async Task TestGetPagedByUserAsync()
        {
            var problemaTest = await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var problemaList = await _problemaService.GetPagedByUser(1, 10, _userId, null, null, null, null, null, null);
            Assert.NotNull(problemaList);
            Assert.Equal(4, problemaList.RowCount);

            var problemaListOne = await _problemaService.GetPagedByUser(1, 10, _userId, problemaTest.DesProblema, problemaTest.IndTipoBeneficio, problemaTest.IndTipoSolucao, null, null, null);
            Assert.NotNull(problemaListOne);
            Assert.Equal(1, problemaListOne.RowCount);
        }

        [Fact]
        public async Task TestGetPagedInitialScreenAsync()
        {
            var problemaTest = await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var problemaList = await _problemaService.GetPagedInitialScreen(1, 10, null, null, null, null, null, null);
            Assert.NotNull(problemaList);
            Assert.Equal(4, problemaList.RowCount);
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var problemaDto = new ProblemaPostDto();
            Reflection.CopyProperties(_problemaEntity, problemaDto);

            List<ProblemaAnexoPostDto> listAnexos = new List<ProblemaAnexoPostDto>();
            for (int i = 0; i < 4; i++)
            {
                var anexoDto = new ProblemaAnexoPostDto();
                Reflection.CopyProperties(_problemaAnexoBuilder.InstanciarObjeto(), anexoDto);
                listAnexos.Add(anexoDto);
            }
            problemaDto.Anexos = listAnexos;

            var problemaPresenter = await _problemaService.Post(problemaDto);

            var problemaSearch = await _problemaBuilder.Search(Guid.Parse(problemaPresenter.Id));
            Assert.NotNull(problemaSearch);
            Assert.Equal(_problemaEntity.DesProblema, problemaSearch.DesProblema);

            var problemaListOne = await _problemaService.GetPaged(1, 10, _problemaEntity.DesProblema, _problemaEntity.IndTipoBeneficio, _problemaEntity.IndTipoSolucao, null, null, null);
            Assert.NotNull(problemaListOne);
            Assert.Equal(1, problemaListOne.RowCount);
            Assert.Equal(4, problemaListOne.Results[0].Anexos.Count());
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaEntity);
            var problemaAnexoBuilder = new ProblemaAnexoBuilder(_problemaAnexoRepository, problema);
            await problemaAnexoBuilder.CreateInDataBase(problemaAnexoBuilder.InstanciarObjeto());

            var problemaDto = new ProblemaPutDto();
            Reflection.CopyProperties(problema, problemaDto);
            problemaDto.Id = problema.Id.ToString();
            problemaDto.DesProblema = _faker.Lorem.Paragraph();

            List<ProblemaAnexoPostDto> listAnexos = new List<ProblemaAnexoPostDto>();
            for (int i = 0; i < 4; i++)
            {
                var anexoDto = new ProblemaAnexoPostDto();
                Reflection.CopyProperties(_problemaAnexoBuilder.InstanciarObjeto(), anexoDto);
                listAnexos.Add(anexoDto);
            }
            problemaDto.Anexos = listAnexos;

            await _problemaService.Put(problemaDto);

            var problemaSearch = await _problemaBuilder.Search(problema.Id);
            Assert.NotNull(problemaSearch);
            Assert.Equal(problemaDto.DesProblema, problemaSearch.DesProblema);

            var problemaListOne = await _problemaService.GetPaged(1, 10, _problemaEntity.DesProblema, _problemaEntity.IndTipoBeneficio, _problemaEntity.IndTipoSolucao, null, null, null);
            Assert.NotNull(problemaListOne);
            Assert.Equal(1, problemaListOne.RowCount);
            Assert.Equal(4, problemaListOne.Results[0].Anexos.Count());
        }

        [Fact]
        public async Task TestPutAvaliacaoAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaEntity);
            var problemaAnexoBuilder = new ProblemaAnexoBuilder(_problemaAnexoRepository, problema);
            await problemaAnexoBuilder.CreateInDataBase(problemaAnexoBuilder.InstanciarObjeto());

            var problemaDto = new ProblemaAvaliacaoPutDto();
            Reflection.CopyProperties(problema, problemaDto);
            problemaDto.Id = problema.Id.ToString();
            problemaDto.DesProblema = _faker.Lorem.Paragraph();

            List<ProblemaAnexoPostDto> listAnexos = new List<ProblemaAnexoPostDto>();
            for (int i = 0; i < 4; i++)
            {
                var anexoDto = new ProblemaAnexoPostDto();
                Reflection.CopyProperties(_problemaAnexoBuilder.InstanciarObjeto(), anexoDto);
                listAnexos.Add(anexoDto);
            }
            problemaDto.Anexos = listAnexos;

            await _problemaService.PutAvaliacao(problemaDto);

            var problemaSearch = await _problemaBuilder.Search(problema.Id);
            Assert.NotNull(problemaSearch);
            Assert.Equal(problemaDto.DesProblema, problemaSearch.DesProblema);

            var problemaListOne = await _problemaService.GetPaged(1, 10, _problemaEntity.DesProblema, _problemaEntity.IndTipoBeneficio, _problemaEntity.IndTipoSolucao, null, null, null);
            Assert.NotNull(problemaListOne);
            Assert.Equal(1, problemaListOne.RowCount);
            Assert.Equal(4, problemaListOne.Results[0].Anexos.Count());
        }
    }
}
