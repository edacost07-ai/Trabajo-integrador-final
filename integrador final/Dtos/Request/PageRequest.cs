using System.ComponentModel.DataAnnotations;

namespace integrador_final.Dtos.Request
{
    public class PageRequest
    {
        public int PageNumber { get; set; } = 1;

        [Range(1, 30, ErrorMessage = "el rango debe ser entre 1 y 30")]
        public int PageSize { get; set; } = 10;

    }
}
