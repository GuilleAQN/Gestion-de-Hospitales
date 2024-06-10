using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Controller;
using Primer_Parcial.DTOs.Cita;
using Primer_Parcial.DTOs.Departamento;
using Primer_Parcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_de_Hospitales.UnitTest
{
    public class DepartamentoControllerTest
    {
        private DepartamentosController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public DepartamentoControllerTest()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new DepartamentosController(_fixture.Context, _fixture.Mapper);
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

            _fixture.Context.Departamentos.AddRange(departamentos);
            _fixture.Context.SaveChanges();
        }

        [Fact]
        public async Task GetDepartamentos_ReturnsOkResult()
        {
            // Arrange
            Setup();

            // Act
            var result = await _controller.GetDepartamentos();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            // Verificar que el valor devuelto no es nulo
            Assert.NotNull(result.Result);

            // Verificar los datos devueltos
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var departamentos = okResult.Value as IEnumerable<DepartamentoGetDTO>;
            Assert.NotNull(departamentos);

            Assert.Equal(2, departamentos.Count()); // Verificar que se devuelvan todas las habitaciones
        }

        [Fact]
        public async Task GetDepartamento_ReturnsOkResult_WithValidId()
        {
            // Arrange
            Setup();
            var departamento = new Departamento
            {
                IdDepartamento = 2,
                Nombre = "Cardiología",
                Descripcion = "Departamento de Cardiología",
                Ubicación = "Edificio A, Planta 2",
                Telefono = "555-1234"
            };

            // Act
            var result = await _controller.GetDepartamento(2);

            // Assert
            var departamentoDto = Assert.IsType<DepartamentoGetDTO>(result.Value);
            Assert.Equal(departamento.IdDepartamento, departamentoDto.IdDepartamento);
        }

        [Fact]
        public async Task PostDepartamentos_ReturnsOkResult_WithValidData()
        {
            // Arrange
            Setup();
            var departamentoDto = new DepartamentoInsertDTO
            {
                Nombre = "Cardiología",
                Descripcion = "Departamento de Cardiología",
                Ubicación = "Edificio A, Planta 2",
                Telefono = "555-1234"
            };

            // Act
            var result = await _controller.PostDepartamento(departamentoDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async Task PutDepartamento_ReturnsNoContent_WithValidId()
        {
            // Arrange
            Setup();
            var departamentoDto = new DepartamentoInsertDTO
            {
                Nombre = "El mejorlogia",
                Descripcion = "Departamento de Cardiología",
                Ubicación = "Edificio A, Planta 2",
                Telefono = "555-1234"
            };

            await _controller.PostDepartamento(departamentoDto);

            // Desatachar la entidad que se acaba de insertar para evitar conflictos
            var insertedDepartamento = _fixture.Context.Departamentos.Local.FirstOrDefault(h => h.IdDepartamento == 3);
            if (insertedDepartamento != null)
            {
                _fixture.Context.Entry(insertedDepartamento).State = EntityState.Detached;
            }

            var departamentoUpdateDto = new DepartamentoUpdateDTO
            {
                IdDepartamento = 3,
                Nombre = "Tontologia",
                Descripcion = "Departamento de Tontologia",
                Ubicación = "Edificio A, Planta 2",
                Telefono = "555-1234"
            };

            // Act
            var result = await _controller.PutDepartamento(3, departamentoUpdateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task DeleteDepartamento_ReturnsNoContent_WithValidId()
        {
            // Act
            Setup();
            var result = await _controller.DeleteDepartamento(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
