using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.Tests.Integration
{
    public class TestFixture<TStartup> : IDisposable
    {
        public string _connectionString;
        public Checkpoint _checkpoint;

        public static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;
            var applicationBasePath = AppContext.BaseDirectory;
            var directoryInfo = new DirectoryInfo(applicationBasePath);

            do
            {
                directoryInfo = directoryInfo.Parent;
                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
                if (projectDirectoryInfo.Exists)
                    if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }

        public TestServer Server { get; }

        public TestFixture() : this(Path.Combine(""))
        {
            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory"
                },
                SchemasToExclude = new[]
                {
                    "dbapiinova"
                },
                DbAdapter = DbAdapter.MySql,

            };
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var manager = new ApplicationPartManager
            {
                ApplicationParts =
                {
                    new AssemblyPart(startupAssembly)
                }
            };

            services.AddSingleton(manager);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToEntityProfile());
                cfg.AddProfile(new EntityToPresenterProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            _connectionString = "server=aa1tb9hblhskc0c.czdgknerwits.us-east-2.rds.amazonaws.com;port=3306;database=dbapiinovateste;uid=dbapiinova;password=rootinova123";

            ConfigureRepository.ConfigureDependenciesRepository(services, null, _connectionString);
            ConfigureService.ConfigureDependenciesService(services);
        }

        protected TestFixture(string relativeTargetProjectParentDir)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath(relativeTargetProjectParentDir, startupAssembly);

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.json");

            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Development")
                .UseStartup(typeof(TStartup));

            // Create instance of test server
            Server = new TestServer(webHostBuilder);

            // Add configuration for client
            Client = Server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5001");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
