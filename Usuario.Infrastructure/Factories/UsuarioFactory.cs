using Framework.Domain.Core.Factory;
using System;
using Usuario.Domain.Dtos;
using Usuario.Domain.Factories;
using Usuario.Domain.ValueObjects.Enumerators;

namespace Usuario.Infrastructure.Factories
{
    public class UsuarioFactory : IUsuarioFactory
    {
        public UsuarioDto CriarDto(Usuario.Domain.Entity.Usuario source)
        {
            if (source == null)
            {
                return null;
            }

            UsuarioDto result = this.CriarDto(source.Cpf, source.DataNascimento, source.Nome, source.Perfil, source.Email);

            return result;
        }

        public UsuarioDto CriarDto(string cpf, DateTime dataNascimento, string nome, Perfil perfil, string email)
        {
            UsuarioDto result = new UsuarioDto(cpf, dataNascimento, nome, perfil, email);

            return result;
        }

        public Usuario.Domain.Entity.Usuario CriarDominio(UsuarioDto source)
        {
            if (source == null)
            {
                return null;
            }

            Usuario.Domain.Entity.Usuario result = new Domain.Entity.Usuario();

            result.PopularDados(source.Nome, source.Cpf, source.Email, source.DataNascimento, source.Perfil);

            return result;
        }
    }
}
