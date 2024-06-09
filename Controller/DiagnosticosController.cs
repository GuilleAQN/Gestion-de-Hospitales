using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.Diagnostico;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {
        private readonly HospitalDbContext context;

        private readonly IMapper mapper;

        public DiagnosticosController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Diagnosticos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosticoGetDTO>>> GetDiagnosticos()
        {
            var diagnosticoList = await context.Diagnosticos.ToListAsync();
            var diagnosticosDto = mapper.Map<IEnumerable<DiagnosticoGetDTO>>(diagnosticoList);
            return Ok(diagnosticosDto);
        }

        // GET: api/Diagnosticos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosticoGetDTO>> GetDiagnostico(int id)
        {
            var diagnostico = await context.Diagnosticos.FindAsync(id);

            if (diagnostico == null)
            {
                return NotFound();
            }

            var diagnosticoDto = mapper.Map<DiagnosticoGetDTO>(diagnostico);
            return diagnosticoDto;
        }

        // PUT: api/Diagnosticos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnostico(int id, DiagnosticoUpdateDTO diagnosticoDto)
        {
            var diagnostico = mapper.Map<Diagnostico>(diagnosticoDto);

            if (id != diagnostico.IdDiagnostico)
            {
                return BadRequest();
            }

            context.Entry(diagnostico).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DiagnosticoExists(id))
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

        // POST: api/Diagnosticos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diagnostico>> PostDiagnostico(Diagnostico diagnostico)
        {
            context.Diagnosticos.Add(diagnostico);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetDiagnostico", new { id = diagnostico.IdDiagnostico }, diagnostico);
        }

        // DELETE: api/Diagnosticos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnostico(int id)
        {
            var diagnostico = await context.Diagnosticos.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            context.Diagnosticos.Remove(diagnostico);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> DiagnosticoExists(int id)
        {
            return await context.Diagnosticos.AnyAsync(e => e.IdDiagnostico == id);
        }
    }
}
