namespace integrador_final.Dtos.Responses
{
    public class RegionListDto
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public string Code { get; set; } = null!;

        public string LocalCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Keywords { get; set; }

        public string? WikipediaLink { get; set; }

        public int AirportsCount { get; set; }
    }
}
