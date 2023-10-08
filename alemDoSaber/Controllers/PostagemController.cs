using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Model;
using RedeSocial.Service;

namespace RedeSocial.Controllers
{
    [Authorize]
    [ApiController]
    [Route("~/postagens")]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _postagemService;
        private readonly IValidator<Postagem> _postagemValidator;

        public PostagemController(IPostagemService postagem, IValidator<Postagem> validator)
        {
            _postagemService = postagem;
            _postagemValidator = validator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _postagemService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var BuscarPostagens = await _postagemService.GetById(id);

            if (BuscarPostagens is null)
                return NoContent();

            return Ok(BuscarPostagens);
        }

        [HttpGet("titulo/{titulo}")]
        public async Task<ActionResult> GetByTitulo(string titulo)
        {
            return Ok(await _postagemService.GetByTitulo(titulo));
        }

        [HttpGet("data/{dataInicial}/data2/{dataFinal}")]
        public async Task<ActionResult> GetByData(DateTimeOffset dataInicial, DateTimeOffset dataFinal)
        {
           return Ok(await _postagemService.GetByData(dataInicial, dataFinal));
        }
        


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Postagem postagem)
        {
            var validarPostagem = await _postagemValidator.ValidateAsync(postagem);

            if (!validarPostagem.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);

            var Resposta = await _postagemService.Create(postagem);

            if (Resposta is null)
                return BadRequest("Tema não encontrado.");

            return CreatedAtAction(nameof(GetById), new { id = postagem.Id }, postagem);
        }

        [HttpPut("curtir/{id}")]
        public async Task<ActionResult> Curtir(long id)
        {
            var Resposta = await _postagemService.Curtir(id);

            if (Resposta is null)
            {
                return NotFound("Postagem não encontrada!");
            }

            return Ok(Resposta);
        }

        [HttpPut("amei/{id}")]
        public async Task<ActionResult> Amei(long id)
        {
            var Resposta = await _postagemService.Amei(id);

            if (Resposta is null)
            {
                return NotFound("Postagem não encontrada!");
            }

            return Ok(Resposta);
        }

        [HttpPut("indico/{id}")]
        public async Task<ActionResult> Indico(long id)
        {
            var Resposta = await _postagemService.Indico(id);

            if (Resposta is null)
            {
                return NotFound("Postagem não encontrada!");
            }

            return Ok(Resposta);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Postagem postagem)
        {
            if (postagem.Id == 0)
                return BadRequest("Id da postagem inválido");

            var validarPostagem = await _postagemValidator.ValidateAsync(postagem);

            if (!validarPostagem.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);

            var Resposta = await _postagemService.Update(postagem);

            if (Resposta is null)
                return NotFound("Postagem não encontrada");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscarPostagem = await _postagemService.GetById(id);

            if (BuscarPostagem is null)
                return NotFound("Essa postagem não foi encontrada");

            await _postagemService.Delete(BuscarPostagem);

            return NoContent();
        }

    }
}
