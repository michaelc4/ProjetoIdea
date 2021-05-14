using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Util;
using Api.Tests.Integration.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Controller
{
    public class UsuarioTest : BaseFixture
    {
        protected IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto> _userService;
        protected IUsuarioRepository _userRepository;
        protected UsuarioBuilder _usuarioBuilder;
        protected UsuarioEntity _usuarioEntity;

        public UsuarioTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _userService = _testServer.Services.GetService<IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto>>();

            _userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            _usuarioBuilder = new UsuarioBuilder(_userRepository);
            _usuarioEntity = _usuarioBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioEntity);

            var request = new
            {
                Url = $"/api/usuario/delete?id={user.Id}"
            };

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, null);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/usuario/getall"
            };

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/usuario/get?id={user.Id}"
            };

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/usuario/getallpaged?page=1&pageSize=10"
            };

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var userDto = new UsuarioPostDto();
            Reflection.CopyProperties(_usuarioEntity, userDto);

            var request = new
            {
                Url = "/api/usuario/post",
                Body = userDto
            };

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioEntity);

            var userDto = new UsuarioPutDto();
            Reflection.CopyProperties(_usuarioEntity, userDto);
            userDto.Id = _usuarioEntity.Id.ToString();
            userDto.DesSenha = _faker.Internet.Password();

            var request = new
            {
                Url = "/api/usuario/put",
                Body = userDto
            };

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }
    }
}
