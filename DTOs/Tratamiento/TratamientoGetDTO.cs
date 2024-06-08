namespace Primer_Parcial.DTOs.Tratamiento
{
    public class TratamientoGetDTO
    {
        public int IdTratamiento { get; set; }

        public int IdDiagnostico { get; set; }

        public int IdDoctor { get; set; }

        public string Descripcion { get; set; } = null!;

        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaFin { get; set; }
    }
}
