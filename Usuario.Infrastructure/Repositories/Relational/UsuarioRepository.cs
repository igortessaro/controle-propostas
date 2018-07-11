using Framework.Infrastructure.Repositories.Relational;
using Usuario.Domain.Dtos;
using System.Linq;
using Usuario.Domain.Repositories;
using System.Collections.Generic;

namespace Usuario.Infrastructure.Repositories.Relational
{
    public class UsuarioRepository : RelationalRepository<Usuario.Domain.Entity.Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }

        public void Inserir(UsuarioDto usuario)
        {
            Usuario.Domain.Entity.Usuario entity = new Domain.Entity.Usuario();

            entity.PopularDados(usuario.Nome, usuario.Cpf, usuario.DataNascimento, usuario.Perfil);

            this.Insert(entity);
        }

        public IList<UsuarioDto> Consultar()
        {
            var entityList = this.QueryAll().Select(u => this.CriarDto(u)).ToList();

            return entityList;
        }

        public UsuarioDto Consultar(string cpf)
        {
            var entity = this.Query().Where(u => u.Cpf.Equals(cpf)).FirstOrDefault();

            if (entity == null)
            {
                return null;
            }

            return this.CriarDto(entity);
        }

        private UsuarioDto CriarDto(Usuario.Domain.Entity.Usuario entity)
        {
            UsuarioDto result = new UsuarioDto();

            result.Cpf = entity.Cpf;
            result.DataNascimento = entity.DataNascimento;
            result.Nome = entity.Nome;
            result.Perfil = entity.Perfil;

            return result;
        }
    }
}
