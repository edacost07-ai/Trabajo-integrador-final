using integrador_final.Dtos.Request;

namespace integrador_final.Dtos.Responses
{
    public class CountryListDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Continent { get; set; }

        public string? Keywords { get; set; }

        public string? WikipediaLink { get; set; }
    }
}
