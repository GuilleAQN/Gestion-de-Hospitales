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
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Paciente no encontrado",
                    Detail = $"No se encontró un paciente con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            var pacienteDto = mapper.Map<PacienteGetDTO>(paciente);
            return pacienteDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, PacienteUpdateDTO pacienteDto)
        {
            if (id != pacienteDto.IdPaciente)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "ID no coincide",
                    Detail = "El ID proporcionado no coincide con el ID del paciente.",
                    Instance = HttpContext.Request.Path
                });
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
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Paciente no encontrado",
                        Detail = $"No se encontró un paciente con el ID {id}.",
                        Instance = HttpContext.Request.Path
                    });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(PacienteInsertDTO pacienteDto)
        {
            var paciente = mapper.Map<Paciente>(pacienteDto);

            if (await PacienteExists(paciente?.Cedula))
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Cédula no coincide",
                    Detail = "La cédula proporcionada no coincide con la cédula del paciente.",
                    Instance = HttpContext.Request.Path
                });
            }

            context.Pacientes.Add(paciente);
            await context.SaveChangesAsync();

            return Ok(paciente.IdPaciente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Paciente no encontrado",
                    Detail = $"No se encontró un paciente con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
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
