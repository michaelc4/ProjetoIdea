using Bogus;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration
{
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
    }
}
