using Microsoft.AspNetCore.Mvc;
using Usuario.Domain.Commands;
using Usuario.Domain.Dtos;
using Usuario.Domain.Services;

namespace Usuario.Application.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        protected IUsuarioService UsuarioService { get; }

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.UsuarioService = usuarioService;
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
            var usuarios = this.UsuarioService.ObterTodosUsuarios();

            return this.Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Criar([FromBody]CriarUsuarioCommand usuario)
        {
            UsuarioDto entity = new UsuarioDto();

            entity.Cpf = usuario.Cpf;
            entity.DataNascimento = usuario.DataNascimento;
            entity.Nome = usuario.Nome;
            entity.Perfil = usuario.Perfil;

            this.UsuarioService.CriarUsuario(entity);

            var usuarioCriado = this.UsuarioService.ObterUsuario(usuario.Cpf);

            var result = this.CreatedAtRoute(routeName: "ObterUsuarioPorCpf", routeValues: new { cpf = entity.Cpf }, value: entity);

            return result;
        }
    }
}