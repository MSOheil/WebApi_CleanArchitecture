using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Entities
{
    public class Patient
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; }
        public double NationalCode { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsActive { get; set; }

        //Navigation Property
        public IEnumerable<Visit> Visits { get; set; }
    }
}
