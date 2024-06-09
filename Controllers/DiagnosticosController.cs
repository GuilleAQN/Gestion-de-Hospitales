using AutoMapper;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosticoGetDTO>>> GetDiagnosticos()
        {
            var diagnosticoList = await context.Diagnosticos.ToListAsync();
            var diagnosticosDto = mapper.Map<IEnumerable<DiagnosticoGetDTO>>(diagnosticoList);
            return Ok(diagnosticosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosticoGetDTO>> GetDiagnostico(int id)
        {
            var diagnostico = await context.Diagnosticos.FindAsync(id);

            if (diagnostico == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Diagnóstico no encontrado",
                    Detail = $"No se encontró un diagnóstico con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            var diagnosticoDto = mapper.Map<DiagnosticoGetDTO>(diagnostico);
            return diagnosticoDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnostico(int id, DiagnosticoUpdateDTO diagnosticoDto)
        {

            if (id != diagnosticoDto.IdDiagnostico)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "ID no coincide",
                    Detail = "El ID proporcionado no coincide con el ID del diagnóstico.",
                    Instance = HttpContext.Request.Path
                });
            }

            var diagnostico = mapper.Map<Diagnostico>(diagnosticoDto);
            context.Entry(diagnostico).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DiagnosticoExists(id))
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Diagnóstico no encontrado",
                        Detail = $"No se encontró un diagnóstico con el ID {id}.",
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
        public async Task<ActionResult<Diagnostico>> PostDiagnostico(DiagnosticoInsertDTO diagnosticoDto)
        {
            var diagnostico = mapper.Map<Diagnostico>(diagnosticoDto);
            context.Diagnosticos.Add(diagnostico);
            await context.SaveChangesAsync();

            return Ok(diagnostico.IdDiagnostico);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnostico(int id)
        {
            var diagnostico = await context.Diagnosticos.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Diagnóstico no encontrado",
                    Detail = $"No se encontró un diagnóstico con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
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
