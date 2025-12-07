using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weterynaria.Data;
using Weterynaria.DTO;
using System.Security.Claims;

namespace Weterynaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VisitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisitsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> BookVisits([FromBody] CreateVisitDto dto)
        {
            var owner = await _context.Owners.Include(o => o.Animals)
                .FirstOrDefaultAsync(o => o.PhoneNumber == dto.OwnerPhone);

            if (owner == null)
            {
                owner = new Owners { FullName = dto.OwnerName, PhoneNumber = dto.OwnerPhone };
                _context.Owners.Add(owner);
            }

            var Animal = owner.Animals.FirstOrDefault(p => p.Name.ToLower() == dto.AnimalName.ToLower());
            if (Animal == null)
            {
                Animal = new Animals { Name = dto.AnimalName, Species = dto.AnimalSpecies, Owner = owner };
                _context.Animals.Add(Animal);
            }

            var Visits = new Visits
            {
                VisitDate = dto.VisitDate,
                Description = dto.Description,
                Animal = Animal,
                ServiceTypeId = dto.ServicesTypesId,
                IsConfirmed = false
            };

            _context.Visits.Add(Visits);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Wizyta zarezerwowana.", VisitsId = Visits.Id });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllVisits()
        {
            var list = await _context.Visits
                .Include(a => a.Animal).ThenInclude(p => p.Owner)
                .Include(a => a.ServiceType)
                .OrderByDescending(a => a.VisitDate)
                .ToListAsync();
            return Ok(list);
        }

        [HttpPut("{id}/confirm")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmVisits(int id)
        {
            var appt = await _context.Visits.FindAsync(id);
            if (appt == null) return NotFound();

            appt.IsConfirmed = true;
            await _context.SaveChangesAsync();
            return Ok("Wizyta potwierdzona");
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVisits(int id)
        {
            var appt = await _context.Visits.FindAsync(id);
            if (appt == null) return NotFound();

            _context.Visits.Remove(appt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}