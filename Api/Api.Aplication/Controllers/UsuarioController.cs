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
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : BaseController<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto>
    {
        private IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto> _service;

        public UsuarioController(IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto> service) : base(service)
        {
            _service = service;
        }

        [Authorize("Bearer", Roles = "Admin")]
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
