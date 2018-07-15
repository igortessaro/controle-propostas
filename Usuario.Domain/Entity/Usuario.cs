using Framework.Domain.Core.Entity;
using System;
using Usuario.Domain.ValueObjects.Enumerators;

namespace Usuario.Domain.Entity
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; }

        public string Cpf { get; private set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; private set; }

        public string ChaveAcesso { get; private set; }

        public Perfil Perfil { get; private set; }

        public void PopularDados(string nome, string cpf, string email, DateTime dataNascimento, Perfil perfil, string chaveAcesso)
        {
            this.Nome = nome;
            this.Cpf = this.RemoverCaracteresEspeciais(cpf);
            this.Email = email;
            this.DataNascimento = dataNascimento;
            this.Perfil = perfil;
            this.ChaveAcesso = chaveAcesso;
        }

        private string RemoverCaracteresEspeciais(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Replace("-", string.Empty).Replace(".", string.Empty);
        }
    }
}
