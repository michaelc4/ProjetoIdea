using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Tests.Integration.Builders;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Repository
{
    public class UserIntegrationTest : BaseFixture
    {
        protected IUsuarioRepository _userRepository;
        protected UsuarioBuilder _usuarioBuilder;
        protected UsuarioEntity _usuarioEntity;

        public UserIntegrationTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            _usuarioBuilder = new UsuarioBuilder(_userRepository);
            _usuarioEntity = _usuarioBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioEntity);

            await _userRepository.DeleteAsync(user.Id);

            var userSearch = await _usuarioBuilder.Search(user.Id);
            Assert.Null(userSearch);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestExistsAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioEntity);
            var result = await _userRepository.ExistsAsync(user.Id);

            Assert.True(result);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestFindByLoginAsync()
        {
            await _usuarioBuilder.CreateInDataBase(_usuarioEntity);

            var userSearch = await _userRepository.FindByLogin(_usuarioEntity.DesEmail);
            Assert.NotNull(userSearch);
            Assert.Equal(_usuarioEntity.DesEmail, userSearch.DesEmail);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var userList = await _userRepository.GetPaged(_userRepository.GetQuery(), 1, 10);
            Assert.NotNull(userList);
            Assert.Equal(4, userList.RowCount);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestInsertAsync()
        {
            await _userRepository.InsertAsync(_usuarioEntity);

            var userSearch = await _usuarioBuilder.Search(_usuarioEntity.Id);
            Assert.NotNull(userSearch);
            Assert.Equal(_usuarioEntity.DesEmail, userSearch.DesEmail);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestSelectOneAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var userSearch = await _userRepository.SelectAsync(user.Id);
            Assert.NotNull(userSearch);
            Assert.Equal(user.Id, userSearch.Id);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestSelectManyAsync()
        {
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());
            await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var userSearch = (await _userRepository.SelectAsync()).ToList();
            Assert.NotNull(userSearch);
            Assert.Equal(4, userSearch.Count);

            await ResetDatabase();
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            var user = await _usuarioBuilder.CreateInDataBase(_usuarioEntity);
            user.DesSenha = _faker.Internet.Email();

            await _userRepository.UpdateAsync(user);

            var userSearch = await _usuarioBuilder.Search(_usuarioEntity.Id);
            Assert.NotNull(userSearch);
            Assert.Equal(user.DesEmail, userSearch.DesEmail);

            await ResetDatabase();
        }
    }
}
