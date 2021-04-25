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
    [Route("api/voluntario")]
    [ApiController]
    public class VoluntarioController : BaseController<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto>
    {
        private IVoluntarioService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto> _service;

        public VoluntarioController(IVoluntarioService<VoluntarioEntity, VoluntarioPresenter, VoluntarioPostDto, VoluntarioPutDto> service) : base(service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet("getallpaged")]
        public async Task<ActionResult> GetPaged(int page, int pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPaged(page, pageSize));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet("getallpagedbyuser")]
        public async Task<ActionResult> GetPagedByUser(int page, int pageSize, Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPagedByUser(page, pageSize, userId));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet("getallpagedbyproblemorideia")]
        public async Task<ActionResult> GetPagedByProblemOrIdeia(int page, int pageSize, Guid? problemId, Guid? ideaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPagedByProblemOrIdeia(page, pageSize, problemId, ideaId));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
