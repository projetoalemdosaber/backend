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
                .MaximumLength(2000);

            RuleFor(t => t.Assunto)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(255);
        }
    }
}
