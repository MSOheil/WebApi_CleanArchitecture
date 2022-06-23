using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Queries
{
    class GetVisitsWithIdQueryValidator:AbstractValidator<GetPatientsWithIdQuery>
    {
        public GetVisitsWithIdQueryValidator()
        {
            RuleFor(a => a.PatienId)
                .NotEmpty();
        }
    }
}
