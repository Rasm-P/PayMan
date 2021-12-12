using System.ComponentModel.DataAnnotations;

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
