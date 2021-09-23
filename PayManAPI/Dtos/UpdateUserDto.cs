using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PayManAPI.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(50)]
        public String UserName { get; init; }
        [Required]
        [StringLength(30)]
        public String Password { get; init; }
    }
}
