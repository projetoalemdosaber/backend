using FluentValidation;
using RedeSocial.Model;

namespace RedeSocial.Validator
{
    public class PostagemValidator : AbstractValidator<Postagem>
    {
        public PostagemValidator() 
        {
            RuleFor(p => p.Titulo)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(p => p.Texto)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(1000);

            RuleFor(p => p.Foto)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(2000);

            RuleFor(p => p.Video)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(1000);

            RuleFor(p => p.Curtir)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Amei)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Indico)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

        }
    }
}
