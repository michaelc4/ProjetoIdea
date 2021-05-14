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
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Controller
{
    public class LoginTest : BaseFixture
    {
        private HttpClient _client;
        protected UsuarioBuilder _usuarioBuilder;
        protected UsuarioEntity _usuarioEntity;

        public LoginTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _client = fixture.Client;

            var userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            _usuarioBuilder = new UsuarioBuilder(userRepository);
            _usuarioEntity = _usuarioBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestLoginAsync()
        {
            var userEntity = _usuarioBuilder.InstanciarObjeto();
            string senha = userEntity.DesSenha;
            const int WorkFactor = 14;
            userEntity.DesSenha = BCrypt.Net.BCrypt.HashPassword(userEntity.DesSenha, WorkFactor);

            var user = await _usuarioBuilder.CreateInDataBase(userEntity);

            var request = new
            {
                Url = "/api/login",
                Body = new
                {
                    Email = user.DesEmail,
                    Senha = senha,
                    Provider = "LOCAL"
                }
            };

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();

            var contents = await response.Content.ReadAsStringAsync();
            var obj = ContentHelper.GetObject(contents);
            Assert.NotNull(obj);
        }

        [Fact]
        public async Task TestCreateUserAsync()
        {
            var userDto = new UsuarioPostDto();
            Reflection.CopyProperties(_usuarioEntity, userDto);

            var request = new
            {
                Url = "/api/login/createuser",
                Body = userDto
            };

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }
    }
}
