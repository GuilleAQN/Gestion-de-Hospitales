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
        private readonly HospitalDbContext _context;

        public IMapper mapper { get; }

        public HabitacionesController(HospitalDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Habitaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitacione>>> GetHabitaciones()
        {
            return await _context.Habitaciones.ToListAsync();
        }

        // GET: api/Habitaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Habitacione>> GetHabitacione(int id)
        {
            var habitacione = await _context.Habitaciones.FindAsync(id);

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

            _context.Entry(habitacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacioneExists(id))
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

            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();

            return Ok(habitacion.IdHabitacion);
        }

        // DELETE: api/Habitaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitacione(int id)
        {
            var habitacione = await _context.Habitaciones.FindAsync(id);
            if (habitacione == null)
            {
                return NotFound();
            }

            _context.Habitaciones.Remove(habitacione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HabitacioneExists(int id)
        {
            return _context.Habitaciones.Any(e => e.IdHabitacion == id);
        }
    }
}
