using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Patients.Commands.CreatePatient
{
   public class CreatePatientCommand :IRequest<Guid>
    {
        public string Name { get; set; }
        public double NationalCode { get; set; }
        public DateTime Birthday { get; set; }
    }
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid>
    {
        private readonly IApplicationDbContext _db;
        public CreatePatientCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = new Patient
            {
                Birthday = request.Birthday,

                Name = request.Name,

                NationalCode = request.NationalCode,

                IsActive = true

            };




            await _db.Patients.AddAsync(patient);



            await _db.SaveChangesAsync(cancellationToken);


            return patient.PatientId;
        }
    }
}
