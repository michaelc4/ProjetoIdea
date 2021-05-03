using Api.Domain.Entities;
using Api.Domain.Presenters;
using System;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface ILikeService<T, TPresenter, TPostDto, TPutDto> : IBaseService<T, TPresenter, TPostDto, TPutDto> where T : BaseEntity
    {

    }
}
