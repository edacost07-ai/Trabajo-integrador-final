using integrador_final.Dtos.Request;
using integrador_final.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace integrador_final.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly AirportDbContext _context;
        
        public AirportsController(AirportDbContext context)
        {
            _context = context;
        }

        //consulta nivel intermedio
        [HttpPut("{airportId}")]
        public async Task<IActionResult> UpdateAirport(int airportId, [FromBody] UpdateAirportDto dto)
        {
            var airport = await _context.Airports
                .FirstOrDefaultAsync(a => a.Id == airportId);

            if (airport == null)
            {
                return NotFound("Airport not found");
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(c => c.Id == dto.CountryId);

            if (country == null)
            {
                return BadRequest("Country not found");
            }

            if (dto.RegionId.HasValue)
            {
                var region = await _context.Regions
                    .FirstOrDefaultAsync(r => r.Id == dto.RegionId);

                if (region == null)
                {
                    return BadRequest("Region not found");
                }
            }

            airport.CountryId = dto.CountryId;
            airport.RegionId = dto.RegionId;
            airport.AirportCode = dto.AirportCode;
            airport.Type = (byte)dto.Type;
            airport.Name = dto.Name;
            airport.City = dto.City;
            airport.Municipality = dto.Municipality;
            airport.Latitude = dto.Latitude;
            airport.Longitude = dto.Longitude;
            airport.ElevationFt = dto.ElevationFt;
            airport.ScheduledService = dto.ScheduledService;
            airport.GpsCode = dto.GpsCode;
            airport.Iatacode = dto.Iatacode;
            airport.LocalCode = dto.LocalCode;
            airport.HomeLink = dto.HomeLink;
            airport.WikipediaLink = dto.WikipediaLink;
            airport.Keywords = dto.Keywords;
            airport.Description = dto.Description;
            airport.FrequencyMhz = dto.FrequencyMhz;
            airport.RunwayLengthFt = dto.RunwayLengthFt;
            airport.RunwayWidthFt = dto.RunwayWidthFt;
            airport.RunwaySurface = dto.RunwaySurface;
            airport.RunwayLighted = dto.RunwayLighted;
            airport.RunwayClosed = dto.RunwayClosed;

            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
