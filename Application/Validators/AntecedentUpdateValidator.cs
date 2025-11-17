using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class AntecedentUpdateValidator : AbstractValidator<AntecedentUpdate>
    {
        public AntecedentUpdateValidator() {
            RuleFor(x => x.Category)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.Category))
                .WithMessage("La categoría no puede estar vacía si se proporciona")
                .MaximumLength(100)
                .WithMessage("La categoría no puede exceder 100 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("La descripción no puede exceder 2000 caracteres");

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La fecha de inicio no puede ser futura");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.EndDate.HasValue)
                .WithMessage("La fecha de fin debe ser posterior a la fecha de inicio");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("El estado es obligatorio")
                .Must(status => status == "Signed")
                .WithMessage("El estado debe ser Signed");
        }
    }
}
