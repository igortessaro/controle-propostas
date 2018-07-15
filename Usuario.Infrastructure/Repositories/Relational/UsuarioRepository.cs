using Framework.Domain.Dtos;
using Framework.Domain.Factories;
using Framework.Infrastructure.Repositories.Relational;
using System.Collections.Generic;
using System.Linq;
using Usuario.Domain.Dtos;
using Usuario.Domain.Factories;
using Usuario.Domain.Repositories;

namespace Usuario.Infrastructure.Repositories.Relational
{
    public class UsuarioRepository : RelationalRepository<Usuario.Domain.Entity.Usuario>, IUsuarioRepository
    {
        private IUsuarioFactory UsuarioFactory { get; }

        private IResponseFactory ResponseFactory { get; set; }

        public UsuarioRepository(PrincipalDbContext dbContext, IUsuarioFactory usuarioFactory, IResponseFactory responseFactory)
            : base(dbContext)
        {
            this.UsuarioFactory = usuarioFactory;
            this.ResponseFactory = responseFactory;
        }

        public ResponseDto Inserir(UsuarioDto usuario)
        {
            Usuario.Domain.Entity.Usuario entity = this.UsuarioFactory.CriarDominio(usuario);

            this.Insert(entity);

            return this.ResponseFactory.Success();
        }

        public ResponseDto Atualizar(UsuarioDto usuario)
        {
            var entity = this.QueryAsTracking().FirstOrDefault(x => x.Cpf.Equals(usuario.Cpf));

            if (entity == null)
            {
                return this.ResponseFactory.Fail($"Nenhum usuário encontrado para o CPF:{usuario.Cpf}.");
            }

            entity.PopularDados(usuario.Nome, usuario.Cpf, usuario.Email, usuario.DataNascimento, usuario.Perfil);

            this.Update(entity);

            return this.ResponseFactory.Success();
        }

        public ResponseDto Deletar(string cpf)
        {
            var entity = this.QueryAsTracking().FirstOrDefault(x => x.Cpf.Equals(cpf));

            if (entity == null)
            {
                return this.ResponseFactory.Fail($"Nenhum usuário encontrado para o CPF:{cpf}.");
            }

            this.Delete(entity);

            return this.ResponseFactory.Success();
        }

        public IList<UsuarioDto> Consultar()
        {
            var entityList = this.QueryAll().Select(u => this.UsuarioFactory.CriarDto(u)).ToList();

            return entityList;
        }

        public UsuarioDto Consultar(string cpf)
        {
            var entity = this.Query().Where(u => u.Cpf.Equals(cpf)).FirstOrDefault();

            if (entity == null)
            {
                return null;
            }

            return this.UsuarioFactory.CriarDto(entity);
        }
    }
}
