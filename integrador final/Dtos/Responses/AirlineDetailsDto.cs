namespace integrador_final.Dtos.Responses
{
    public class AirlineDetailsDto
    {
        public int Id { get; set; }

        public string Iatacode { get; set; } = null!;

        public string? Icaocode { get; set; }

        public string Name { get; set; } = null!;

        public List<AirlineCountryDto> Countries { get; set; } = new();

        public int TotalFlights { get; set; }

        public int CancelledFlights { get; set; }

        public int DivertedFlights { get; set; }

        public double? AverageDepartureDelay { get; set; }

        public int TotalReviews { get; set; }

        public double? AverageRating { get; set; }

        public int RecommendedReviews { get; set; }
    }

}

