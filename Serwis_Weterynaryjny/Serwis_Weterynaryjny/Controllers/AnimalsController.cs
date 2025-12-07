using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weterynaria.Data;

namespace VetClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PetsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPets([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Podaj fragment nazwy.");

            var pets = await _context.Animals
                .Include(p => p.Owner)
                .Where(p => p.Name.ToLower().StartsWith(name.ToLower()))
                .ToListAsync();

            return Ok(pets);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetPetHistory(int id)
        {
            var history = await _context.Visits
                .Include(a => a.ServiceType)
                .Where(a => a.AnimalId == id)
                .Select(a => new
                {
                    Date = a.VisitDate,
                    Procedure = a.ServiceType != null ? a.ServiceType.Name : "Nieznana",
                    Description = a.Description,
                    Status = a.IsConfirmed ? "Potwierdzona" : "Oczekująca"
                })
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            if (!history.Any()) return NotFound("Brak historii leczenia dla tego zwierzaka.");

            return Ok(history);
        }
    }
}