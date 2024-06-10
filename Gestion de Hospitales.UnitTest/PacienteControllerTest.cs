using Microsoft.AspNetCore.Mvc;
using Primer_Parcial.Controller;
using Primer_Parcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_de_Hospitales.UnitTest
{
    public class PacienteControllerTest
    {
        private PacientesController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public PacienteControllerTest()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new PacientesController(_fixture.Context, _fixture.Mapper);
        }

        [Fact]
        public async Task GetPacientes_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetPacientes();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
