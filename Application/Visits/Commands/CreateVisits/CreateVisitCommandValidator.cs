using Application.Visits.Commands.CreateVisits;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visits.Commands.CreateVisits
{
    class CreateVisitCommandValidator : AbstractValidator<CreateVisitCommand>
    {
        public CreateVisitCommandValidator()
        {
            RuleFor(s => new { s.CreateAt,s.PatientId })
                .NotEmpty();
        }
    }
}
