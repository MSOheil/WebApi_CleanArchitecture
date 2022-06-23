using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visits.Commands.CreateVisits
{
   public class CreateVisitCommand :IRequest<Guid>
    {
        public virtual DateTime CreateAt { get; set; }
        public virtual DateTime? VisitedAt { get; set; }
        public virtual Guid PatientId { get; set; }
        public bool IsActive { get; set; }


    }
    public class CreateVisitCommandHandler : IRequestHandler<CreateVisitCommand, Guid>
    {
        private readonly IApplicationDbContext _db;
        public CreateVisitCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> Handle(CreateVisitCommand request, CancellationToken cancellationToken)
        {
            var visit = new Visit
            {
                CreateAt = request.CreateAt,

                PatientId = request.PatientId,

                VisitedAt = request.VisitedAt,

                IsActive = true

            };




            await _db.Visits.AddAsync(visit);



            await _db.SaveChangesAsync(cancellationToken);


            return visit.VisitId;
        }
    }
}
