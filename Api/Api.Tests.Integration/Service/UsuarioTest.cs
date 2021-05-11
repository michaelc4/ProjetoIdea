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
    public class UsuarioTest : BaseFixture
    {
        protected IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto> _userService;
        protected UsuarioBuilder _usuarioBuilder;
        protected UsuarioEntity _usuarioEntity;

        public UsuarioTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _userService = _testServer.Services.GetService<IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto>>();

            var userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            _usuarioBuilder = new UsuarioBuilder(userRepository);
            _usuarioEntity = _usuarioBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioEntity);

            await _userService.Delete(user.Id);

            var userSearch = await _usuarioBuilder.Search(user.Id);
            Assert.Null(userSearch);
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var userSearch = (await _userService.GetAll()).ToList();
            Assert.NotNull(userSearch);
            Assert.Equal(4, userSearch.Count);
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var userSearch = await _userService.Get(user.Id);
            Assert.NotNull(userSearch);
            Assert.Equal(user.Id.ToString(), userSearch.Id);
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            var userTest = await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var userList = await _userService.GetPaged(1, 10, null, null, null, null);
            Assert.NotNull(userList);
            Assert.Equal(4, userList.RowCount);

            var userListOne = await _userService.GetPaged(1, 10, userTest.DesNome, userTest.DesEmail, userTest.DesTelefone, null);
            Assert.NotNull(userListOne);
            Assert.Equal(1, userListOne.RowCount);
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var userDto = new UsuarioPostDto();
            Reflection.CopyProperties(_usuarioEntity, userDto);

            var userPresenter = await _userService.Post(userDto);

            var userSearch = await _usuarioBuilder.Search(Guid.Parse(userPresenter.Id));
            Assert.NotNull(userSearch);
            Assert.Equal(_usuarioEntity.DesEmail, userSearch.DesEmail);
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioEntity);
            
            var userDto = new UsuarioPutDto();
            Reflection.CopyProperties(_usuarioEntity, userDto);
            userDto.Id = _usuarioEntity.Id.ToString();
            userDto.DesSenha = _faker.Internet.Password();

            await _userService.Put(userDto);

            var userSearch = await _usuarioBuilder.Search(_usuarioEntity.Id);
            Assert.NotNull(userSearch);
            Assert.Equal(user.DesSenha, userSearch.DesSenha);
        }
    }
}
