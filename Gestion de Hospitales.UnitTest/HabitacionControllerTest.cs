using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Controller;
using Primer_Parcial.DTOs.Habitacion;
using Primer_Parcial.Models;
using Xunit; // Agrega el namespace para los atributos de las pruebas unitarias

namespace Gestion_de_Hospitales.UnitTest
{
    public class HabitacionControllerTest
    {
        private HabitacionesController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public HabitacionControllerTest()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new HabitacionesController(_fixture.Context, _fixture.Mapper);
        }

        [Fact]
        public void Setup()
        {
            var estados = new List<Estado>
            {
                new Estado { Nombre = "B" },
                new Estado { Nombre = "C" }
            };
            _fixture.Context.Estados.AddRange(estados);
            _fixture.Context.SaveChanges();

            var habitaciones = new List<Habitacione>
            {
                new Habitacione { Numero = "1", Piso = 1, Tipo = "Tipo A", IdEstado = 1 },
                new Habitacione { Numero = "2", Piso = 2, Tipo = "Tipo B", IdEstado = 2 }
            };
            _fixture.Context.Habitaciones.AddRange(habitaciones);
            _fixture.Context.SaveChanges();
        }

        [Fact]
        public async Task GetHabitaciones_ReturnsOkResult()
        {
            // Arrange
            Setup();

            // Act
            var result = await _controller.GetHabitaciones();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            // Verificar que el valor devuelto no es nulo
            Assert.NotNull(result.Result);

            // Verificar los datos devueltos
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var habitaciones = okResult.Value as IEnumerable<HabitacionGetDTO>;
            Assert.NotNull(habitaciones);

            Assert.Equal(2, habitaciones.Count()); // Verificar que se devuelvan todas las habitaciones
        }

        [Fact]
        public async Task GetHabitacion_ReturnsOkResult_WithValidId()
        {
            // Arrange
            Setup();
            var habitacion = new Habitacione { IdHabitacion = 2, Numero = "101", Piso = 1, Tipo = "Tipo A", IdEstado = 1 };

            // Act
            var result = await _controller.GetHabitacion(2);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var habitacionDto = Assert.IsType<HabitacionGetDTO>(okResult.Value);
            Assert.Equal(habitacion.IdHabitacion, habitacionDto.IdHabitacion);
        }

        [Fact]
        public async Task PostHabitacion_ReturnsOkResult_WithValidData()
        {
            // Arrange
            Setup();
            var habitacionDto = new HabitacionInsertDTO { Numero = "102", Piso = 1, Tipo = "Tipo A", IdEstado = 2 };

            // Act
            var result = await _controller.PostHabitacion(habitacionDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async Task PutHabitacione_ReturnsNoContent_WithValidId()
        {
            // Arrange
            Setup();
            var habitacionInsertDto = new HabitacionInsertDTO { Numero = "103", Piso = 1, Tipo = "Tipo A", IdEstado = 1 };
            await _controller.PostHabitacion(habitacionInsertDto);

            // Desatachar la entidad que se acaba de insertar para evitar conflictos
            var insertedHabitacion = _fixture.Context.Habitaciones.Local.FirstOrDefault(h => h.Numero == "103");
            if (insertedHabitacion != null)
            {
                _fixture.Context.Entry(insertedHabitacion).State = EntityState.Detached;
            }

            var habitacionDto = new HabitacionUpdateDTO { IdHabitacion = 3, Numero = "101", Piso = 1, Tipo = "Tipo A", IdEstado = 1 };

            // Act
            var result = await _controller.PutHabitacione(3, habitacionDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task DeleteHabitacion_ReturnsNoContent_WithValidId()
        {
            // Act
            Setup();
            var result = await _controller.DeleteHabitacion(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
