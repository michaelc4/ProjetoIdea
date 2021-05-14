using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Tests.Integration.Builders
{
    public class UsuarioBuilder
    {
        private Faker _faker = new Faker("pt_BR");
        private IUsuarioRepository _userRepository;

        public UsuarioBuilder(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UsuarioEntity InstanciarObjeto() => CreateUser();

        private UsuarioEntity CreateUser() => new UsuarioEntity
        {
            DesNome = _faker.Lorem.Sentence(),
            DesEmail = _faker.Internet.Email(),
            DesTelefone = _faker.Phone.PhoneNumber(),
            DesEspecialidade = _faker.Lorem.Sentence(),
            DesExperiencia = _faker.Lorem.Sentence(),
            DesSenha = _faker.Internet.Password(),
            Admin = 1,
            Local = 1
        };

        public async Task<UsuarioEntity> CreateInDataBase(UsuarioEntity user)
        {
            await _userRepository.InsertAsync(user);
            return await _userRepository.SelectAsync(user.Id);
        }

        public async Task<UsuarioEntity> Search(Guid id)
        {
            return await _userRepository.SelectAsync(id);
        }

        public async Task DeleteInDatabase(UsuarioEntity user)
        {
            await _userRepository.DeleteAsync(user.Id);
        }
    }
}
