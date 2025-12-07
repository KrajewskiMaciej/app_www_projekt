using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weterynaria.Data;
using Weterynaria.DTO;

namespace Weterynaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetServices()
        {
            return Ok(await _context.ServiceTypes.ToListAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateService([FromBody] ServicesTypes service)
        {
            _context.ServiceTypes.Add(service);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetServices), new { id = service.Id }, service);
        }
    }
}