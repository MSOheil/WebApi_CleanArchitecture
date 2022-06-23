using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Patients.Commands
{
    public class DeletePatientCommand : IRequest
    {
        public Guid PatientId { get; set; }
    }
    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
    {
        private readonly IApplicationDbContext _db;
        public DeletePatientCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _db.Patients.FindAsync(request.PatientId);



            if(patient is null)
            {
                throw new NotFoundException(nameof(Patient), request.PatientId);
            }



            patient.IsActive = false;


            _db.Patients.Update(patient);


            await _db.SaveChangesAsync(cancellationToken);



            return Unit.Value;
        }
    }


}
