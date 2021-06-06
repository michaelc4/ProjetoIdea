using Api.Domain.Dtos;
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
    public class ProblemaController : ControllerBase
    {
        private IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto> _service;

        public ProblemaController(IProblemaService<ProblemaEntity, ProblemaPresenter, ProblemaPostDto, ProblemaPutDto> service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpPut("delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet("get")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Get(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer", Roles = "Admin")]
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer", Roles = "Admin")]
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

        [HttpGet("getallpagedinitialscreen")]
        public async Task<ActionResult> GetPagedInitialScreen(int page, int pageSize, Guid? userId, string problemSearch, string benefitTypeSearch, string solutionTypeSearch, string registrationDateIniSearch, string registrationDateEndSearch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPagedInitialScreen(page, pageSize, userId, problemSearch, benefitTypeSearch, solutionTypeSearch, registrationDateIniSearch, registrationDateEndSearch));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost("post")]
        public async Task<ActionResult> Post([FromBody] ProblemaPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Post(dto));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut("put")]
        public async Task<ActionResult> Put([FromBody] ProblemaPutDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Put(dto));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer", Roles = "Admin")]
        [HttpPut("putavaliacao")]
        public async Task<ActionResult> PutAvaliacao([FromBody] ProblemaAvaliacaoPutDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.PutAvaliacao(dto));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
