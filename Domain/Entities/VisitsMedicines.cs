using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public  class VisitsMedicines
    {
        public Guid VisitsMedicinesId { get; set; }
        public Guid VisitId { get; set; }
        public Guid MedicineId { get; set; }
        //Navigation Property
        public Medicine Medicine { get; set; }
        public Visit Visit { get; set; }
    }
}
