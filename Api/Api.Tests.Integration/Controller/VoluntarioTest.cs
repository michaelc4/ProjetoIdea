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
    public class VoluntarioTest : BaseFixture
    {
        protected IVoluntarioService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto> _voluntarioService;
        protected IUsuarioRepository _userRepository;
        protected VoluntarioBuilder _voluntarioIdeiaBuilder;
        protected VoluntarioBuilder _voluntarioProblemaBuilder;
        protected UsuarioBuilder _usuarioBuilder;
        protected VoluntarioEntity _voluntarioIdeiaEntity;
        protected VoluntarioEntity _voluntarioProblemaEntity;
        protected UsuarioEntity _usuarioVoluntario;
        protected IdeiaEntity _ideiaEntity;
        protected ProblemaEntity _problemaEntity;

        public VoluntarioTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _voluntarioService = _testServer.Services.GetService<IVoluntarioService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto>>();

            _userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            _usuarioBuilder = new UsuarioBuilder(_userRepository);
            var usuarioEntity = _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto()).Result;
            _usuarioVoluntario = _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto()).Result;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            var ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            _ideiaEntity = ideiaBuilder.CreateInDataBase(ideiaBuilder.InstanciarObjeto()).Result;

            var problemaRepository = _testServer.Services.GetService<IProblemaRepository>();
            var problemaBuilder = new ProblemaBuilder(problemaRepository, usuarioEntity);
            _problemaEntity = problemaBuilder.CreateInDataBase(problemaBuilder.InstanciarObjeto()).Result;

            var voluntarioRepository = _testServer.Services.GetService<IVoluntarioRepository>();
            _voluntarioIdeiaBuilder = new VoluntarioBuilder(voluntarioRepository, _usuarioVoluntario, _ideiaEntity, null);
            _voluntarioIdeiaEntity = _voluntarioIdeiaBuilder.InstanciarObjeto();
            _voluntarioProblemaBuilder = new VoluntarioBuilder(voluntarioRepository, _usuarioVoluntario, null, _problemaEntity);
            _voluntarioProblemaEntity = _voluntarioProblemaBuilder.InstanciarObjeto();
        }

        [Fact]
        public async Task TestIdeiaDeleteAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaEntity);

            var request = new
            {
                Url = $"/api/voluntario/delete?id={voluntario.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, null);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestIdeiaGetAllAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/getall"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestIdeiaGetAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/get?id={voluntario.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestIdeiaGetPagedAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/getallpaged?page=1&pageSize=10"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestIdeiaGetPagedByUserAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/getallpaged?page=1&pageSize=10&userId={_usuarioVoluntario.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestIdeiaGetPagedByProblemOrIdeiaAsync()
        {
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());
            await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/getallpaged?page=1&pageSize=10&ideaId={_ideiaEntity.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestIdeiaPostAsync()
        {
            var voluntarioDto = new VoluntarioPostDto();
            Reflection.CopyProperties(_voluntarioIdeiaEntity, voluntarioDto);

            var request = new
            {
                Url = "/api/voluntario/post",
                Body = voluntarioDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestIdeiaPutAsync()
        {
            var voluntario = await _voluntarioIdeiaBuilder.CreateInDataBase(_voluntarioIdeiaEntity);
            var usuarioVoluntarioNew = await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var voluntarioDto = new VoluntarioPutDto();
            Reflection.CopyProperties(voluntario, voluntarioDto);
            voluntarioDto.Id = voluntario.Id.ToString();
            voluntarioDto.UsuarioId = usuarioVoluntarioNew.Id;

            var request = new
            {
                Url = "/api/voluntario/put",
                Body = voluntarioDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaDeleteAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaEntity);

            var request = new
            {
                Url = $"/api/voluntario/delete?id={voluntario.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, null);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaGetAllAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/getall"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaGetAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/get?id={voluntario.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaGetPagedAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/getallpaged?page=1&pageSize=10"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaGetPagedByUserAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/getallpaged?page=1&pageSize=10&userId={_usuarioVoluntario.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaGetPagedByProblemOrIdeiaAsync()
        {
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());
            await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/voluntario/getallpaged?page=1&pageSize=10&problemId={_problemaEntity.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaPostAsync()
        {
            var voluntarioDto = new VoluntarioPostDto();
            Reflection.CopyProperties(_voluntarioProblemaEntity, voluntarioDto);

            var request = new
            {
                Url = "/api/voluntario/post",
                Body = voluntarioDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestProblemaPutAsync()
        {
            var voluntario = await _voluntarioProblemaBuilder.CreateInDataBase(_voluntarioProblemaEntity);
            var usuarioVoluntarioNew = await _usuarioBuilder.CreateInDataBase(_usuarioBuilder.InstanciarObjeto());

            var voluntarioDto = new VoluntarioPutDto();
            Reflection.CopyProperties(voluntario, voluntarioDto);
            voluntarioDto.Id = voluntario.Id.ToString();
            voluntarioDto.UsuarioId = usuarioVoluntarioNew.Id;

            var request = new
            {
                Url = "/api/voluntario/put",
                Body = voluntarioDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }
    }
}
