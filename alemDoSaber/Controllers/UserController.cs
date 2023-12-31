﻿using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Model;
using RedeSocial.Security;
using RedeSocial.Service;

namespace RedeSocial.Controllers
{

    [Route("~/usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<User> _userValidator;
        private readonly IAuthService _authService;
        
        public UserController(
            IUserService userService,
            IValidator<User> userValidator,
                IAuthService authService)
        {
            _userService = userService;
            _userValidator = userValidator;
            _authService = authService;
        }

        [Authorize]
        [HttpGet("todos")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [Authorize]
        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _userService.GetById(id);

            if (Resposta is null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return Ok(Resposta);
        }

        [HttpGet("email/{usuario}")]
        public async Task<ActionResult> GetByEmail(string usuario)
        {
            var Resposta = await _userService.GetByEmail(usuario);

            if (Resposta is null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return Ok(Resposta);
        } 

        [AllowAnonymous]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> Create([FromBody] User usuario)
        {
            var validarUser = await _userValidator.ValidateAsync(usuario);

            if (!validarUser.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarUser);

            var Resposta = await _userService.Create(usuario);

            if (Resposta is null)
                return BadRequest("Usuário já cadastrado!");

            return CreatedAtAction(nameof(GetById), new { id = Resposta.Id }, Resposta);
        }
        [Authorize]
        [HttpPut("atualizar")]
        public async Task<ActionResult> Update([FromBody] User usuario)
        {
            if (usuario.Id == 0)
                return BadRequest("O Id do Usuário é inválido!");

            var validarUser = await _userValidator.ValidateAsync(usuario);

            if (!validarUser.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarUser);

            var UserUpdate = await _userService.GetByUsuario(usuario.Usuario);

            if ((UserUpdate is not null) && (UserUpdate.Id != usuario.Id))
                return BadRequest("O Usuário (e-mail) já está em uso por outro usuário.");

            var Resposta = await _userService.Update(usuario);

            if (Resposta is null)
                return NotFound("Usuário não encontrado!");

            return Ok(Resposta);
        }

        [AllowAnonymous]
        [HttpPost("logar")]
        public async Task<ActionResult> Autenticar([FromBody] UserLogin usuarioLogin)
        {
            var Resposta = await _authService.Autenticar(usuarioLogin);

            if (Resposta is null)
                return Unauthorized("Usuário e/ou senha inválidos!");

            return Ok(Resposta);
        }
    }
    
}
