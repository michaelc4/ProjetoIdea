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
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Service
{
    public class LoginTest : BaseFixture
    {
        protected ILoginService _loginService;
        protected UsuarioBuilder _usuarioBuilder;
        protected UsuarioEntity _usuarioEntity;

        public LoginTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _loginService = _testServer.Services.GetService<ILoginService>();

            var userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            _usuarioBuilder = new UsuarioBuilder(userRepository);
            _usuarioEntity = _usuarioBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestFindByLoginAsync()
        {
            var userEntity = _usuarioBuilder.InstanciarObjeto();
            const int WorkFactor = 14;
            userEntity.DesSenha = BCrypt.Net.BCrypt.HashPassword(userEntity.DesSenha, WorkFactor);

            var user = await _usuarioBuilder.CreateInDataBase(userEntity);

            var loginDto = new LoginDto();
            loginDto.Email = user.DesEmail;
            loginDto.Senha = user.DesSenha;
            loginDto.Provider = "LOCAL";

            var userSearch = await _loginService.FindByLogin(loginDto);
            Assert.NotNull(userSearch);
        }

        [Fact]
        public async Task TestCreateUserAsync()
        {
            var userDto = new UsuarioPostDto();
            Reflection.CopyProperties(_usuarioEntity, userDto);

            var userPresenter = await _loginService.CreateUser(userDto);
            Assert.NotNull(userPresenter);
        }
    }
}
