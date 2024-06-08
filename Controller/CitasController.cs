using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;

        public CitasController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
        {
            return await context.Citas.ToListAsync();
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await context.Citas.FindAsync(id);

            if (cita == null)
            {
                return NotFound();
            }

            return cita;
        }

        // PUT: api/Citas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita cita)
        {
            if (id != cita.IdCita)
            {
                return BadRequest();
            }

            context.Entry(cita).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
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

        // POST: api/Citas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita(Cita cita)
        {
            context.Citas.Add(cita);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCita", new { id = cita.IdCita }, cita);
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            context.Citas.Remove(cita);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitaExists(int id)
        {
            return context.Citas.Any(e => e.IdCita == id);
        }
    }
}
