using System.ComponentModel.DataAnnotations;

namespace integrador_final.Dtos.Request
{
    public class AirlineCountryCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El CountryId debe ser mayor a cero.")]
        public int CountryId { get; set; }

        public bool Native { get; set; }

    }
}