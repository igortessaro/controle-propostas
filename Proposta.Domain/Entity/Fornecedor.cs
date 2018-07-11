using Framework.Domain.Core.Entity;

namespace Proposta.Domain.Entity
{
    public class Fornecedor : BaseEntity
    {
        public string CpfCnpj { get; private set; }

        public string Nome { get; private set; }

        public short Ddd { get; private set; }

        public string Telefone { get; private set; }

        public string Email { get; private set; }
    }
}
