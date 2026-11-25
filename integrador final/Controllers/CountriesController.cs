using integrador_final.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using integrador_final.Dtos.Responses;
using integrador_final.Dtos.Request;
using Microsoft.Identity.Client;

namespace integrador_final.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly AirportDbContext _context;
        public CountriesController(AirportDbContext context)
        {
            _context = context;
        }
        //Consuta nivel intermedio
        [HttpGet]
        public async Task<ActionResult<List<CountryListDto>>> GetCountries([FromQuery]PageRequest pageRequest, string? name)
        {


            var countries = await (from c in _context.Countries
                                   orderby c.Id
                                   where string.IsNullOrEmpty(name) || c.Name.StartsWith(name)
                                   select new CountryListDto
                                   {
                                       Id = c.Id,
                                       Code = c.Code,
                                       Name = c.Name,
                                       Continent = c.Continent,
                                       Keywords = c.Keywords,
                                       WikipediaLink = c.WikipediaLink
                                   })
                                   .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
                                   .Take(pageRequest.PageSize)
                                   .ToListAsync();

            return Ok(countries);
        }

    }
}
