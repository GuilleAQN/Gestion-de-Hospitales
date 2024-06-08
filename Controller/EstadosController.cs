using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;

        public EstadosController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Estados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            return await context.Estados.ToListAsync();
        }

        // GET: api/Estados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            var estado = await context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        // PUT: api/Estados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estado)
        {
            if (id != estado.IdEstado)
            {
                return BadRequest();
            }

            context.Entry(estado).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
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

        // POST: api/Estados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            context.Estados.Add(estado);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.IdEstado }, estado);
        }

        // DELETE: api/Estados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            context.Estados.Remove(estado);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoExists(int id)
        {
            return context.Estados.Any(e => e.IdEstado == id);
        }
    }
}
