using Clientes.Dominio.Entidades;
using Clientes.Dominio.ObjetosDeValor;
using FluentValidation;

namespace Clientes.Dominio.Validacoes
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome não pode estar vazio")
                .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");

            RuleFor(c => c.Email.Endereco)
                .NotEmpty().WithMessage("O email é obrigatório")
                .EmailAddress().WithMessage("O email informado não é válido");

            RuleFor(c => c.DataNascimento)
                .Must((cliente, dataNascimento) => {
                    if (cliente.Documento.Tipo != TipoDocumento.CPF)
                        return true;

                    var idade = DateTime.Now.Year - dataNascimento.Year;
                    if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                        idade--;

                    return idade >= 18;
                })
                .When(c => c.Documento.Tipo == TipoDocumento.CPF)
                .WithMessage("Cliente deve ter pelo menos 18 anos");

            RuleFor(c => c.InscricaoEstadual)
                .NotEmpty()
                .When(c => c.Documento.Tipo == TipoDocumento.CNPJ && !c.Isento)
                .WithMessage("É necessário informar a Inscrição Estadual ou marcar como Isento");
        }
    }
}
