using Framework.Domain.Core.Entity;
using System;
using Usuario.Domain.ValueObjects.Enumerators;

namespace Usuario.Domain.Entity
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; }

        public string Cpf { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public string ChaveAcesso { get; private set; }

        public Perfil Perfil { get; private set; }

        public void PopularDados(string nome, string cpf, DateTime dataNascimento, Perfil perfil)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.DataNascimento = dataNascimento;
            this.Perfil = perfil;
        }
    }
}
