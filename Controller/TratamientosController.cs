﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientosController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public TratamientosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/Tratamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tratamiento>>> GetTratamientos()
        {
            return await _context.Tratamientos.ToListAsync();
        }

        // GET: api/Tratamientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tratamiento>> GetTratamiento(int id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);

            if (tratamiento == null)
            {
                return NotFound();
            }

            return tratamiento;
        }

        // PUT: api/Tratamientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratamiento(int id, Tratamiento tratamiento)
        {
            if (id != tratamiento.IdTratamiento)
            {
                return BadRequest();
            }

            _context.Entry(tratamiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TratamientoExists(id))
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

        // POST: api/Tratamientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tratamiento>> PostTratamiento(Tratamiento tratamiento)
        {
            _context.Tratamientos.Add(tratamiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTratamiento", new { id = tratamiento.IdTratamiento }, tratamiento);
        }

        // DELETE: api/Tratamientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratamiento(int id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            _context.Tratamientos.Remove(tratamiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TratamientoExists(int id)
        {
            return _context.Tratamientos.Any(e => e.IdTratamiento == id);
        }
    }
}
