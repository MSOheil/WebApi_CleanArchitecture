using Application.Visits.Commands.DeleteVisits;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visits.Commands.DeleteVisits
{
    public class DeleteVisitsCommandValidator : AbstractValidator<DeleteVisitsCommand>
    {
        public DeleteVisitsCommandValidator()
        {
            RuleFor(a => a.VisitId)
                .NotEmpty();
        }
    }
}
