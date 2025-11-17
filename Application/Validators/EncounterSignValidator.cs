using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class EncounterSignValidator : AbstractValidator<EncounterSign>
    {
        public EncounterSignValidator() {
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("El campo Status es obligatorio para firmar el encuentro")
                .Must(status => status == "Signed")
                .WithMessage("El estado debe ser Signed");

            RuleFor(x => x.Notes)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrEmpty(x.Notes))
                .WithMessage("El campo Objetivo no puede exceder 2000 caracteres");
        }
    }
}
