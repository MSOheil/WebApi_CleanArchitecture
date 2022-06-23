using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.CustomeIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Medicine> Medicines { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Visit>().HasOne(a => a.Patient)
                   .WithMany(a => a.Visits).HasForeignKey(a => a.PatientId);

            builder.Entity<Visit>().HasOne(a => a.ApplicationUser)
                  .WithMany(a => a.Visits).HasForeignKey(a => a.UserId);

            builder.Entity<Visit>().HasOne(a => a.ApplicationUser)
                  .WithMany(a => a.Visits).HasForeignKey(a => a.DoctorId);


            base.OnModelCreating(builder);
        }
    }
}
