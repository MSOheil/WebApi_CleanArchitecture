using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visits.Queries
{
    public class GetVisitsWithPaginationQuery : IRequest<IEnumerable<VisitReadDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool sortByBirthday { get; set; }
    }
    public class GetVisitsWithPaginationQueryHandler : IRequestHandler<GetVisitsWithPaginationQuery, IEnumerable<VisitReadDto>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;
        public GetVisitsWithPaginationQueryHandler(IApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VisitReadDto>> Handle(GetVisitsWithPaginationQuery request, CancellationToken cancellationToken)
        {
                var visits = await _db.Visits.Where(a=>a.IsActive==true).PaginatedListAsync(request.PageNumber, request.PageSize, request.sortByBirthday);
                return _mapper.Map<IEnumerable<VisitReadDto>>(visits.Items);
        }
    }
}
