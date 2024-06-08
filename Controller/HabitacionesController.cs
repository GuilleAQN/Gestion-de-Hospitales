using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Habitaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitacionGetDTO>>> GetHabitaciones()
        {
            var habitacionList = await context.Habitaciones.ToListAsync();
            var habitacionesDto = mapper.Map<IEnumerable<HabitacionGetDTO>>(habitacionList);
            return Ok(habitacionesDto);
        }

        // GET: api/Habitaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Habitacione>> GetHabitacione(int id)
        {
            var habitacione = await context.Habitaciones.FindAsync(id);

            if (habitacione == null)
            {
                return NotFound();
            }

            return habitacione;
        }

        // PUT: api/Habitaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitacione(int id, HabitacionUpdateDTO habitacionDto)
        {
            var habitacion = mapper.Map<Habitacione>(habitacionDto);

            if (id != habitacion.IdHabitacion)
            {
                return BadRequest();
            }

            context.Entry(habitacion).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HabitacioneExists(id))
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

        // POST: api/Habitaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Habitacione>> PostHabitacione(HabitacionInsertDTO habitacionDto)
        {
            var habitacion = mapper.Map<Habitacione>(habitacionDto);

            context.Habitaciones.Add(habitacion);
            await context.SaveChangesAsync();

            return Ok(habitacion.IdHabitacion);
        }

        // DELETE: api/Habitaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitacione(int id)
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

        private async Task<bool> HabitacioneExists(int id)
        {
            return await context.Habitaciones.AnyAsync(e => e.IdHabitacion == id);
        }
    }
}
