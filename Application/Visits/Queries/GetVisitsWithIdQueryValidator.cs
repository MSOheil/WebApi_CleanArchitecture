using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visits.Queries
{
    public class GetVisitsWithIdQueryValidator : AbstractValidator<GetVisitsWithIdQuery>
    {
        public GetVisitsWithIdQueryValidator()
        {
            RuleFor(a => a.VisitId)
                .NotEmpty();
        }
    }
}
