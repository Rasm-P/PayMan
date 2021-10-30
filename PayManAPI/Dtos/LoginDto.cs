using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Dtos
{
    public record LoginDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; init; }
        [Required]
        [StringLength(30)]
        public string Password { get; init; }
    }
}
