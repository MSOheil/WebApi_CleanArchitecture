using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Commands.CreatePatient
{
    class CreateVisitCommandValidator :AbstractValidator<CreatePatientCommand>
    {
        public CreateVisitCommandValidator()
        {
            RuleFor(s => new { s.Birthday, s.Name, s.NationalCode })
                .NotEmpty();
        }
    }
}
