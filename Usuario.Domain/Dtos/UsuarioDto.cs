using System;
using Usuario.Domain.ValueObjects.Enumerators;
using Framework.Infrastructure.Extensions;

namespace Usuario.Domain.Dtos
{
    public class UsuarioDto
    {
        public UsuarioDto()
        {
        }

        public UsuarioDto(string cpf, DateTime dataNascimento, string nome, Perfil perfil, string email, string chaveAcesso)
            : this()
        {
            this.Cpf = cpf;
            this.DataNascimento = dataNascimento;
            this.Nome = nome;
            this.Perfil = perfil;
            this.Email = email;
            this.ChaveAcesso = chaveAcesso;
        }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public Perfil Perfil { get; set; }

        public string PerfilDescricao { get { return this.Perfil.Description(); } }

        public string ChaveAcesso { get; set; }

        public override string ToString()
        {
            return $"{this.Cpf} - {this.Nome} - {this.Email}";
        }
    }
}
