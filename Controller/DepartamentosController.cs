using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.Departamento;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;

        public DepartamentosController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartamentoGetDTO>>> GetDepartamentos()
        {
            var departamentoList = await context.Departamentos.ToListAsync();
            var departamentosDto = mapper.Map<IEnumerable<DepartamentoGetDTO>>(departamentoList);
            return Ok(departamentosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoGetDTO>> GetDepartamento(int id)
        {
            var departamento = await context.Departamentos.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            var departamentoDto = mapper.Map<DepartamentoGetDTO>(departamento);
            return departamentoDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, DepartamentoUpdateDTO departamentoDto)
        {
            if (id != departamentoDto.IdDepartamento)
            {
                return BadRequest();
            }

            var departamento = mapper.Map<Departamento>(departamentoDto);
            context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DepartamentoExists(id))
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
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        {
            context.Departamentos.Add(departamento);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetDepartamento", new { id = departamento.IdDepartamento }, departamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            var departamento = await context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            context.Departamentos.Remove(departamento);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> DepartamentoExists(int id)
        {
            return await context.Departamentos.AnyAsync(e => e.IdDepartamento == id);
        }
    }
}
