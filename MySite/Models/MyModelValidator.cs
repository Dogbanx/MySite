using FluentValidation;

namespace MySite.Models
{
    public class MyModelValidator : AbstractValidator<ModelValid> { 

        public MyModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ім'я є обов'язковим");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Прізвище є обов'язковим");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Будь ласка, введіть коректну електронну адресу");
            RuleFor(x => x.Phone).NotEmpty().Matches(@"^\+\d{1,3}-\d{3,14}$").WithMessage("Будь ласка, введіть коректний номер телефону у форматі +код-номер");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Тема є обов'язковою");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Повідомлення є обов'язковим");
        }
    }
}
