namespace Primer_Parcial.DTOs.Doctor
{
    public class DoctorGetDTO
    {
        public int IdDoctor { get; set; }

        public string Cedula { get; set; } = null!;

        public string NombreCompleto { get; set; } = null!;

        public DateOnly FechaContratacion { get; set; }

        public string? Direccion { get; set; }

        public string Telefono { get; set; } = null!;

        public string? CorreoElectronico { get; set; }

        public int IdEspecialidad { get; set; }

        public int IdDepartamento { get; set; }
    }
}
