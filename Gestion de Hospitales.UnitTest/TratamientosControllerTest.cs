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
    public class TratamientosControllerTest
    {
        private TratamientosController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public TratamientosControllerTest()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new TratamientosController(_fixture.Context, _fixture.Mapper);
        }

        [Fact]
        public async Task GetTratamientos_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetTratamientos();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
