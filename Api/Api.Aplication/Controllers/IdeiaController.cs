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

        [Authorize("Bearer", Roles = "Admin")]
        [HttpGet("getallpaged")]
        public async Task<ActionResult> GetPaged(int page, int pageSize, string ideaSearch, string reasonSearch, string shareSearch, string developmentSearch, string secretSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPaged(page, pageSize, ideaSearch, reasonSearch, shareSearch, developmentSearch, secretSearch, approvedSearch, registrationDateIniSearch, registrationDateEndSearch));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer", Roles = "Admin")]
        [HttpGet("getallpagedbyuser")]
        public async Task<ActionResult> GetPagedByUser(int page, int pageSize, Guid userId, string ideaSearch, string reasonSearch, string shareSearch, string developmentSearch, string secretSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPagedByUser(page, pageSize, userId, ideaSearch, reasonSearch, shareSearch, developmentSearch, secretSearch, approvedSearch, registrationDateIniSearch, registrationDateEndSearch));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("getallpagedinitialscreen")]
        public async Task<ActionResult> GetPagedInitialScreen(int page, int pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPagedInitialScreen(page, pageSize));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
