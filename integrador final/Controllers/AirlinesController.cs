using integrador_final.Dtos.Request;
using integrador_final.Dtos.Responses;
using integrador_final.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static integrador_final.Dtos.Responses.AirlineDetailsDto;

namespace integrador_final.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AirlinesController : ControllerBase
    {
        private readonly AirportDbContext _context;

        public AirlinesController(AirportDbContext context)
        {
            _context = context;
        }
        
        //consulta nivel avanzado
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AirlineCreateDto dto)
        {
            if (dto.Countries == null || dto.Countries.Count == 0)
            {
                return BadRequest("Debe indicar al menos un país.");
            }

            var countryIds = dto.Countries.Select(c => c.CountryId).ToList();

            if (countryIds.Distinct().Count() != countryIds.Count)
            {
                return BadRequest("No se pueden repetir países.");
            }

            var countries = await _context.Countries
                .Where(c => countryIds.Contains(c.Id))
                .ToListAsync();

            if (countries.Count != countryIds.Count)
            {
                return NotFound("Uno o más países indicados no existen.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var airline = new Airline
                {
                    Name = dto.Name.Trim(),
                    Iatacode = dto.Iatacode.Trim().ToUpper(),
                    Icaocode = dto.Icaocode?.Trim().ToUpper()
                };

                _context.Airlines.Add(airline);

                await _context.SaveChangesAsync();

                foreach (var country in dto.Countries)
                {
                    var airlineCountry = new AirlineCountry
                    {
                        AirlineId = airline.Id,
                        CountryId = country.CountryId,
                        Native = country.Native
                    };

                    _context.AirlineCountries.Add(airlineCountry);
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return CreatedAtAction(
                    nameof(GetAirline),
                    new { id = airline.Id },
                    new
                    {
                        airline.Id,
                        airline.Name,
                        airline.Iatacode,
                        airline.Icaocode
                    });
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        //consulta nivel avanzado
        [HttpGet("{id}")]
        public async Task<ActionResult<AirlineDetailsDto>> GetAirline(int id)
        {
            var airline = await _context.Airlines
                .Where(a => a.Id == id)
                .Select(a => new AirlineDetailsDto
                {
                    Id = a.Id,
                    Iatacode = a.Iatacode,
                    Icaocode = a.Icaocode,
                    Name = a.Name,

                    Countries = a.AirlineCountries.Select(ac => new AirlineCountryDto
                    {
                        CountryId = ac.CountryId,
                        Code = ac.Country.Code,
                        Name = ac.Country.Name,
                        Native = ac.Native
                    }).ToList(),

                    TotalFlights = a.Flights.Count(),

                    CancelledFlights = a.Flights.Count(f => f.Cancelled),

                    DivertedFlights = a.Flights.Count(f => f.Diverted),

                    AverageDepartureDelay = a.Flights
                        .Select(f => (double?)f.DepDelayMin)
                        .Average(),

                    TotalReviews = a.AirlineReviews.Count(),

                    AverageRating = a.AirlineReviews
                        .Select(r => (double?)r.OverallRating)
                        .Average(),

                    RecommendedReviews = a.AirlineReviews
                        .Count(r => r.Recommended)
                })
                .FirstOrDefaultAsync();

            if (airline == null)
            {
                return NotFound("Airline not found");
            }

            return Ok(airline);
        }

    }
}
