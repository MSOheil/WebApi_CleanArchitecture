using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visits.Commands.UpdateVisits
{
    public class UpdateVisitsCommand : IRequest
    {
        public Guid VisitId { get; set; }
        public DateTime VisitedAt { get; set; }
    }
    public class UpdateVisitsCommandHandler : IRequestHandler<UpdateVisitsCommand>
    {
        private readonly IApplicationDbContext _db;
        public UpdateVisitsCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Unit> Handle(UpdateVisitsCommand request, CancellationToken cancellationToken)
        {
            var visit = await _db.Visits.FindAsync(request.VisitId);




            visit.VisitedAt = request.VisitedAt;




            _db.Visits.Update(visit);




            await _db.SaveChangesAsync(cancellationToken);



            return Unit.Value;
        }
    }
}
