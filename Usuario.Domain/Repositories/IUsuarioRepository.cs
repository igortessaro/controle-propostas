using Framework.Domain.Core.Repositories;
using System.Collections.Generic;
using Usuario.Domain.Dtos;

namespace Usuario.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository
    {
        UsuarioDto Consultar(string cpf);

        IList<UsuarioDto> Consultar();

        void Inserir(UsuarioDto usuario);
    }
}