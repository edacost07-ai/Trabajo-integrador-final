namespace integrador_final.Dtos.Responses
{
    public class AirlinesListDto
    {
        public int Id { get; set; }

        public string Iatacode { get; set; } = null!;

        public string? Icaocode { get; set; }

        public string Name { get; set; } = null!;
    }
}
