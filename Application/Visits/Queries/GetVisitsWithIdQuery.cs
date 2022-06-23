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

namespace Application.Visits.Queries
{
   public class GetVisitsWithIdQuery:IRequest<VisitReadDto>
    {
        public Guid VisitId { get; set; }
    }
    public class GetVisitsWithIdWithPaginationQueryHandler : IRequestHandler<GetVisitsWithIdQuery, VisitReadDto>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;
        public GetVisitsWithIdWithPaginationQueryHandler(IApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<VisitReadDto> Handle(GetVisitsWithIdQuery request, CancellationToken cancellationToken)
        {
            var visits = await _db.Visits.FindAsync(request.VisitId);
            return _mapper.Map<VisitReadDto>(visits);
        }
     
    }
}
