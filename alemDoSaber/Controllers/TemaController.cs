﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Model;
using RedeSocial.Service;

namespace RedeSocial.Controllers
{

    [Route("~/temas")]
    [ApiController]
    public class TemaController : ControllerBase
    {

        private readonly ITemaService _temaService;
        private readonly IValidator<Tema> _temaValidator;

        public TemaController(
            ITemaService temaService,
            IValidator<Tema> temaValidator
            )
        {
            _temaService = temaService;
            _temaValidator = temaValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _temaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _temaService.GetById(id);

            if (Resposta is null)
                return NotFound();

            return Ok(Resposta);
        }

        [HttpGet("assunto/{assunto}")]
        public async Task<ActionResult> GetByAssunto(string assunto)
        {
            return Ok(await _temaService.GetByAssunto(assunto));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Tema tema)
        {

            var validarTema = await _temaValidator.ValidateAsync(tema);

            if (!validarTema.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarTema);
            }

            await _temaService.Create(tema);

            return CreatedAtAction(nameof(GetById), new { id = tema.Id }, tema);

        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Tema tema)
        {
            if (tema.Id == 0)
                return BadRequest("Id da Tema é inválido!");

            var validarTema = await _temaValidator.ValidateAsync(tema);

            if (!validarTema.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarTema);
            }

            var Resposta = await _temaService.Update(tema);

            if (Resposta is null)
                return NotFound("Tema não Encontrado!");

            return Ok(Resposta);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaTema = await _temaService.GetById(id);

            if (BuscaTema is null)
                return NotFound("Tema não foi encontrado!");

            await _temaService.Delete(BuscaTema);

            return NoContent();

        }
    }
}

