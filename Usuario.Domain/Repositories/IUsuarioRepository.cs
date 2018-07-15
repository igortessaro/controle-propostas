using Framework.Domain.Core.Repositories;
using Framework.Domain.Dtos;
using System.Collections.Generic;
using Usuario.Domain.Dtos;

namespace Usuario.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository
    {
        UsuarioDto Consultar(string cpf);

        IList<UsuarioDto> Consultar();

        ResponseDto Inserir(UsuarioDto usuario);

        ResponseDto Deletar(string cpf);

        ResponseDto Atualizar(UsuarioDto usuario);
    }
}