using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.Doctor;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;

        public DoctoresController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Doctores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorGetDTO>>> GetDoctores()
        {
            var doctorList = await context.Doctores.ToListAsync();
            var doctoresDto = mapper.Map<IEnumerable<DoctorGetDTO>>(doctorList);
            return Ok(doctoresDto);
        }

        // GET: api/Doctores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorGetDTO>> GetDoctor(int id)
        {
            var doctor = await context.Doctores.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var doctorDto = mapper.Map<DoctorGetDTO>(doctor);
            return doctorDto;
        }

        // PUT: api/Doctores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctore(int id, Doctore doctore)
        {
            if (id != doctore.IdDoctor)
            {
                return BadRequest();
            }

            context.Entry(doctore).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DoctoreExists(id))
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

        // POST: api/Doctores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctore>> PostDoctore(Doctore doctore)
        {
            context.Doctores.Add(doctore);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetDoctore", new { id = doctore.IdDoctor }, doctore);
        }

        // DELETE: api/Doctores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctore(int id)
        {
            var doctore = await context.Doctores.FindAsync(id);
            if (doctore == null)
            {
                return NotFound();
            }

            context.Doctores.Remove(doctore);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> DoctoreExists(int id)
        {
            return await context.Doctores.AnyAsync(e => e.IdDoctor == id);
        }
    }
}
