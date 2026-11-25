using System.ComponentModel.DataAnnotations;

namespace integrador_final.Dtos.Request
{
    public class CreateCountryDto
    {

        [MaxLength(2, ErrorMessage = "el codigo debe tener maximo 2 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "se debe indicar el codigo")]
        public string Code { get; set; } = null!;


        [MaxLength(200, ErrorMessage = "el nombre debe tener maximo 200 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "se tiene que indicar el nombre")]
        public string Name { get; set; } = null!;

        [MaxLength(2, ErrorMessage = "el continente debe tener maximo 2 caracteres")]
        public string? Continent { get; set; }

        public List<CreateRegionDto>? Regions { get; set; }

        public string? Keywords { get; set; }

        [MaxLength(250)]
        public string? WikipediaLink { get; set; }
    }
}
