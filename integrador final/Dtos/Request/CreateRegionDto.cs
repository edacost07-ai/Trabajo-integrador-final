using System.ComponentModel.DataAnnotations;

namespace integrador_final.Dtos.Request
{
    public class CreateRegionDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "el id del pais debe ser mayor a 0")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "se debe indicar el pais")]
        public int CountryId { get; set; }

        [MaxLength(10, ErrorMessage = "el codigo debe tener maximo 10 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "se debe indicar el codigo")]
        public string Code { get; set; } = null!;

        [MaxLength(5, ErrorMessage = "el codigo local debe tener maximo 5 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "se debe indicar el codigo local")]
        public string LocalCode { get; set; } = null!;

        [MaxLength(250, ErrorMessage = "el nombre debe tener maximo 250 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "se debe indicar el nombre")]
        public string Name { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "las palabras clave deben tener maximo 100 caracteres")]
        public string? Keywords { get; set; }

        [MaxLength(100, ErrorMessage = "el link de wikipedia debe tener maximo 100 caracteres")]
        public string? WikipediaLink { get; set; }
    }
}
