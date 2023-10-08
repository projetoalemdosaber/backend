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
                .MaximumLength(5000);

            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);

            RuleFor(u => u.DataNascimento)
                .NotEmpty()
                .GreaterThanOrEqualTo(u => DateTime.Today.AddYears(-120)).WithMessage("Idade incorreta, adicione uma idade válida!")
                .LessThanOrEqualTo(u => DateTime.Today.AddYears(-13)).WithMessage("Menores de 13 anos não podem se cadastrar na plataforma.");
        }
    }
}
