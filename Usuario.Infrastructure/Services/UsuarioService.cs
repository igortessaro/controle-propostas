using System;
using System.Collections.Generic;
using Usuario.Domain.Dtos;
using Usuario.Domain.Repositories;
using Usuario.Domain.Services;

namespace Usuario.Infrastructure.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository UsuarioRepository { get; set; }

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.UsuarioRepository = usuarioRepository;
        }

        public UsuarioDto ObterUsuario(string cpf)
        {
            return this.UsuarioRepository.Consultar(cpf);
        }

        public IList<UsuarioDto> ObterTodosUsuarios()
        {
            return this.UsuarioRepository.Consultar();
        }

        public void CriarUsuario(UsuarioDto usuario)
        {
            this.UsuarioRepository.Inserir(usuario);
        }
    }
}
