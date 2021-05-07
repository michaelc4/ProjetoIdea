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
    public class LikeBuilder
    {
        private Faker _faker = new Faker("pt_BR");
        private ILikeRepository _likeRepository;
        private IdeiaEntity _idea;
        private ProblemaEntity _problem;

        public LikeBuilder(ILikeRepository likeRepository, IdeiaEntity idea, ProblemaEntity problem)
        {
            _likeRepository = likeRepository;
            _idea = idea;
            _problem = problem;
        }

        public LikeEntity InstanciarObjeto() => CreateLike();

        private LikeEntity CreateLike() => new LikeEntity
        {
            IpUsr = _faker.Internet.Ip(),
            IdeiaId = _idea != null ? _idea.Id : null,
            ProblemaId = _problem != null ? _problem.Id : null
        };

        public async Task<LikeEntity> CreateInDataBase(LikeEntity like)
        {
            await _likeRepository.InsertAsync(like);
            return await _likeRepository.SelectAsync(like.Id);
        }

        public async Task<LikeEntity> Search(Guid id)
        {
            return await _likeRepository.SelectAsync(id);
        }

        public async Task DeleteInDatabase(LikeEntity like)
        {
            await _likeRepository.DeleteAsync(like.Id);
        }
    }
}
