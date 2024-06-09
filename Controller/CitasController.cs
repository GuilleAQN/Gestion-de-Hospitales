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
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Cita no encontrada",
                    Detail = $"No se encontró una cita con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            var citaDto = mapper.Map<CitaGetDTO>(cita);
            return citaDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, CitaUpdateDTO citaDto)
        {
            if (id != citaDto.IdCita)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "ID no coincide",
                    Detail = "El ID proporcionado no coincide con el ID de la cita.",
                    Instance = HttpContext.Request.Path
                });
            }

            var cita = mapper.Map<Cita>(citaDto);
            context.Entry(cita).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CitaExists(id))
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Cita no encontrada",
                        Detail = $"No se encontró una cita con el ID {id}.",
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
        public async Task<ActionResult<Cita>> PostCita(CitaInsertDTO citaDto)
        {
            var cita = mapper.Map<Cita>(citaDto);
            context.Citas.Add(cita);
            await context.SaveChangesAsync();

            return Ok(cita.IdCita);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Cita no encontrada",
                    Detail = $"No se encontró una cita con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
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
