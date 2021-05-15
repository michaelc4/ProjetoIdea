using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Service.Util;
using Api.Tests.Integration.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration.Controller
{
    public class IdeiaTest : BaseFixture
    {
        protected IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto> _ideiaService;
        protected IIdeiaAnexoRepository _ideiaAnexoRepository;
        protected IUsuarioRepository _userRepository;
        protected IdeiaBuilder _ideiaBuilder;
        protected IdeiaEntity _ideiaEntity;
        protected IdeiaAnexoBuilder _ideiaAnexoBuilder;
        protected Guid _userId;

        public IdeiaTest(TestFixture<Startup> fixture) : base(fixture)
        {
            _ideiaService = _testServer.Services.GetService<IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto>>();

            _userRepository = _testServer.Services.GetService<IUsuarioRepository>();
            var usuarioBuilder = new UsuarioBuilder(_userRepository);
            var usuarioEntity = usuarioBuilder.CreateInDataBase(usuarioBuilder.InstanciarObjeto()).Result;

            _userId = usuarioEntity.Id;

            var ideiaRepository = _testServer.Services.GetService<IIdeiaRepository>();
            _ideiaBuilder = new IdeiaBuilder(ideiaRepository, usuarioEntity);
            _ideiaEntity = _ideiaBuilder.InstanciarObjeto();

            _ideiaAnexoRepository = _testServer.Services.GetService<IIdeiaAnexoRepository>();
            _ideiaAnexoBuilder = new IdeiaAnexoBuilder(_ideiaAnexoRepository, _ideiaEntity);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaEntity);

            var request = new
            {
                Url = $"/api/ideia/delete?id={ideia.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, null);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideia/getall"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideia/get?id={ideia.Id}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedAsync()
        {
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideia/getallpaged?page=1&pageSize=10"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedByUserAsync()
        {
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideia/getallpagedbyuser?page=1&pageSize=10&userId={_userId}"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetPagedInitialScreenAsync()
        {
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());
            await _ideiaBuilder.CreateInDataBase(_ideiaBuilder.InstanciarObjeto());

            var request = new
            {
                Url = $"/api/ideia/getallpagedinitialscreen?page=1&pageSize=10"
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.GetAsync(request.Url);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var ideiaDto = new IdeiaPostDto();
            Reflection.CopyProperties(_ideiaEntity, ideiaDto);

            List<IdeiaAnexoPostDto> listAnexos = new List<IdeiaAnexoPostDto>();
            for (int i = 0; i < 4; i++)
            {
                var anexoDto = new IdeiaAnexoPostDto();
                Reflection.CopyProperties(_ideiaAnexoBuilder.InstanciarObjeto(), anexoDto);
                listAnexos.Add(anexoDto);
            }
            ideiaDto.Anexos = listAnexos;

            var request = new
            {
                Url = "/api/ideia/post",
                Body = ideiaDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var ideia = await _ideiaBuilder.CreateInDataBase(_ideiaEntity);
            var ideiaAnexoBuilder = new IdeiaAnexoBuilder(_ideiaAnexoRepository, ideia);
            await ideiaAnexoBuilder.CreateInDataBase(ideiaAnexoBuilder.InstanciarObjeto());

            var ideiaDto = new IdeiaPutDto();
            Reflection.CopyProperties(ideia, ideiaDto);
            ideiaDto.Id = ideia.Id.ToString();
            ideiaDto.DesIdeia = _faker.Lorem.Paragraph();

            List<IdeiaAnexoPostDto> listAnexos = new List<IdeiaAnexoPostDto>();
            for (int i = 0; i < 4; i++)
            {
                var anexoDto = new IdeiaAnexoPostDto();
                Reflection.CopyProperties(_ideiaAnexoBuilder.InstanciarObjeto(), anexoDto);
                listAnexos.Add(anexoDto);
            }
            ideiaDto.Anexos = listAnexos;

            var request = new
            {
                Url = "/api/ideia/put",
                Body = ideiaDto
            };

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken(_userRepository));

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }
    }
}
