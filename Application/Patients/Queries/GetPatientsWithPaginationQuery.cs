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

namespace Application.Patients.Queries
{
    public class GetPatientsWithPaginationQuery : IRequest<IEnumerable<ReadPatientDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool sortByBirthday { get; set; }
    }
    public class GetPatientsWithPaginationQueryHandler : IRequestHandler<GetPatientsWithPaginationQuery, IEnumerable<ReadPatientDto>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;
        public GetPatientsWithPaginationQueryHandler(IApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReadPatientDto>> Handle(GetPatientsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var patients = await _db.Patients.Where(a => a.IsActive == true).PaginatedListAsync(request.PageNumber, request.PageSize, request.sortByBirthday);
            var map = _mapper.Map<IEnumerable<ReadPatientDto>>(patients.Items);
            return map;
        }
    }
}
