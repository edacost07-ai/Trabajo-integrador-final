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
    public class RegionsController : ControllerBase
    {
        private readonly AirportDbContext _context;

        public RegionsController(AirportDbContext context)
        {
            _context = context;
        }
        //consulta nivel intermedio
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto dto)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(c => c.Id == dto.CountryId);

            if (country == null)
            {
                return NotFound("El país indicado no existe.");
            }

            var existingRegion = await _context.Regions
                .AnyAsync(r => r.Code == dto.Code);

            if (existingRegion)
            {
                return Conflict("Ya existe una región con ese código.");
            }

            var region = new Region
            {
                CountryId = dto.CountryId,
                Code = dto.Code,
                LocalCode = dto.LocalCode,
                Name = dto.Name,
                Keywords = dto.Keywords,
                WikipediaLink = dto.WikipediaLink
            };

            _context.Regions.Add(region);

            await _context.SaveChangesAsync();

            var result = new RegionListDto
            {
                Id = region.Id,
                CountryId = region.CountryId,
                Code = region.Code,
                LocalCode = region.LocalCode,
                Name = region.Name,
                Keywords = region.Keywords,
                WikipediaLink = region.WikipediaLink,
                AirportsCount = 0
            };

            return StatusCode(201, result);
        }
    }
}

