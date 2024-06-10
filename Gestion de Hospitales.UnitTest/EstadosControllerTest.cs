using Microsoft.AspNetCore.Mvc;
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
    public class EstadosControllerTest
    {
        private EstadosController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public EstadosControllerTest()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new EstadosController(_fixture.Context, _fixture.Mapper);
        }

        [Fact]
        public async Task GetEstados_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetEstados();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
