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
    [Route("api/ideia")]
    [ApiController]
    public class IdeiaController : BaseController<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto>
    {
        private IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto> _service;

        public IdeiaController(IIdeiaService<IdeiaEntity, IdeiaPresenter, IdeiaPostDto, IdeiaPutDto> service) : base(service)
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
    }
}
