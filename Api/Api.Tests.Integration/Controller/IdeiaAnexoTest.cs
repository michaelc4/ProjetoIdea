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
    public class IdeiaAnexoTest : BaseFixture
    {
        protected IIdeiaAnexoService<IdeiaAnexoEntity, IdeiaAnexoPresenter, IdeiaAnexoPostDto, IdeiaAnexoPutDto> _ideiaAnexoService;
        protected IUsuarioRepository _userRepository;
        protected IdeiaAnexoBuilder _ideiaAnexoBuilder;
        protected IdeiaAnexoEntity _ideiaAnexoEntity;

        public IdeiaAnexoTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _ideiaAnexoService = _testServer.Services.GetService<IIdeiaAnexoService<IdeiaAnexoEntity, IdeiaAnexoPresenter, IdeiaAnexoPostDto, IdeiaAnexoPutDto>>();

            _userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(_userRepository);
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

            var request = new
            {
                Url = $"/api/ideiaanexo/delete?id={ideiaAnexo.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, null);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideiaanexo/getall"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideiaanexo/get?id={ideiaAnexo.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());
            await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideiaanexo/getallpaged?page=1&pageSize=10"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var ideiaAnexoDto = new IdeiaAnexoPostDto();
            Reflection.CopyProperties(_ideiaAnexoEntity, ideiaAnexoDto);

            var request = new
            {
                Url = "/api/ideiaanexo/post",
                Body = ideiaAnexoDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var ideiaAnexo = await _ideiaAnexoBuilder.CreateInDataBase(_ideiaAnexoEntity);

            var ideiaAnexoDto = new IdeiaAnexoPutDto();
            Reflection.CopyProperties(ideiaAnexo, ideiaAnexoDto);
            ideiaAnexoDto.Id = ideiaAnexo.Id.ToString();
            ideiaAnexoDto.DesNomeOriginal = _faker.System.FileName();

            var request = new
            {
                Url = "/api/ideiaanexo/put",
                Body = ideiaAnexoDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }
    }
}
