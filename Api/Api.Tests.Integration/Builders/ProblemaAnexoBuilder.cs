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
    public class ProblemaAnexoBuilder
    {
        private Faker _faker = new Faker("pt_BR");
        private IProblemaAnexoRepository _problemaAnexoRepository;
        private ProblemaEntity _problem;

        public ProblemaAnexoBuilder(IProblemaAnexoRepository problemaAnexoRepository, ProblemaEntity problem)
        {
            _problemaAnexoRepository = problemaAnexoRepository;
            _problem = problem;
        }

        public ProblemaAnexoEntity InstanciarObjeto() => CreateAttachment();

        private ProblemaAnexoEntity CreateAttachment() => new ProblemaAnexoEntity
        {
            DesAnexo = Convert.ToBase64String(Encoding.ASCII.GetBytes(_faker.Lorem.Paragraph())),
            IndTipoArquivo = "1",
            DesNomeOriginal = _faker.System.FileName(),
            ProblemaId = _problem.Id
        };

        public async Task<ProblemaAnexoEntity> CreateInDataBase(ProblemaAnexoEntity attach)
        {
            await _problemaAnexoRepository.InsertAsync(attach);
            return await _problemaAnexoRepository.SelectAsync(attach.Id);
        }

        public async Task<ProblemaAnexoEntity> Search(Guid id)
        {
            return await _problemaAnexoRepository.SelectAsync(id);
        }

        public async Task DeleteInDatabase(ProblemaAnexoEntity attach)
        {
            await _problemaAnexoRepository.DeleteAsync(attach.Id);
        }
    }
}
