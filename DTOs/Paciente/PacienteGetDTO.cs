namespace Primer_Parcial.DTOs.Paciente
{
    public class PacienteGetDTO
    {
        public int IdPaciente { get; set; }

        public string Cedula { get; set; } = null!;

        public string NombreCompleto { get; set; } = null!;

        public DateOnly FechaNacimiento { get; set; }

        public string Genero { get; set; } = null!;

        public string? Direccion { get; set; }

        public string Telefono { get; set; } = null!;

        public string? CorreoElectronico { get; set; }
    }
}
