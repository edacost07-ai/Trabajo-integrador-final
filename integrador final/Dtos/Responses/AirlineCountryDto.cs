namespace integrador_final.Dtos.Responses
{
    public class AirlineCountryDto
    {

        public int CountryId { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public bool Native { get; set; }

    }
}
