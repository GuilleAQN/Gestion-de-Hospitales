namespace Gestion_de_Hospitales.IntegrationTest
{
    public class Tests
    {
        private HttpClient _client;

        [SetUp]
        public void ConfigurarClienteHttp()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("http://localhost:1234")); // Obtener la URL base de una variable de entorno
        }

        [Test]
        public async Task AgregarCita_DeberiaAgregarCorrectamente()
        {
            // Arrange
            var nuevaCita = new
            {
                IdPaciente = 1,
                IdDoctor = 2,
                IdEnfermera = 3,
                Fecha = DateTime.Now.AddDays(1),
                IdCategoriaCita = 4,
                Descripción = "Cita de seguimiento"
            };

            var json = JsonSerializer.Serialize(nuevaCita);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/citas", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}