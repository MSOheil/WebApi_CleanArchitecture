using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public  class Medicine
    {
        public Guid MedicineId { get; set; }
        public string Name { get; set; }
        //Navigation Property
        public VisitsMedicines VisitsMedicines { get; set; }
    }
}
