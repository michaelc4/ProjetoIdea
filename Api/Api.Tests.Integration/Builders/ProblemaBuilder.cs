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
    public class ProblemaBuilder
    {
        private Faker _faker = new Faker("pt_BR");
        private IProblemaRepository _problemaRepository;
        private UsuarioEntity _user;

        public ProblemaBuilder(IProblemaRepository problemaRepository, UsuarioEntity user)
        {
            _problemaRepository = problemaRepository;
            _user = user;
        }

        public ProblemaEntity InstanciarObjeto() => CreateProblem();

        private ProblemaEntity CreateProblem() => new ProblemaEntity
        {
            DesProblema = _faker.Lorem.Text(),
            IndTipoBeneficio = "1",
            IndTipoSolucao = "1",
            NumProbabilidadeInvestir = _faker.Random.Int(),
            IndAtivo = "1",
            IndAprovado = "1",
            UsuarioId = _user.Id
        };

        public async Task<ProblemaEntity> CreateInDataBase(ProblemaEntity problem)
        {
            await _problemaRepository.InsertAsync(problem);
            return await _problemaRepository.SelectAsync(problem.Id);
        }

        public async Task<ProblemaEntity> Search(Guid id)
        {
            return await _problemaRepository.SelectAsync(id);
        }

        public async Task DeleteInDatabase(ProblemaEntity problem)
        {
            await _problemaRepository.DeleteAsync(problem.Id);
        }
    }
}
