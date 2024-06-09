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
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Estado no encontrado",
                    Detail = $"No se encontró un estado con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            var estadoDto = mapper.Map<EstadoGetDTO>(estado);
            return estadoDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, EstadoUpdateDTO estadoDto)
        {
            if (id != estadoDto.IdEstado)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "ID no coincide",
                    Detail = "El ID proporcionado no coincide con el ID del estado.",
                    Instance = HttpContext.Request.Path
                });
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
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Estado no encontrado",
                        Detail = $"No se encontró un estado con el ID {id}.",
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
        public async Task<ActionResult<Estado>> PostEstado(EstadoInsertDTO estadoDto)
        {
            var estado = mapper.Map<Estado>(estadoDto);
            context.Estados.Add(estado);
            await context.SaveChangesAsync();

            return Ok(estado.IdEstado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Estado no encontrado",
                    Detail = $"No se encontró un estado con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
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
