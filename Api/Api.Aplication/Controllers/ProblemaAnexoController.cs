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
    [Route("api/problemaanexo")]
    [ApiController]
    public class ProblemaAnexoController : BaseController<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto>
    {
        private IProblemaAnexoService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto> _service;

        public ProblemaAnexoController(IProblemaAnexoService<ProblemaAnexoEntity, ProblemaAnexoPresenter, ProblemaAnexoPostDto, ProblemaAnexoPutDto> service) : base(service)
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
    }
}
