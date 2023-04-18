using DevFM.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevFM.Business.Models.Validations
{
    public class PessoaValidation : AbstractValidator<Pessoa>
    {
        public PessoaValidation()
        {
            RuleFor(f => f.Nome)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(2, 100)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


            RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");



            //RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
            //    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            //RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true);


        }
    }
}
