using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Primer_Parcial.Controller;
using Primer_Parcial.DTOs;
using Primer_Parcial.DTOs.Cita;
using Primer_Parcial;
using System;
using System.Threading.Tasks;
using Xunit;
using Primer_Parcial.Models;
using Gestion_de_Hospitales.UnitTest;
using Primer_Parcial.DTOs.CategoriasCita;

namespace Gestion_de_Hospitales
{
    public class CitasControllerTests
    {
        private CitasController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public CitasControllerTests()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new CitasController(_fixture.Context, _fixture.Mapper);
        }

        [Fact]
        public void Setup()
        {
            var pacientes = new List<Paciente>{
                new Paciente
                {
                    IdPaciente = 1,
                    Cedula = "00101010100",
                    NombreCompleto = "Juan Perez",
                    FechaNacimiento = new DateOnly(1990, 1, 1),
                    Genero = "M",
                    Direccion = "Calle Falsa 123",
                    Telefono = "1234567890",
                    CorreoElectronico = "juan.perez@example.com"
                },
                new Paciente
                {
                    IdPaciente = 2,
                    Cedula = "00202020200",
                    NombreCompleto = "Maria Rodriguez",
                    FechaNacimiento = new DateOnly(1985, 2, 2),
                    Genero = "Femenino",
                    Direccion = "Avenida Siempre Viva 456",
                    Telefono = "0987654321",
                    CorreoElectronico = "maria.rodriguez@example.com"
                } };

            var departamentos = new List<Departamento>
            {
                new Departamento
                {
                    IdDepartamento = 1,
                    Nombre = "Cardiología",
                    Descripcion = "Departamento de Cardiología",
                    Ubicación = "Edificio A, Planta 2",
                    Telefono = "555-1234"
                },
                new Departamento
                {
                    IdDepartamento = 2,
                    Nombre = "Neurología",
                    Descripcion = "Departamento de Neurología",
                    Ubicación = "Edificio B, Planta 3",
                    Telefono = "555-5678"
                }
            };

            var doctores = new List<Doctore>
            {
                new Doctore
                {
                    IdDoctor = 1,
                    Cedula = "00101010100",
                    NombreCompleto = "Dr. Luis Gomez",
                    FechaContratacion = new DateOnly(2015, 3, 3),
                    Direccion = "Boulevard de los Médicos 789",
                    Telefono = "1112223334",
                    CorreoElectronico = "luis.gomez@example.com",
                    IdEspecialidad = 1,
                    IdDepartamento = 1
                },
                new Doctore
                {
                    IdDoctor = 2,
                    Cedula = "00202020200",
                    NombreCompleto = "Dra. Ana Lopez",
                    FechaContratacion = new DateOnly(2018, 4, 4),
                    Direccion = "Calle de la Salud 101",
                    Telefono = "2223334445",
                    CorreoElectronico = "ana.lopez@example.com",
                    IdEspecialidad = 2,
                    IdDepartamento = 2
                }
            };

            var enfermeras = new List<Enfermera>
            {
                new Enfermera
                {
                    IdEnfermera = 1,
                    Cedula = "00101010100",
                    NombreCompleto = "Enfermera Carla Sanchez",
                    FechaContratacion = new DateOnly(2020, 5, 5),
                    Direccion = "Plaza de la Salud 202",
                    Telefono = "3334445556",
                    CorreoElectronico = "carla.sanchez@example.com",
                    IdDepartamento = 1
                },
                new Enfermera
                {
                    IdEnfermera = 2,
                    Cedula = "00202020200",
                    NombreCompleto = "Enfermero Pedro Jimenez",
                    FechaContratacion = new DateOnly(2019, 6, 6),
                    Direccion = "Avenida Enfermeria 303",
                    Telefono = "4445556667",
                    CorreoElectronico = "pedro.jimenez@example.com",
                    IdDepartamento = 2
                }
            };

            var categoriasCitas = new List<CategoriasCita>
            {
                new CategoriasCita
                {
                    IdCategoriaCita = 1,
                    Nombre = "Consulta General",
                    Descripcion = "Consulta médica general"
                },
                new CategoriasCita
                {
                    IdCategoriaCita = 2,
                    Nombre = "Especialidad",
                    Descripcion = "Consulta con especialista"
                }
            };

            var citas = new List<Cita>
            {
                new Cita
                {
                    IdCita = 1,
                    IdPaciente = 1,
                    IdDoctor = 1,
                    IdEnfermera = 1,
                    Fecha = DateTime.Now,
                    IdCategoriaCita = 1,
                    Descripción = "Revisión general",
                },
                new Cita
                {
                    IdCita = 2,
                    IdPaciente = 2,
                    IdDoctor = 2,
                    IdEnfermera = 2,
                    Fecha = DateTime.Now.AddDays(1),
                    IdCategoriaCita = 2,
                    Descripción = "Consulta de especialidad",
                }
            };

            _fixture.Context.Citas.AddRange(citas);
            _fixture.Context.SaveChanges();
        }

