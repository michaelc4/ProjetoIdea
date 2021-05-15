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
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : BaseController<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto>
    {
        private IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto> _service;

        public UsuarioController(IUsuarioService<UsuarioEntity, UsuarioPresenter, UsuarioPostDto, UsuarioPutDto> service) : base(service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet("get")]
        public override async Task<ActionResult> Get(Guid id)
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
        [HttpGet("getallpaged")]
        public async Task<ActionResult> GetPaged(int page, int pageSize, string nameSearch, string emailSearch, string foneSearch, bool? isAdminSearch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetPaged(page, pageSize, nameSearch, emailSearch, foneSearch, isAdminSearch));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut("put")]
        public override async Task<ActionResult> Put([FromBody] UsuarioPutDto dto)
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
    }
}
