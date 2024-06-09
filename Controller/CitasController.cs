using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.Cita;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaGetDTO>>> GetCitas()
        {
            var citaList = await context.Citas.ToListAsync();
            var citasDto = mapper.Map<IEnumerable<CitaGetDTO>>(citaList);
            return Ok(citasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitaGetDTO>> GetCita(int id)
        {
            var cita = await context.Citas.FindAsync(id);

            if (cita == null)
            {
                return NotFound();
            }

            var citaDto = mapper.Map<CitaGetDTO>(cita);
            return citaDto;
        }

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
                if (!await CitaExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita(Cita cita)
        {
            context.Citas.Add(cita);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCita", new { id = cita.IdCita }, cita);
        }

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

        private async Task<bool> CitaExists(int id)
        {
            return await context.Citas.AnyAsync(e => e.IdCita == id);
        }
    }
}
