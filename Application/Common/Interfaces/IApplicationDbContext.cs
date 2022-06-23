using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Threading;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
