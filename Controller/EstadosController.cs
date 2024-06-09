using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.Estado;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoGetDTO>>> GetEstados()
        {
            var estadoList = await context.Estados.ToListAsync();
            var estadosDto = mapper.Map<IEnumerable<EstadoGetDTO>>(estadoList);
            return Ok(estadosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoGetDTO>> GetEstado(int id)
        {
            var estado = await context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            var estadoDto = mapper.Map<EstadoGetDTO>(estado);
            return estadoDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, EstadoUpdateDTO estadoDto)
        {
            if (id != estadoDto.IdEstado)
            {
                return BadRequest();
            }

            var estado = mapper.Map<Estado>(estadoDto);
            context.Entry(estado).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EstadoExists(id))
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
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            context.Estados.Add(estado);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.IdEstado }, estado);
        }

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

        private async Task<bool> EstadoExists(int id)
        {
            return await context.Estados.AnyAsync(e => e.IdEstado == id);
        }
    }
}
