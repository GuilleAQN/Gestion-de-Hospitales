using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.Paciente;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;

        public PacientesController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteGetDTO>>> GetPacientes()
        {
            var pacienteList = await context.Pacientes.ToListAsync();
            var pacientesDto = mapper.Map<IEnumerable<PacienteGetDTO>>(pacienteList);
            return Ok(pacientesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteGetDTO>> GetPaciente(int id)
        {
            var paciente = await context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }

            var pacienteDto = mapper.Map<PacienteGetDTO>(paciente);
            return pacienteDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, PacienteUpdateDTO pacienteDto)
        {
            if (id != pacienteDto.IdPaciente)
            {
                return BadRequest();
            }

            var paciente = mapper.Map<Paciente>(pacienteDto);
            context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PacienteExists(id))
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
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            context.Pacientes.Add(paciente);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPaciente", new { id = paciente.IdPaciente }, paciente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            context.Pacientes.Remove(paciente);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PacienteExists(int id)
        {
            return await context.Pacientes.AnyAsync(e => e.IdPaciente == id);
        }

        private async Task<bool> PacienteExists(string cedula)
        {
            return await context.Pacientes.AnyAsync(e => e.Cedula == cedula);
        }
    }
}
