using Framework.Domain.Core.Service;
using Framework.Domain.Dtos;
using System.Collections.Generic;
using Usuario.Domain.Dtos;

namespace Usuario.Domain.Services
{
    public interface IUsuarioService : IService
    {
        ResponseDto CriarUsuario(UsuarioDto usuario);

        ResponseDto ExcluirUsuario(string cpf);

        UsuarioDto ObterUsuarioPorCpf(string cpf);

        UsuarioDto ObterUsuarioPorEmail(string email);

        IList<UsuarioDto> ObterTodosUsuarios();

        ResponseDto AtualizarUsuario(UsuarioDto usuario);
    }
}