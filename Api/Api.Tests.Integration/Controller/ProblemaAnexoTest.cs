using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Util;
using Api.Tests.Integration.Builders;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Controller
{
    public class ProblemaAnexoTest : BaseFixture
    {
        protected IProblemaAnexoService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto> _problemaAnexoService;
        protected IUsuarioRepository _userRepository;
        protected ProblemaAnexoBuilder _problemaAnexoBuilder;
        protected ProblemaAnexoEntity _problemaAnexoEntity;

        public ProblemaAnexoTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _problemaAnexoService = _testServer.Services.GetService<IProblemaAnexoService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto>>();

            _userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(_userRepository);
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

            var request = new
            {
                Url = $"/api/problemaanexo/delete?id={problemaAnexo.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, null);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/problemaanexo/getall"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideiaanexo/get?id={problemaAnexo.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());
            await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/problemaanexo/getallpaged?page=1&pageSize=10"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var problemaAnexoDto = new ProblemaAnexoPostDto();
            Reflection.CopyProperties(_problemaAnexoEntity, problemaAnexoDto);

            var request = new
            {
                Url = "/api/problemaanexo/post",
                Body = problemaAnexoDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var problemaAnexo = await _problemaAnexoBuilder.CreateInDataBase(_problemaAnexoEntity);

            var problemaAnexoDto = new ProblemaAnexoPutDto();
            Reflection.CopyProperties(problemaAnexo, problemaAnexoDto);
            problemaAnexoDto.Id = problemaAnexo.Id.ToString();
            problemaAnexoDto.DesNomeOriginal = _faker.System.FileName();

            var request = new
            {
                Url = "/api/problemaanexo/put",
                Body = problemaAnexoDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }
    }
}
