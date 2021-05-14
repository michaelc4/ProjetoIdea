using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Service.Util;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Tests.Integration.Builders
{
    public class VoluntarioBuilder
    {
        private Faker _faker = new Faker("pt_BR");
        private IVoluntarioRepository _voluntarioRepository;
        private UsuarioEntity _user;
        private IdeiaEntity _idea;
        private ProblemaEntity _problem;

        public VoluntarioBuilder(IVoluntarioRepository voluntarioRepository, UsuarioEntity user, IdeiaEntity idea, ProblemaEntity problem)
        {
            _voluntarioRepository = voluntarioRepository;
            _user = user;
            _idea = idea;
            _problem = problem;
        }

        public VoluntarioEntity InstanciarObjeto() => CreateVoluntary();

        private VoluntarioEntity CreateVoluntary() => new VoluntarioEntity
        {
            Id = Guid.Empty,
            UsuarioId = _user.Id,
            IdeiaId = _idea != null ? _idea.Id : null,
            ProblemaId = _problem != null ? _problem.Id : null
        };

        public async Task<VoluntarioEntity> CreateInDataBase(VoluntarioEntity voluntary)
        {
            await _voluntarioRepository.InsertAsync(voluntary);
            return await _voluntarioRepository.SelectAsync(voluntary.Id);
        }

        public async Task<VoluntarioEntity> Search(Guid id)
        {
            return await _voluntarioRepository.SelectAsync(id);
        }

        public async Task DeleteInDatabase(VoluntarioEntity voluntary)
        {
            await _voluntarioRepository.DeleteAsync(voluntary.Id);
        }
    }
}
