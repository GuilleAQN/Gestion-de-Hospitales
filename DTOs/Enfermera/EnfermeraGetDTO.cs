namespace Primer_Parcial.DTOs.Enfermera
{
    public class EnfermeraGetDTO
    {
        public int IdEnfermera { get; set; }

        public string Cedula { get; set; } = null!;

        public string NombreCompleto { get; set; } = null!;

        public DateOnly FechaContratacion { get; set; }

        public string? Direccion { get; set; }

        public string Telefono { get; set; } = null!;

        public string? CorreoElectronico { get; set; }

        public int IdDepartamento { get; set; }
    }
}
