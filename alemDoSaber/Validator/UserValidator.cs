using FluentValidation;
using RedeSocial.Model;

namespace RedeSocial.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(u => u.Usuario)
                .NotEmpty()
                .WithMessage("E-mail obrigatório!")
                .EmailAddress()
                .WithMessage("É necessário colocar um e-mail válido!");

            RuleFor(u => u.Foto)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(5000);

            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);
        }
    }
}
