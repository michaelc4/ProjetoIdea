using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Api.Domain.Presenters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Aplication.Controllers
{
    [Route("api/like")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private ILikeService<LikeEntity, LikePresenter, LikePostDto, LikePutDto> _service;

        public LikeController(ILikeService<LikeEntity, LikePresenter, LikePostDto, LikePutDto> service)
        {
            _service = service;
        }

        [HttpPost("post")]
        public async Task<ActionResult> Post([FromBody] LikePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.Post(dto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
