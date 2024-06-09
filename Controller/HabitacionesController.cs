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
                return NotFound();
            }

            var habitacionDto = mapper.Map<HabitacionGetDTO>(habitacion);
            return habitacionDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitacione(int id, HabitacionUpdateDTO habitacionDto)
        {
            if (id != habitacionDto.IdHabitacion)
            {
                return BadRequest();
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
        public async Task<ActionResult<Habitacione>> PostHabitacion(HabitacionInsertDTO habitacionDto)
        {
            var habitacion = mapper.Map<Habitacione>(habitacionDto);

            if (await HabitacionExists(habitacion?.Numero))
            {
                return BadRequest();
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
                return NotFound();
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
