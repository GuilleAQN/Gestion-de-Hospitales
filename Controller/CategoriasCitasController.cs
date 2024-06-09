using AutoMapper;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaCitaGetDTO>>> GetCategoriasCitas()
        {
            var categoriaCitaList = await context.CategoriasCitas.ToListAsync();
            var categoriasCitasDto = mapper.Map<IEnumerable<CategoriaCitaGetDTO>>(categoriaCitaList);
            return Ok(categoriasCitasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaCitaGetDTO>> GetCategoriasCita(int id)
        {
            var categoriaCita = await context.CategoriasCitas.FindAsync(id);

            if (categoriaCita == null)
            {
                return NotFound();
            }

            var categoriaCitaDto = mapper.Map<CategoriaCitaGetDTO>(categoriaCita);
            return categoriaCitaDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriasCita(int id, CategoriaCitaUpdateDTO categoriasCitaDto)
        {
            if (id != categoriasCitaDto.IdCategoriaCita)
            {
                return BadRequest();
            }

            var categoriasCita = mapper.Map<CategoriasCita>(categoriasCitaDto);
            context.Entry(categoriasCita).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoriasCitaExists(id))
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
        public async Task<ActionResult<CategoriasCita>> PostCategoriasCita(CategoriaCitaInsertDTO categoriasCitaDto)
        {
            var categoriasCita = mapper.Map<CategoriasCita>(categoriasCitaDto);
            context.CategoriasCitas.Add(categoriasCita);
            await context.SaveChangesAsync();

            return Ok(categoriasCita.IdCategoriaCita);
        }

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

        private async Task<bool> CategoriasCitaExists(int id)
        {
            return await context.CategoriasCitas.AnyAsync(e => e.IdCategoriaCita == id);
        }
    }
}
