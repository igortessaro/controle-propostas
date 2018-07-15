using Framework.Domain.Core.Factory;
using System;
using Usuario.Domain.Dtos;
using Usuario.Domain.ValueObjects.Enumerators;

namespace Usuario.Domain.Factories
{
    public interface IUsuarioFactory : IFactory
    {
        Entity.Usuario CriarDominio(UsuarioDto source);

        UsuarioDto CriarDto(Entity.Usuario source);

        UsuarioDto CriarDto(string cpf, DateTime dataNascimento, string nome, Perfil perfil, string email, string chaveAcesso);
    }
}