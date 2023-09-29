using RedeSocial;
using FluentValidation;
using RedeSocial.Model;

namespace RedeSocial.Validator
{
    public class TemaValidator : AbstractValidator<Tema>
    {

        public TemaValidator()
        {
            RuleFor(t => t.Descricao)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(2000);

            RuleFor(t => t.Assunto)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(255);
        }
    }
}
