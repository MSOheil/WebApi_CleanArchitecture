using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Patients.Queries;
using Microsoft.EntityFrameworkCore;

namespace Application.Patients.Queries
{
    public class GetPatientsWithIdQuery : IRequest<ReadPatientWithIdDto>
    {
        public Guid PatienId { get; set; }
    }
    public class GetPatientsWithIdQueryHandler : IRequestHandler<GetPatientsWithIdQuery, ReadPatientWithIdDto>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;
        public GetPatientsWithIdQueryHandler(IApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ReadPatientWithIdDto> Handle(GetPatientsWithIdQuery request, CancellationToken cancellationToken)
        {
            var patientById = _db.Patients.Include(a => a.Visits).SingleOrDefault(a => a.PatientId == request.PatienId);


            return await Task.FromResult(_mapper.Map<ReadPatientWithIdDto>(patientById));
        }
    }
}
