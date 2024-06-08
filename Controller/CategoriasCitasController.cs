using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.CategoriasCita;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasCitasController : ControllerBase
    {
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;

        public CategoriasCitasController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/CategoriasCitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaCitaGetDTO>>> GetCategoriasCitas()
        {
            var categoriaCitaList = await context.CategoriasCitas.ToListAsync();
            var categoriasCitasDto = mapper.Map<IEnumerable<CategoriaCitaGetDTO>>(categoriaCitaList);
            return Ok(categoriasCitasDto);
        }

        // GET: api/CategoriasCitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriasCita>> GetCategoriasCita(int id)
        {
            var categoriasCita = await context.CategoriasCitas.FindAsync(id);

            if (categoriasCita == null)
            {
                return NotFound();
            }

            return categoriasCita;
        }

        // PUT: api/CategoriasCitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriasCita(int id, CategoriasCita categoriasCita)
        {
            if (id != categoriasCita.IdCategoriaCita)
            {
                return BadRequest();
            }

            context.Entry(categoriasCita).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriasCitaExists(id))
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

        // POST: api/CategoriasCitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriasCita>> PostCategoriasCita(CategoriasCita categoriasCita)
        {
            context.CategoriasCitas.Add(categoriasCita);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriasCita", new { id = categoriasCita.IdCategoriaCita }, categoriasCita);
        }

        // DELETE: api/CategoriasCitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriasCita(int id)
        {
            var categoriasCita = await context.CategoriasCitas.FindAsync(id);
            if (categoriasCita == null)
            {
                return NotFound();
            }

            context.CategoriasCitas.Remove(categoriasCita);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriasCitaExists(int id)
        {
            return context.CategoriasCitas.Any(e => e.IdCategoriaCita == id);
        }
    }
}
