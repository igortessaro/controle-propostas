using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Usuario.Domain.Services;
using Framework.Aplication.Services.Configurations;
using Usuario.Domain.Dtos;

namespace Usuario.Application.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        public IUsuarioService UsuarioService { get; set; }

        public LoginController(IUsuarioService usuarioService)
        {
            this.UsuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(
            [FromBody]UsuarioLoginDto usuario,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;

            if (usuario == null || string.IsNullOrEmpty(usuario.Login))
            {
                return this.BadRequest("Usuário não informado.");
            }

            UsuarioDto usuarioBase = this.UsuarioService.ObterUsuarioPorEmail(usuario.Login);

            credenciaisValidas = (usuarioBase != null && usuario.Login == usuarioBase.Email && usuario.Senha == usuarioBase.ChaveAcesso);

            if (!credenciaisValidas)
            {
                return this.Forbid();
            }

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(usuario.Login, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });

            var token = handler.WriteToken(securityToken);

            var result = new Framework.Domain.Dtos.ResponseTokenDto(true, dataCriacao, dataExpiracao, token);

            return this.Ok(result);
        }
    }
}