using Api.Domain.Interfaces.Repository;
using Api.Service.Util;
using Api.Tests.Integration.Builders;
using Bogus;
using Microsoft.AspNetCore.TestHost;
using MySqlConnector;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration
{
    [Collection("Non-Parallel Collection")]
    public abstract class BaseFixture : IClassFixture<TestFixture<Startup>>, IDisposable
    {
        private TestFixture<Startup> _fixture;
        public HttpClient _client;
        public TestServer _testServer;
        public Faker _faker = new Faker("pt_BR");

        public BaseFixture(TestFixture<Startup> fixture)
        {
            _fixture = fixture;
            _client = fixture.Client;
            _testServer = fixture.Server;
        }

        public async Task ResetDatabase()
        {
            using (var conn = new MySqlConnection(_fixture._connectionString))
            {
                await conn.OpenAsync();
                await _fixture._checkpoint.Reset(conn);
            }
        }

        public void Dispose()
        {
            ResetDatabase().Wait();
        }

        public async Task<string> GetToken(IUsuarioRepository userRepository)
        {
            var usuarioBuilder = new UsuarioBuilder(userRepository);
            var userEntity = usuarioBuilder.InstanciarObjeto();

            string senha = userEntity.DesSenha;
            const int WorkFactor = 14;
            userEntity.DesSenha = BCrypt.Net.BCrypt.HashPassword(userEntity.DesSenha, WorkFactor);

            var user = await usuarioBuilder.CreateInDataBase(userEntity);

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
            var contents = await response.Content.ReadAsStringAsync();
            var obj = ContentHelper.GetObject(contents);

            return obj.accessToken;
        }
    }
}
