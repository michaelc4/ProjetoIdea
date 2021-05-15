using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Api.Domain.Security;
using Api.Service.Util;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUsuarioRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private readonly IMapper _mapper;

        public LoginService(IUsuarioRepository repository,
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations,
                            IMapper mapper)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _mapper = mapper;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            bool statusSocialOk = false, statusLocalOk = false;
            if (!string.IsNullOrWhiteSpace(user.Provider) && user.Provider.ToUpper().Equals("GOOGLE"))
            {
                statusSocialOk = await ValidateGoogleToken(user.Email, user.IdToken);
            }
            else if (!string.IsNullOrWhiteSpace(user.Provider) && user.Provider.ToUpper().Equals("FACEBOOK"))
            {
                statusSocialOk = await ValidateFacebookToken(user.Email, user.AuthToken);
            }
            else
            {
                var userEntity = await _repository.FindByLogin(user.Email);
                if (userEntity != null && userEntity.Local == 1 && !string.IsNullOrEmpty(user.Senha) && BCrypt.Net.BCrypt.Verify(user.Senha, userEntity.DesSenha))
                {
                    statusLocalOk = true;
                }
            }

            if (statusSocialOk)
            {
                await CreateUser(user);
            }

            if (!string.IsNullOrWhiteSpace(user.Email) && (statusSocialOk || statusLocalOk))
            {
                var userEntity = await _repository.FindByLogin(user.Email);
                if (userEntity != null && ((statusSocialOk && userEntity.Local == 0) || (statusLocalOk && userEntity.Local == 1)))
                {
                    ClaimsIdentity identity = null;
                    if (userEntity.Admin == 1)
                    {
                        identity = new ClaimsIdentity(
                            new GenericIdentity(user.Email),
                            new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                                new Claim(ClaimTypes.Role, "Admin")
                            }
                        );
                    }
                    else
                    {
                        identity = new ClaimsIdentity(
                            new GenericIdentity(user.Email),
                            new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                            }
                        );
                    }

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();

                    return new
                    {
                        authenticated = true,
                        message = "Usuário autenticado",
                        accessToken = CreateToken(identity, createDate, expirationDate, handler),
                        id = userEntity.Id,
                        email = userEntity.DesEmail,
                        imagem = userEntity.DesImagem,
                        admin = userEntity.Admin == 1,
                        created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                }
            }

            return new
            {
                authenticated = false,
                message = "Falha na autenticação! Verifique o usuário e a senha."
            };
        }

        public async Task<object> CreateUser(UsuarioPostDto user)
        {
            var userEntity = await _repository.FindByLogin(user.DesEmail);
            if (userEntity == null)
            {
                var usuarioEntity = _mapper.Map<UsuarioEntity>(user);

                const int WorkFactor = 14;
                usuarioEntity.DesSenha = BCrypt.Net.BCrypt.HashPassword(usuarioEntity.DesSenha, WorkFactor);
                usuarioEntity.Admin = 0;
                usuarioEntity.Local = 1;
                if (!string.IsNullOrEmpty(usuarioEntity.DesImagem))
                {
                    usuarioEntity.DesImagem = ConvertUrlToBase64.ImageResize(usuarioEntity.DesImagem);
                }
                else
                {
                    usuarioEntity.DesImagem = null;
                }

                await _repository.InsertAsync(usuarioEntity);

                return new
                {
                    created = true,
                    message = "Usuário criado com sucesso."
                };
            }

            return new
            {
                created = false,
                message = "Falha ao criar usuário."
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }

        private async Task<bool> ValidateGoogleToken(string email, string token)
        {
            Payload payload = await ValidateAsync(token);
            return payload != null && payload.Email.Trim().Equals(email.Trim());
        }

        private async Task<bool> ValidateFacebookToken(string email, string token)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://graph.facebook.com/v2.9/") };
            var response = await httpClient.GetAsync($"me?access_token={token}&fields=id,name,email,first_name,last_name,age_range,birthday,gender,locale,picture");
            if (!response.IsSuccessStatusCode) return false;
            var result = await response.Content.ReadAsStringAsync();
            var facebookAccount = JsonConvert.DeserializeObject<FacebookAccountPresenter>(result);
            return facebookAccount != null && facebookAccount.email.Trim().Equals(email.Trim());
        }

        private async Task CreateUser(LoginDto user)
        {
            var userEntity = await _repository.FindByLogin(user.Email);
            if (userEntity == null)
            {
                var usuarioEntity = new UsuarioEntity();
                usuarioEntity.DesNome = user.Name;
                usuarioEntity.DesImagem = string.IsNullOrEmpty(user.PhotoUrl) ? null : ConvertUrlToBase64.ConvertToBase64(user.PhotoUrl);
                usuarioEntity.DesEmail = user.Email;
                usuarioEntity.DesSenha = null;
                usuarioEntity.DesTelefone = null;
                usuarioEntity.DesEspecialidade = null;
                usuarioEntity.DesExperiencia = null;
                usuarioEntity.Admin = 0;
                usuarioEntity.Local = 0;

                await _repository.InsertAsync(usuarioEntity);
            }
        }
    }
}
