using System.ComponentModel.DataAnnotations;

namespace integrador_final.Dtos.Request
{
    public class AirlineCreateDto
    {
        [Required]
        [StringLength(5, MinimumLength = 2)]
        public string Iatacode { get; set; } = null!;

        [StringLength(5, MinimumLength = 3)]
        public string? Icaocode { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(1, ErrorMessage = "Debe indicar al menos un país.")]
        public List<AirlineCountryCreateDto> Countries { get; set; } = new();

    }
}



