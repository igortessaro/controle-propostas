using System;
using Usuario.Domain.ValueObjects.Enumerators;

namespace Usuario.Domain.Dtos
{
    public class UsuarioDto
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public DateTime DataNascimento { get; set; }

        public Perfil Perfil { get; set; }
    }
}
