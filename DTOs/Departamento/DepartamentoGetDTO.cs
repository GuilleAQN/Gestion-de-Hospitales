namespace Primer_Parcial.DTOs.Departamento
{
    public class DepartamentoGetDTO
    {
        public int IdDepartamento { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? Ubicación { get; set; }

        public string Telefono { get; set; } = null!;
    }
}
