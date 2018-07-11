using Framework.Domain.Core.Service;
using System.Collections.Generic;
using Usuario.Domain.Dtos;

namespace Usuario.Domain.Services
{
    public interface IUsuarioService : IService
    {
        void CriarUsuario(UsuarioDto usuario);

        UsuarioDto ObterUsuario(string cpf);

        IList<UsuarioDto> ObterTodosUsuarios();
    }
}