using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.CustomeIdentity;

namespace Domain.Entities
{
    public class Visit
    {
        public Guid VisitId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? VisitedAt { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public string DoctorId { get; set; }
        // Navigation Property
        public Patient Patient { get; set; }
        public IEnumerable<VisitsMedicines> VisitsMedicines { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
