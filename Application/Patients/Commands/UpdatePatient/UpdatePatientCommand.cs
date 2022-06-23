using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommand : IRequest
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; }
        public double NationalCode { get; set; }
        public DateTime Birthday { get; set; }
    }
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
    {
        private readonly IApplicationDbContext _db;
        public UpdatePatientCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _db.Patients.FindAsync(request.PatientId);




            patient.Name = request.Name;
            patient.NationalCode = request.NationalCode;
            patient.Birthday = request.Birthday;




            _db.Patients.Update(patient);




            await _db.SaveChangesAsync(cancellationToken);



            return Unit.Value;
        }
    }
}
