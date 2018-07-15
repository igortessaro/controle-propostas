using Framework.Domain.Factories;
using Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Usuario.Domain.Commands;
using Usuario.Domain.Dtos;
using Usuario.Domain.Factories;
using Usuario.Domain.Services;
using Usuario.Domain.ValueObjects.Enumerators;

namespace Usuario.Application.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        protected IUsuarioService UsuarioService { get; }

        protected IUsuarioFactory UsuarioFactory { get; }

        protected IListItemFactory ListItemFactory { get; }

        public UsuarioController(
            IUsuarioService usuarioService,
            IUsuarioFactory usuarioFactory,
            IListItemFactory listItemFactory)
        {
            this.UsuarioService = usuarioService;
            this.UsuarioFactory = usuarioFactory;
            this.ListItemFactory = listItemFactory;
        }

        [Authorize("Bearer")]
        [HttpDelete("{cpf}")]
        public IActionResult ExcluirUsuario(string cpf)
        {
            var response = this.UsuarioService.ExcluirUsuario(cpf);

            if (!response.Success)
            {
                return this.BadRequest(response.Error);
            }

            return this.Ok();
        }

        [Authorize("Bearer")]
        [HttpGet("{cpf}", Name = "ObterUsuarioPorCpf")]
        public IActionResult ObterUsuario(string cpf)
        {
            var usuario = this.UsuarioService.ObterUsuarioPorCpf(cpf);

            return this.Ok(usuario);
        }

        [Authorize("Bearer")]
        [HttpGet]
        public IActionResult ObterTodosUsuarios()
        {
            IList<UsuarioDto> usuarios = this.UsuarioService.ObterTodosUsuarios();

            return this.Ok(usuarios);
        }

        [Authorize("Bearer")]
        [HttpPost]
        public IActionResult Criar([FromBody]CriarUsuarioCommand usuario)
        {
            if (usuario == null)
            {
                return this.BadRequest($"{nameof(usuario)} é obrigatório.");
            }

            UsuarioDto entity = this.UsuarioFactory.CriarDto(usuario.Cpf, usuario.DataNascimento, usuario.Nome, usuario.Perfil, usuario.Email, usuario.ChaveAcesso);

            var response = this.UsuarioService.CriarUsuario(entity);

            if (!response.Success)
            {
                return this.BadRequest(response.Error);
            }

            var result = this.CreatedAtRoute(routeName: "ObterUsuarioPorCpf", routeValues: new { cpf = entity.Cpf }, value: entity);

            return result;
        }

        [Authorize("Bearer")]
        [HttpPut("{cpf}")]
        public IActionResult Atualizar([FromBody]CriarUsuarioCommand usuario, string cpf)
        {
            if (usuario == null)
            {
                return this.BadRequest($"{nameof(usuario)} é obrigatório.");
            }

            UsuarioDto entity = this.UsuarioFactory.CriarDto(cpf, usuario.DataNascimento, usuario.Nome, usuario.Perfil, usuario.Email, usuario.ChaveAcesso);

            var response = this.UsuarioService.AtualizarUsuario(entity);

            if (!response.Success)
            {
                return this.BadRequest(response.Error);
            }

            return this.Ok(usuario);
        }

        [Authorize("Bearer")]
        [HttpGet("perfis")]
        public IActionResult ObterPerfis()
        {
            var result = Enum.GetValues(typeof(Perfil))
                .Cast<Perfil>()
                .Select(x => this.ListItemFactory.Create((int)x, x.Description()))
                .ToList();

            return this.Ok(result);
        }
    }
}