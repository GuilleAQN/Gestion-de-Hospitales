using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnfermerasController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public EnfermerasController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/Enfermeras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enfermera>>> GetEnfermeras()
        {
            return await _context.Enfermeras.ToListAsync();
        }

        // GET: api/Enfermeras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enfermera>> GetEnfermera(int id)
        {
            var enfermera = await _context.Enfermeras.FindAsync(id);

            if (enfermera == null)
            {
                return NotFound();
            }

            return enfermera;
        }

        // PUT: api/Enfermeras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnfermera(int id, Enfermera enfermera)
        {
            if (id != enfermera.IdEnfermera)
            {
                return BadRequest();
            }

            _context.Entry(enfermera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnfermeraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Enfermeras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enfermera>> PostEnfermera(Enfermera enfermera)
        {
            _context.Enfermeras.Add(enfermera);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnfermera", new { id = enfermera.IdEnfermera }, enfermera);
        }

        // DELETE: api/Enfermeras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnfermera(int id)
        {
            var enfermera = await _context.Enfermeras.FindAsync(id);
            if (enfermera == null)
            {
                return NotFound();
            }

            _context.Enfermeras.Remove(enfermera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnfermeraExists(int id)
        {
            return _context.Enfermeras.Any(e => e.IdEnfermera == id);
        }
    }
}