        [Fact]
        public async Task GetCitas_ReturnsOkResult()
        {
            // Arrange
            Setup();

            // Act
            var result = await _controller.GetCitas();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            // Verificar que el valor devuelto no es nulo
            Assert.NotNull(result.Result);

            // Verificar los datos devueltos
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var cita = okResult.Value as IEnumerable<CitaGetDTO>;
            Assert.NotNull(cita);

            Assert.Equal(2, cita.Count()); // Verificar que se devuelvan todas las habitaciones
        }

        [Fact]
        public async Task GetCita_ReturnsOkResult_WithValidId()
        {
            // Arrange
            Setup();
            var cita = new Cita
            {
                IdCita = 2,
                IdPaciente = 1,
                IdDoctor = 1,
                IdEnfermera = 1,
                Fecha = DateTime.Now,
                IdCategoriaCita = 1,
                Descripción = "Revisión general",
            };

            // Act
            var result = await _controller.GetCita(2);

            // Assert
            var citaDto = Assert.IsType<CitaGetDTO>(result.Value);
            Assert.Equal(cita.IdCita, citaDto.IdCita);
        }

        [Fact]
        public async Task PostCitas_ReturnsOkResult_WithValidData()
        {
            // Arrange
            Setup();
            var citaDto = new CitaInsertDTO
            {
                IdPaciente = 1,
                IdDoctor = 1,
                IdEnfermera = 1,
                Fecha = DateTime.Now,
                IdCategoriaCita = 1,
                Descripción = "Revisión general",
            };

            // Act
            var result = await _controller.PostCita(citaDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async Task PutCitas_ReturnsNoContent_WithValidId()
        {
            // Arrange
            Setup();
            var citaDto = new CitaInsertDTO
            {
                IdPaciente = 1,
                IdDoctor = 1,
                IdEnfermera = 1,
                Fecha = DateTime.Now,
                IdCategoriaCita = 1,
                Descripción = "Revisión general",
            };

            await _controller.PostCita(citaDto);

            // Desatachar la entidad que se acaba de insertar para evitar conflictos
            var insertedCita = _fixture.Context.Citas.Local.FirstOrDefault(h => h.IdCita == 3);
            if (insertedCita != null)
            {
                _fixture.Context.Entry(insertedCita).State = EntityState.Detached;
            }

            var citaUpdateDto = new CitaUpdateDTO
            {
                IdCita = 3,
                IdPaciente = 1,
                IdDoctor = 1,
                IdEnfermera = 1,
                Fecha = DateTime.Now,
                IdCategoriaCita = 1,
                Descripción = "Revisión general",
            };

            // Act
            var result = await _controller.PutCita(3, citaUpdateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task DeleteCitas_ReturnsNoContent_WithValidId()
        {
            // Act
            Setup();
            var result = await _controller.DeleteCita(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
