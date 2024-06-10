using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.Habitacion;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionesController : ControllerBase
    {
        private readonly HospitalDbContext context;

        private readonly IMapper mapper;

        public HabitacionesController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitacionGetDTO>>> GetHabitaciones()
        {
            var habitacionList = await context.Habitaciones.ToListAsync();
            var habitacionesDto = mapper.Map<IEnumerable<HabitacionGetDTO>>(habitacionList);
            return Ok(habitacionesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HabitacionGetDTO>> GetHabitacion(int id)
        {
            var habitacion = await context.Habitaciones.FindAsync(id);

            if (habitacion == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Habitación no encontrada",
                    Detail = $"No se encontró una habitación con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            var habitacionDto = mapper.Map<HabitacionGetDTO>(habitacion);
            return Ok(habitacionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitacione(int id, HabitacionUpdateDTO habitacionDto)
        {
            if (id != habitacionDto.IdHabitacion)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "ID no coincide",
                    Detail = "El ID proporcionado no coincide con el ID de la habitación.",
                    Instance = HttpContext.Request.Path
                });
            }

            var habitacion = mapper.Map<Habitacione>(habitacionDto);
            context.Entry(habitacion).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HabitacionExists(id))
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Habitación no encontrada",
                        Detail = $"No se encontró una habitación con el ID {id}.",
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
        public async Task<ActionResult<Habitacione>> PostHabitacion(HabitacionInsertDTO habitacionDto)
        {
            var habitacion = mapper.Map<Habitacione>(habitacionDto);

            if (await HabitacionExists(habitacion?.Numero))
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Número de Habitación no coincide",
                    Detail = "El número de habitación proporcionado no coincide con el número de habitación dado.",
                    Instance = HttpContext.Request.Path
                });
            }

            context.Habitaciones.Add(habitacion);
            await context.SaveChangesAsync();

            return Ok(habitacion.IdHabitacion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitacion(int id)
        {
            var habitacione = await context.Habitaciones.FindAsync(id);
            if (habitacione == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Habitación no encontrada",
                    Detail = $"No se encontró una habitación con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            context.Habitaciones.Remove(habitacione);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> HabitacionExists(int id)
        {
            return await context.Habitaciones.AnyAsync(e => e.IdHabitacion == id);
        }

        private async Task<bool> HabitacionExists(string numeroHabitacion)
        {
            return await context.Habitaciones.AnyAsync(e => e.Numero == numeroHabitacion);
        }
    }
}
