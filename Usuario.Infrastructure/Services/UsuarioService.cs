using Framework.Domain.Dtos;
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

        public ResponseDto ExcluirUsuario(string cpf)
        {
            return this.UsuarioRepository.Deletar(cpf);
        }

        public UsuarioDto ObterUsuarioPorCpf(string cpf)
        {
            return this.UsuarioRepository.ConsultarPorCpf(cpf);
        }

        public UsuarioDto ObterUsuarioPorEmail(string email)
        {
            return this.UsuarioRepository.ConsultarPorEmail(email);
        }

        public IList<UsuarioDto> ObterTodosUsuarios()
        {
            return this.UsuarioRepository.Consultar();
        }

        public ResponseDto CriarUsuario(UsuarioDto usuario)
        {
            return this.UsuarioRepository.Inserir(usuario);
        }

        public ResponseDto AtualizarUsuario(UsuarioDto usuario)
        {
            return this.UsuarioRepository.Atualizar(usuario);
        }
    }
}
