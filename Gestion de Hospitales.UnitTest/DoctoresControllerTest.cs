using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Primer_Parcial.Controller;
using Primer_Parcial.DTOs.Doctor;
using Primer_Parcial.DTOs.Departamento;
using Primer_Parcial.Models;

namespace Gestion_de_Hospitales.UnitTest
{
    public class DoctoresControllerTest
    {
        private DoctoresController _controller;
        private DbTestFixture<HospitalDbContext> _fixture;

        public DoctoresControllerTest()
        {
            _fixture = new DbTestFixture<HospitalDbContext>();
            _controller = new DoctoresController(_fixture.Context, _fixture.Mapper);
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

            var doctores = new List<Doctore>
            {
                new Doctore
                {
                    IdDoctor = 1,
                    Cedula = "00112345678",
                    NombreCompleto = "Dr. Juan Perez",
                    FechaContratacion = new DateOnly(2020, 1, 1),
                    Direccion = "Calle Principal 123",
                    Telefono = "123-456-7890",
                    CorreoElectronico = "juan.perez@example.com",
                    IdEspecialidad = 1,
                    IdDepartamento = 1
                },
                new Doctore
                {
                    IdDoctor = 2,
                    Cedula = "00298765432",
                    NombreCompleto = "Dra. Maria Gonzalez",
                    FechaContratacion = new DateOnly(2018, 5, 10),
                    Direccion = "Avenida Central 456",
                    Telefono = "987-654-3210",
                    CorreoElectronico = "maria.gonzalez@example.com",
                    IdEspecialidad = 2,
                    IdDepartamento = 2
                }
            };



            _fixture.Context.Doctores.AddRange(doctores);
            _fixture.Context.SaveChanges();
        }

        [Fact]
        public async Task GetDoctores_ReturnsOkResult()
        {
            // Arrange
            Setup();

            // Act
            var result = await _controller.GetDoctores();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            // Verificar que el valor devuelto no es nulo
            Assert.NotNull(result.Result);

            // Verificar los datos devueltos
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var doctores = okResult.Value as IEnumerable<DoctorGetDTO>;
            Assert.NotNull(doctores);

            Assert.Equal(2, doctores.Count()); // Verificar que se devuelvan todas las habitaciones
        }

        [Fact]
        public async Task GetDoctor_ReturnsOkResult_WithValidId()
        {
            // Arrange
            Setup();
            var doctor = new Doctore
            {
                IdDoctor = 2,
                Cedula = "00298765432",
                NombreCompleto = "Dra. Maria Gonzalez",
                FechaContratacion = new DateOnly(2018, 5, 10),
                Direccion = "Avenida Central 456",
                Telefono = "987-654-3210",
                CorreoElectronico = "maria.gonzalez@example.com",
                IdEspecialidad = 2,
                IdDepartamento = 2
            };

            // Act
            var result = await _controller.GetDoctor(2);

            // Assert
            var doctorDto = Assert.IsType<DoctorGetDTO>(result.Value);
            Assert.Equal(doctor.IdDoctor, doctorDto.IdDoctor);
        }

        [Fact]
        public async Task PostDoctor_ReturnsOkResult_WithValidData()
        {
            // Arrange
            Setup();
            var doctorDto = new DoctorInsertDTO
            {
                Cedula = "11298765432",
                NombreCompleto = "Dra. Maria Gonzalez",
                FechaContratacion = new DateOnly(2018, 5, 10),
                Direccion = "Avenida Central 456",
                Telefono = "987-654-3210",
                CorreoElectronico = "maria.gonzalez@example.com",
                IdEspecialidad = 2,
                IdDepartamento = 2
            };

            // Act
            var result = await _controller.PostDoctor(doctorDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<int>(okResult.Value);
        }

        [Fact]
        public async Task PutDoctor_ReturnsNoContent_WithValidId()
        {
            // Arrange
            Setup();
            var doctorDto = new DoctorInsertDTO
            {
                Cedula = "15298765432",
                NombreCompleto = "Dra. Maria Gonzalez",
                FechaContratacion = new DateOnly(2018, 5, 10),
                Direccion = "Avenida Central 456",
                Telefono = "987-654-3210",
                CorreoElectronico = "maria.gonzalez@example.com",
                IdEspecialidad = 2,
                IdDepartamento = 2
            };

            await _controller.PostDoctor(doctorDto);

            // Desatachar la entidad que se acaba de insertar para evitar conflictos
            var insertedDoctor = _fixture.Context.Doctores.Local.FirstOrDefault(h => h.IdDoctor == 3);
            if (insertedDoctor != null)
            {
                _fixture.Context.Entry(insertedDoctor).State = EntityState.Detached;
            }

            var doctorUpdateDto = new DoctorUpdateDTO
            {
                IdDoctor = 3,
                Cedula = "15298765000",
                NombreCompleto = "Dra. Maria Gonzalez",
                FechaContratacion = new DateOnly(2018, 5, 10),
                Direccion = "Avenida Central 456",
                Telefono = "987-654-3210",
                CorreoElectronico = "maria.gonzalez@example.com",
                IdEspecialidad = 2,
                IdDepartamento = 2
            };

            // Act
            var result = await _controller.PutDoctor(3, doctorUpdateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task DeleteDoctor_ReturnsNoContent_WithValidId()
        {
            // Act
            Setup();
            var result = await _controller.DeleteDoctor(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
