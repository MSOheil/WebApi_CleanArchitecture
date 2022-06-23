using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Commands.UpdatePatient
{
    class UpdateVisitsCommandValidator:AbstractValidator<UpdatePatientCommand>
    {
        public UpdateVisitsCommandValidator()
        {
            RuleFor(a =>a.PatientId)
                .NotEmpty();
        }
    }
}
