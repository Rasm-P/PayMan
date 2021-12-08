using System;
using System.ComponentModel.DataAnnotations;

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
