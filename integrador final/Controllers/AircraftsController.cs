using integrador_final.Dtos.Request;
using integrador_final.Dtos.Responses;
using integrador_final.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace integrador_final.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AircraftsController : ControllerBase
    {
      private readonly AirportDbContext _context;

        public AircraftsController(AirportDbContext context)
        {
            _context = context;
        }

        //Consulta Nivel simple

        [HttpGet]
        public async Task<ActionResult<List<AircraftListDto>>> GetAircrafts([FromQuery] string name, [FromQuery] PageRequest pageRequest)
        {
            var query = _context.Aircrafts.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name != null && a.Name.StartsWith(name));
            }

            var result = await query
                .Select(a => new AircraftListDto
                {
                    Id = a.Id,
                    Name = a.Name ?? string.Empty,
                    Iatacode = a.Iatacode ?? string.Empty,
                })
                .ToListAsync();
            
            return Ok(result);
        }
    }
}
