using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteGetDTO>>> GetPacientes()
        {
            var pacienteList = await context.Pacientes.ToListAsync();
            var pacientesDto = mapper.Map<IEnumerable<PacienteGetDTO>>(pacienteList);
            return Ok(pacientesDto);
        }

        // GET: api/Pacientes/5
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

        // PUT: api/Pacientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
        {
            if (id != paciente.IdPaciente)
            {
                return BadRequest();
            }

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

        // POST: api/Pacientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            context.Pacientes.Add(paciente);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPaciente", new { id = paciente.IdPaciente }, paciente);
        }

        // DELETE: api/Pacientes/5
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
    }
}
