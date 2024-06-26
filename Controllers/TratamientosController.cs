﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.DTOs.Tratamiento;
using Primer_Parcial.Models;

namespace Primer_Parcial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientosController : ControllerBase
    {
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;

        public TratamientosController(HospitalDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TratamientoGetDTO>>> GetTratamientos()
        {
            var tratamientoList = await context.Tratamientos.ToListAsync();
            var tratamientosDto = mapper.Map<IEnumerable<TratamientoGetDTO>>(tratamientoList);
            return Ok(tratamientosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TratamientoGetDTO>> GetTratamiento(int id)
        {
            var tratamiento = await context.Tratamientos.FindAsync(id);

            if (tratamiento == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Tratamiento no encontrado",
                    Detail = $"No se encontró un tratamiento con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            var tratamientoDto = mapper.Map<TratamientoGetDTO>(tratamiento);
            return tratamientoDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratamiento(int id, TratamientoUpdateDTO tratamientoDto)
        {
            if (id != tratamientoDto.IdTratamiento)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "ID no coincide",
                    Detail = "El ID proporcionado no coincide con el ID del tratamiento.",
                    Instance = HttpContext.Request.Path
                });
            }

            var tratamiento = mapper.Map<Tratamiento>(tratamientoDto);
            context.Entry(tratamiento).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TratamientoExists(id))
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Tratamiento no encontrado",
                        Detail = $"No se encontró un tratamiento con el ID {id}.",
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
        public async Task<ActionResult<Tratamiento>> PostTratamiento(TratamientoInsertDTO tratamientoDto)
        {
            var tratamiento = mapper.Map<Tratamiento>(tratamientoDto);
            context.Tratamientos.Add(tratamiento);
            await context.SaveChangesAsync();

            return Ok(tratamiento.IdTratamiento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratamiento(int id)
        {
            var tratamiento = await context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Tratamiento no encontrado",
                    Detail = $"No se encontró un tratamiento con el ID {id}.",
                    Instance = HttpContext.Request.Path
                });
            }

            context.Tratamientos.Remove(tratamiento);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> TratamientoExists(int id)
        {
            return await context.Tratamientos.AnyAsync(e => e.IdTratamiento == id);
        }
    }
}
