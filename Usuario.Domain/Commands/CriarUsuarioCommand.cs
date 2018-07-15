using System;
using Usuario.Domain.ValueObjects.Enumerators;

namespace Usuario.Domain.Commands
{
    public class CriarUsuarioCommand
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public Perfil Perfil { get; set; }

        public string ChaveAcesso { get; set; }
    }
}
