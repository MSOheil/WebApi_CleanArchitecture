using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visits.Commands.UpdateVisits
{
    class UpdateVisitsCommandValidator : AbstractValidator<UpdateVisitsCommand>
    {
        public UpdateVisitsCommandValidator()
        {
            RuleFor(a =>new { a.VisitId , a.VisitedAt })
                .NotEmpty();
        }
    }
}
