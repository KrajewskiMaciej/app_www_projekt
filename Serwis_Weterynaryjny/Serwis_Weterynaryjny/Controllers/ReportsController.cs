using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weterynaria.Data;

namespace VetClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Tylko Admin widzi finanse
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenue(int year, int month)
        {
            var appointments = await _context.Visits
                .Include(a => a.ServiceType)
                .Where(a => a.VisitDate.Year == year && a.VisitDate.Month == month && a.IsConfirmed)
                .ToListAsync();

            var totalRevenue = appointments.Sum(a => a.ServiceType?.Price ?? 0);
            var count = appointments.Count;

            return Ok(new
            {
                Period = $"{year}-{month:D2}",
                TotalAppointments = count,
                TotalRevenue = totalRevenue,
                Currency = "PLN"
            });
        }
    }
}