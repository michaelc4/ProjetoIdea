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
    public class IdeiaAnexoBuilder
    {
        private Faker _faker = new Faker("pt_BR");
        private IIdeiaAnexoRepository _ideiaAnexoRepository;
        private IdeiaEntity _idea;

        public IdeiaAnexoBuilder(IIdeiaAnexoRepository ideiaAnexoRepository, IdeiaEntity idea)
        {
            _ideiaAnexoRepository = ideiaAnexoRepository;
            _idea = idea;
        }

        public IdeiaAnexoEntity InstanciarObjeto() => CreateAttachment();

        private IdeiaAnexoEntity CreateAttachment() => new IdeiaAnexoEntity
        {
            DesAnexo = Convert.ToBase64String(Encoding.ASCII.GetBytes(_faker.Lorem.Paragraph())),
            IndTipoArquivo = "1",
            DesNomeOriginal = _faker.System.FileName(),
            IdeiaId = _idea.Id
        };

        public async Task<IdeiaAnexoEntity> CreateInDataBase(IdeiaAnexoEntity attach)
        {
            await _ideiaAnexoRepository.InsertAsync(attach);
            return await _ideiaAnexoRepository.SelectAsync(attach.Id);
        }

        public async Task<IdeiaAnexoEntity> Search(Guid id)
        {
            return await _ideiaAnexoRepository.SelectAsync(id);
        }

        public async Task DeleteInDatabase(IdeiaAnexoEntity attach)
        {
            await _ideiaAnexoRepository.DeleteAsync(attach.Id);
        }
    }
}
