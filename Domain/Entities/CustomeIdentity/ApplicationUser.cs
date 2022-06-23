using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CustomeIdentity
{
   public class ApplicationUser :IdentityUser
    {
        public bool IsActive { get; set; }
        public IEnumerable<Visit> Visits { get; set; }
    }
}
