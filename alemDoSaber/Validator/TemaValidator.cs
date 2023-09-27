using RedeSocial;
using FluentValidation;

namespace RedeSocial.Validator
{
    public class TemaValidator : AbstractValidator<Tema>
    {

        public TemaValidator()
        {
            RuleFor(t => t.Descricao)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(t => t.Assunto)
               .NotEmpty()
               .MinimumLenght(5)
               .MaximumLength(255);
        }
    }
}
