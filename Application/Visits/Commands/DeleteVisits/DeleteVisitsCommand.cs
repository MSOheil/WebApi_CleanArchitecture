using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visits.Commands.DeleteVisits
{
    public class DeleteVisitsCommand : IRequest
    {
        public Guid VisitId { get; set; }
    }
    public class DeleteVisitsCommandHandler : IRequestHandler<DeleteVisitsCommand>
    {
        private readonly IApplicationDbContext _db;
        public DeleteVisitsCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Unit> Handle(DeleteVisitsCommand request, CancellationToken cancellationToken)
        {
            var visit = await _db.Visits.FindAsync(request.VisitId);



            if(visit is null)
            {
                throw new NotFoundException(nameof(Patient), request.VisitId);
            }



            visit.IsActive = false;


            _db.Visits.Update(visit);


            await _db.SaveChangesAsync(cancellationToken);



            return Unit.Value;
        }
    }


}
