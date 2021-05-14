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
    [Route("api/problema")]
    [ApiController]
    public class ProblemaController : BaseController<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto>
    {
        private IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto> _service;

        public ProblemaController(IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto> service) : base(service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet("getallpaged")]
        public async Task<ActionResult> GetPaged(int page, int pageSize, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPaged(page, pageSize, problemSearch, benefitTypeSearch, solutionTypeSearch, approvedSearch, registrationDateIniSearch, registrationDateEndSearch));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet("getallpagedbyuser")]
        public async Task<ActionResult> GetPagedByUser(int page, int pageSize, Guid userId, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string approvedSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPagedByUser(page, pageSize, userId, problemSearch, benefitTypeSearch, solutionTypeSearch, approvedSearch, registrationDateIniSearch, registrationDateEndSearch));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
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
