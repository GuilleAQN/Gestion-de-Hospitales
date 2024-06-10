using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Controller;
using Primer_Parcial.DTOs.Enfermera;
using Primer_Parcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_de_Hospitales.UnitTest
{
    public class EnfermerasControllerTest
    {
        private EnfermerasController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public EnfermerasControllerTest()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new EnfermerasController(_fixture.Context, _fixture.Mapper);
        }

        [Fact]
        public void Setup()
        {

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

            var especialidades = new List<Especialidade>
            {
                new Especialidade
                {
                    IdEspecialidad = 1,
                    Nombre = "Cardiología",
                    Descripcion = "Especialidad médica que se encarga del estudio, diagnóstico y tratamiento de las enfermedades del corazón y del aparato circulatorio."
                },
                new Especialidade
                {
                    IdEspecialidad = 2,
                    Nombre = "Dermatología",
                    Descripcion = "Especialidad médica que se encarga del estudio, diagnóstico y tratamiento de las enfermedades de la piel, cabello y uñas."
                }
            };

            var enfermeras = new List<Enfermera>
            {
                new Enfermera
                {
                    IdEnfermera = 1,
                    Cedula = "123456789",
                    NombreCompleto = "María González",
                    FechaContratacion = new DateOnly(2020, 10, 15),
                    Direccion = "Calle Principal 123",
                    Telefono = "555-1234",
                    CorreoElectronico = "maria@example.com",
                    IdDepartamento = 1 // Assuming Department ID 1
                },
                new Enfermera
                {
                    IdEnfermera = 2,
                    Cedula = "987654321",
                    NombreCompleto = "Juan Pérez",
                    FechaContratacion = new DateOnly(2019, 8, 20),
                    Direccion = "Avenida Central 456",
                    Telefono = "555-5678",
                    CorreoElectronico = "juan@example.com",
                    IdDepartamento = 2 // Assuming Department ID 2
                },
                // Add more nurse instances as needed
            };


            _fixture.Context.Enfermeras.AddRange(enfermeras);
            _fixture.Context.SaveChanges();
        }

        [Fact]
        public async Task GetEnfermeras_ReturnsOkResult()
        {
            // Arrange
            Setup();

            // Act
            var result = await _controller.GetEnfermeras();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            // Verificar que el valor devuelto no es nulo
            Assert.NotNull(result.Result);

            // Verificar los datos devueltos
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var enfermeras = okResult.Value as IEnumerable<EnfermeraGetDTO>;
            Assert.NotNull(enfermeras);

            Assert.Equal(2, enfermeras.Count()); // Verificar que se devuelvan todas las habitaciones
        }

        [Fact]
        public async Task PostEnfermera_ReturnsOkResult_WithValidData()
        {
            // Arrange
            Setup();
            var newEnfermeraDto = new EnfermeraInsertDTO
            {
                Cedula = "11298765432",
                NombreCompleto = "Dra. María González",
                FechaContratacion = new DateOnly(2021, 5, 10),
                Direccion = "Avenida Central 456",
                Telefono = "987-654-3210",
                CorreoElectronico = "maria.gonzalez@example.com",
                IdDepartamento = 2
            };

            // Act
            var result = await _controller.PostEnfermera(newEnfermeraDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async Task DeleteEnfermera_ReturnsNoContent_WithValidId()
        {
            // Act
            Setup();
            var result = await _controller.DeleteEnfermera(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
