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
    public class IdeiaTest : BaseFixture
    {
        protected IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto> _ideiaService;
        protected IIdeiaAnexoRepository _ideiaAnexoRepository;
        protected IdeiaBuilder _ideiaBuilder;
        protected IdeiaEntity _ideiaEntity;
        protected IdeiaAnexoBuilder _ideiaAnexoBuilder;
        protected Guid _userId;

        public IdeiaTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _ideiaService = _testServer.Services.GetService<IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto>>();

            var usuarioRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(usuarioRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            _userId = usuarioEntity.Id;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            _ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            _ideiaEntity = _ideiaBuilder.InstanciarObjeto();

            _ideiaAnexoRepository = _testServer.Services.GetService<IIdeiaAnexoRepository>();
            _ideiaAnexoBuilder = new IdeiaAnexoBuilder(_ideiaAnexoRepository, _ideiaEntity);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaEntity);

            await _ideiaService.Delete(ideia.Id);

            var ideiaSearch = await _ideiaBuilder.Search(ideia.Id);
            Assert.Null(ideiaSearch);
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var ideiaSearch = (await _ideiaService.GetAll()).ToList();
            Assert.NotNull(ideiaSearch);
            Assert.Equal(4, ideiaSearch.Count);
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var ideiaSearch = await _ideiaService.Get(ideia.Id);
            Assert.NotNull(ideiaSearch);
            Assert.Equal(ideia.Id.ToString(), ideiaSearch.Id);
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            var ideiaTest = await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var ideiaList = await _ideiaService.GetPaged(1, 10, null, null, null, null, null, null, null, null);
            Assert.NotNull(ideiaList);
            Assert.Equal(4, ideiaList.RowCount);

            var ideiaListOne = await _ideiaService.GetPaged(1, 10, ideiaTest.DesIdeia, ideiaTest.DesMotivoInvestir, ideiaTest.IndInteresseCompartilhar, ideiaTest.IndNivelDesenvolvimento, null, null, null, null);
            Assert.NotNull(ideiaListOne);
            Assert.Equal(1, ideiaListOne.RowCount);
        }

        [Fact]
        public async Task TestGetPagedByUserAsync()
        {
            var ideiaTest = await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var ideiaList = await _ideiaService.GetPagedByUser(1, 10, _userId, null, null, null, null, null, null, null, null);
            Assert.NotNull(ideiaList);
            Assert.Equal(4, ideiaList.RowCount);

            var ideiaListOne = await _ideiaService.GetPagedByUser(1, 10, _userId, ideiaTest.DesIdeia, ideiaTest.DesMotivoInvestir, ideiaTest.IndInteresseCompartilhar, ideiaTest.IndNivelDesenvolvimento, null, null, null, null);
            Assert.NotNull(ideiaListOne);
            Assert.Equal(1, ideiaListOne.RowCount);
        }

        [Fact]
        public async Task TestGetPagedInitialScreenAsync()
        {
            var ideiaTest = await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var ideiaList = await _ideiaService.GetPagedInitialScreen(1, 10);
            Assert.NotNull(ideiaList);
            Assert.Equal(4, ideiaList.RowCount);
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var ideiaDto = new IdeiaPostDto();
            Reflection.CopyProperties(_ideiaEntity, ideiaDto);

            List<IdeiaAnexoPostDto> listAnexos = new List<IdeiaAnexoPostDto>();
            for (int i = 0; i < 4; i++)
            {
                var anexoDto = new IdeiaAnexoPostDto();
                Reflection.CopyProperties(_ideiaAnexoBuilder.InstanciarObjeto(), anexoDto);
                listAnexos.Add(anexoDto);
            }
            ideiaDto.Anexos = listAnexos;

            var ideiaPresenter = await _ideiaService.Post(ideiaDto);

            var ideiaSearch = await _ideiaBuilder.Search(Guid.Parse(ideiaPresenter.Id));
            Assert.NotNull(ideiaSearch);
            Assert.Equal(_ideiaEntity.DesIdeia, ideiaSearch.DesIdeia);

            var ideiaListOne = await _ideiaService.GetPaged(1, 10, _ideiaEntity.DesIdeia, _ideiaEntity.DesMotivoInvestir, _ideiaEntity.IndInteresseCompartilhar, _ideiaEntity.IndNivelDesenvolvimento, null, null, null, null);
            Assert.NotNull(ideiaListOne);
            Assert.Equal(1, ideiaListOne.RowCount);
            Assert.Equal(4, ideiaListOne.Results[0].Anexos.Count());
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaEntity);
            var ideiaAnexoBuilder = new IdeiaAnexoBuilder(_ideiaAnexoRepository, ideia);
            await ideiaAnexoBuilder.CreateInDataBase(ideiaAnexoBuilder.InstanciarObjeto());

            var ideiaDto = new IdeiaPutDto();
            Reflection.CopyProperties(ideia, ideiaDto);
            ideiaDto.Id = ideia.Id.ToString();
            ideiaDto.DesIdeia = _faker.Lorem.Paragraph();

            List<IdeiaAnexoPostDto> listAnexos = new List<IdeiaAnexoPostDto>();
            for (int i = 0; i < 4; i++)
            {
                var anexoDto = new IdeiaAnexoPostDto();
                Reflection.CopyProperties(_ideiaAnexoBuilder.InstanciarObjeto(), anexoDto);
                listAnexos.Add(anexoDto);
            }
            ideiaDto.Anexos = listAnexos;

            await _ideiaService.Put(ideiaDto);

            var ideiaSearch = await _ideiaBuilder.Search(ideia.Id);
            Assert.NotNull(ideiaSearch);
            Assert.Equal(ideiaDto.DesIdeia, ideiaSearch.DesIdeia);

            var ideiaListOne = await _ideiaService.GetPaged(1, 10, _ideiaEntity.DesIdeia, _ideiaEntity.DesMotivoInvestir, _ideiaEntity.IndInteresseCompartilhar, _ideiaEntity.IndNivelDesenvolvimento, null, null, null, null);
            Assert.NotNull(ideiaListOne);
            Assert.Equal(1, ideiaListOne.RowCount);
            Assert.Equal(4, ideiaListOne.Results[0].Anexos.Count());
        }

        [Fact]
        public async Task TestPutAvalacaoAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaEntity);
            var ideiaAnexoBuilder = new IdeiaAnexoBuilder(_ideiaAnexoRepository, ideia);
            await ideiaAnexoBuilder.CreateInDataBase(ideiaAnexoBuilder.InstanciarObjeto());

            var ideiaDto = new IdeiaAvaliacaoPutDto();
            Reflection.CopyProperties(ideia, ideiaDto);
            ideiaDto.Id = ideia.Id.ToString();
            ideiaDto.DesIdeia = _faker.Lorem.Paragraph();

            List<IdeiaAnexoPostDto> listAnexos = new List<IdeiaAnexoPostDto>();
            for (int i = 0; i < 4; i++)
            {
                var anexoDto = new IdeiaAnexoPostDto();
                Reflection.CopyProperties(_ideiaAnexoBuilder.InstanciarObjeto(), anexoDto);
                listAnexos.Add(anexoDto);
            }
            ideiaDto.Anexos = listAnexos;

            await _ideiaService.PutAvaliacao(ideiaDto);

            var ideiaSearch = await _ideiaBuilder.Search(ideia.Id);
            Assert.NotNull(ideiaSearch);
            Assert.Equal(ideiaDto.DesIdeia, ideiaSearch.DesIdeia);

            var ideiaListOne = await _ideiaService.GetPaged(1, 10, _ideiaEntity.DesIdeia, _ideiaEntity.DesMotivoInvestir, _ideiaEntity.IndInteresseCompartilhar, _ideiaEntity.IndNivelDesenvolvimento, null, null, null, null);
            Assert.NotNull(ideiaListOne);
            Assert.Equal(1, ideiaListOne.RowCount);
            Assert.Equal(4, ideiaListOne.Results[0].Anexos.Count());
        }
    }
}
