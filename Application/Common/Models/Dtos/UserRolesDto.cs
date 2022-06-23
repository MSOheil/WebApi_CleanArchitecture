using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.Dtos
{
   public class UserRolesDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}
