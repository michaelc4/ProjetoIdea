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
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Controller
{
    public class ProblemaTest : BaseFixture
    {
        protected IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto> _problemaService;
        protected IProblemaAnexoRepository _problemaAnexoRepository;
        protected IUsuarioRepository _userRepository;
        protected ProblemaBuilder _problemaBuilder;
        protected ProblemaEntity _problemaEntity;
        protected ProblemaAnexoBuilder _problemaAnexoBuilder;
        protected Guid _userId;

        public ProblemaTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _problemaService = _testServer.Services.GetService<IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto>>();

            _userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(_userRepository);
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

            var request = new
            {
                Url = $"/api/problema/delete?id={problema.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, null);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/problema/getall"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var problema = await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/problema/get?id={problema.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            var problemaTest = await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/problema/getallpaged?page=1&pageSize=10"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedByUserAsync()
        {
            var problemaTest = await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/problema/getallpagedbyuser?page=1&pageSize=10&userId={_userId}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedInitialScreenAsync()
        {
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());
            await _problemaBuilder.CreateInDataBase(_problemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/problema/getallpagedinitialscreen?page=1&pageSize=10"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
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

            var request = new
            {
                Url = "/api/problema/post",
                Body = problemaDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
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

            var request = new
            {
                Url = "/api/problema/put",
                Body = problemaDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
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

            var request = new
            {
                Url = "/api/problema/putavaliacao",
                Body = problemaDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }
    }
}
