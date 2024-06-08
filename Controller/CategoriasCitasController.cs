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
    public class CategoriasCitasController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public CategoriasCitasController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoriasCitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriasCita>>> GetCategoriasCitas()
        {
            return await _context.CategoriasCitas.ToListAsync();
        }

        // GET: api/CategoriasCitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriasCita>> GetCategoriasCita(int id)
        {
            var categoriasCita = await _context.CategoriasCitas.FindAsync(id);

            if (categoriasCita == null)
            {
                return NotFound();
            }

            return categoriasCita;
        }

        // PUT: api/CategoriasCitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriasCita(int id, CategoriasCita categoriasCita)
        {
            if (id != categoriasCita.IdCategoriaCita)
            {
                return BadRequest();
            }

            _context.Entry(categoriasCita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriasCitaExists(id))
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

        // POST: api/CategoriasCitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriasCita>> PostCategoriasCita(CategoriasCita categoriasCita)
        {
            _context.CategoriasCitas.Add(categoriasCita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriasCita", new { id = categoriasCita.IdCategoriaCita }, categoriasCita);
        }

        // DELETE: api/CategoriasCitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriasCita(int id)
        {
            var categoriasCita = await _context.CategoriasCitas.FindAsync(id);
            if (categoriasCita == null)
            {
                return NotFound();
            }

            _context.CategoriasCitas.Remove(categoriasCita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriasCitaExists(int id)
        {
            return _context.CategoriasCitas.Any(e => e.IdCategoriaCita == id);
        }
    }
}
