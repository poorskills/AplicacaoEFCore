using FluentValidation;

namespace AplicacaoEFCore
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(o => o.Nome)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(5, 50);

            RuleFor(o => o.Telefone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(7);

        }
    }
}
