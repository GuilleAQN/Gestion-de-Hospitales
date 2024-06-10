using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Controller;
using Primer_Parcial.DTOs.CategoriasCita;
using Primer_Parcial.Models;

namespace Gestion_de_Hospitales.UnitTest
{
    public class CategoriaCitaControllerTest
    {
        private CategoriasCitasController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public CategoriaCitaControllerTest()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new CategoriasCitasController(_fixture.Context, _fixture.Mapper);
        }

        [Fact]
        public void Setup()
        {
            var categoriasCitas = new List<CategoriasCita>
            {
                new CategoriasCita { IdCategoriaCita = 1, Nombre = "Categoria 1", Descripcion = "Descripcion 1" },
                new CategoriasCita { IdCategoriaCita = 2, Nombre = "Categoria 1", Descripcion = "Descripcion 2" }
            };
            _fixture.Context.CategoriasCitas.AddRange(categoriasCitas);
            _fixture.Context.SaveChanges();
        }

        [Fact]
        public async Task GetCategoriasCitas_ReturnsOkResult()
        {
            // Arrange
            Setup();

            // Act
            var result = await _controller.GetCategoriasCitas();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            // Verificar que el valor devuelto no es nulo
            Assert.NotNull(result.Result);

            // Verificar los datos devueltos
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var categoriasCita = okResult.Value as IEnumerable<CategoriaCitaGetDTO>;
            Assert.NotNull(categoriasCita);

            Assert.Equal(2, categoriasCita.Count()); // Verificar que se devuelvan todas las habitaciones
        }

        [Fact]
        public async Task GetCategoriaCita_ReturnsOkResult_WithValidId()
        {
            // Arrange
            Setup();
            var categoriaCita = new CategoriasCita { IdCategoriaCita = 2, Nombre = "Categoria 1", Descripcion = "Descripcion 1" };

            // Act
            var result = await _controller.GetCategoriasCita(2);

            // Assert
            var categoriaCitaDto = Assert.IsType<CategoriaCitaGetDTO>(result.Value);
            Assert.Equal(categoriaCita.IdCategoriaCita, categoriaCitaDto.IdCategoriaCita);
        }

        [Fact]
        public async Task PostCategoriasCitas_ReturnsOkResult_WithValidData()
        {
            // Arrange
            Setup();
            var categoriaCitaDto = new CategoriaCitaInsertDTO { Nombre = "Categoria 1", Descripcion = "Descripcion 1" };

            // Act
            var result = await _controller.PostCategoriasCita(categoriaCitaDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async Task PutCategoriasCitas_ReturnsNoContent_WithValidId()
        {
            // Arrange
            Setup();
            var categoriaCitaDto = new CategoriaCitaInsertDTO { Nombre = "Categoria 3", Descripcion = "Descripcion 1" };
            await _controller.PostCategoriasCita(categoriaCitaDto);

            // Desatachar la entidad que se acaba de insertar para evitar conflictos
            var insertedCategoriaCita = _fixture.Context.CategoriasCitas.Local.FirstOrDefault(h => h.Nombre == "Categoria 3");
            if (insertedCategoriaCita != null)
            {
                _fixture.Context.Entry(insertedCategoriaCita).State = EntityState.Detached;
            }

            var categoriaCitaUpdateDto = new CategoriaCitaUpdateDTO { IdCategoriaCita = 3, Nombre = "Categoria B", Descripcion = "Descripcion 1" };

            // Act
            var result = await _controller.PutCategoriasCita(3, categoriaCitaUpdateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task DeleteCategoriasCitas_ReturnsNoContent_WithValidId()
        {
            // Act
            Setup();
            var result = await _controller.DeleteCategoriasCita(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}