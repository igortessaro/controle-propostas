using System.ComponentModel;

namespace Usuario.Domain.ValueObjects.Enumerators
{
    public enum Perfil
    {
        [Description("Analista de Compras")]
        AnalistaCompras,
        [Description("Analista Financeiro")]
        AnalistaFinanceiro,
        [Description("Diretor Financeiro")]
        DiretorFinanceiro,
        [Description("Administrador")]
        Administrador
    }
}
