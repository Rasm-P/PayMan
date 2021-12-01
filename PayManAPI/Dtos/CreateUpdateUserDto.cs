using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Dtos
{
    public class CreateUpdateUserDto
    {
        [Required]
        [StringLength(50)]
        public String UserName { get; init; }
        [Required]
        [StringLength(30)]
        public String Password { get; init; }
    }
}
