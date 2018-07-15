using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Usuario.Domain.Commands;
using Usuario.Domain.Dtos;
using Usuario.Domain.Services;
using System.Linq;
using Usuario.Domain.Factories;
using Usuario.Domain.ValueObjects.Enumerators;
using System;
using Framework.Domain.Factories;
using Framework.Infrastructure.Extensions;

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

        [HttpGet("{cpf}", Name = "ObterUsuarioPorCpf")]
        public IActionResult ObterUsuario(string cpf)
        {
            var usuario = this.UsuarioService.ObterUsuario(cpf);

            return this.Ok(usuario);
        }

        [HttpGet]
        public IActionResult ObterTodosUsuarios()
        {
            IList<UsuarioDto> usuarios = this.UsuarioService.ObterTodosUsuarios();

            return this.Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Criar([FromBody]CriarUsuarioCommand usuario)
        {
            if (usuario == null)
            {
                return this.BadRequest($"{nameof(usuario)} é obrigatório.");
            }

            UsuarioDto entity = this.UsuarioFactory.CriarDto(usuario.Cpf, usuario.DataNascimento, usuario.Nome, usuario.Perfil, usuario.Email);

            var response = this.UsuarioService.CriarUsuario(entity);

            if (!response.Success)
            {
                return this.BadRequest(response.Error);
            }

            var result = this.CreatedAtRoute(routeName: "ObterUsuarioPorCpf", routeValues: new { cpf = entity.Cpf }, value: entity);

            return result;
        }

        [HttpPut("{cpf}")]
        public IActionResult Atualizar([FromBody]CriarUsuarioCommand usuario, string cpf)
        {
            if (usuario == null)
            {
                return this.BadRequest($"{nameof(usuario)} é obrigatório.");
            }

            UsuarioDto entity = this.UsuarioFactory.CriarDto(cpf, usuario.DataNascimento, usuario.Nome, usuario.Perfil, usuario.Email);

            var response = this.UsuarioService.AtualizarUsuario(entity);

            if (!response.Success)
            {
                return this.BadRequest(response.Error);
            }

            return this.Ok(usuario);
        }

        [HttpGet("perfis")]
        public IActionResult ObterPerfis()
        {
            var result = Enum.GetValues(typeof(Perfil))
                .Cast<Perfil>()
                .Select(x => this.ListItemFactory.Create((int)x, x.Description()))
                .ToList();

            return this.Ok(result);
        }

        // TODO: Remover - método criado apenas para facilitar testes.
        [HttpPost("postAll")]
        public IActionResult CriarVarios([FromBody]CriarUsuarioCommand[] usuarios)
        {
            usuarios.ForEach(u => this.Criar(u));

            return this.Ok();
        }
    }
}