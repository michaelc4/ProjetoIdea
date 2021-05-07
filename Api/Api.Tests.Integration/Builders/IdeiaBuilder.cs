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
    public class IdeiaBuilder
    {
        private Faker _faker = new Faker("pt_BR");
        private IIdeiaRepository _ideiaRepository;
        private UsuarioEntity _user;

        public IdeiaBuilder(IIdeiaRepository ideiaRepository, UsuarioEntity user)
        {
            _ideiaRepository = ideiaRepository;
            _user = user;
        }

        public IdeiaEntity InstanciarObjeto() => CreateIdea();

        private IdeiaEntity CreateIdea() => new IdeiaEntity
        {
            DesIdeia = _faker.Lorem.Text(),
            DesMotivoInvestir = _faker.Lorem.Text(),
            IndInteresseCompartilhar = "1",
            IndNivelDesenvolvimento = "1",
            IndNivelSigilo = "1",
            DesComentario = _faker.Lorem.Text(),
            NumPotencialDisrupcao = _faker.Random.Int(),
            NumPessoasImpactadas = _faker.Random.Int(),
            NumPotencialGanho = _faker.Random.Int(),
            NumValorInvestimento = _faker.Random.Int(),
            NumImpactoAmbiental = _faker.Random.Int(),
            NumPontuacaoGeral = _faker.Random.Int(),
            IndAtivo = "1",
            IndAprovado = "1",
            UsuarioId = _user.Id
        };

        public async Task<IdeiaEntity> CreateInDataBase(IdeiaEntity idea)
        {
            await _ideiaRepository.InsertAsync(idea);
            return await _ideiaRepository.SelectAsync(idea.Id);
        }

        public async Task<IdeiaEntity> Search(Guid id)
        {
            return await _ideiaRepository.SelectAsync(id);
        }

        public async Task DeleteInDatabase(IdeiaEntity idea)
        {
            await _ideiaRepository.DeleteAsync(idea.Id);
        }
    }
}
