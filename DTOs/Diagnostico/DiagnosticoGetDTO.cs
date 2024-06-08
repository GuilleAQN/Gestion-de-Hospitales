namespace Primer_Parcial.DTOs.Diagnostico
{
    public class DiagnosticoGetDTO
    {
        public int IdDiagnostico { get; set; }

        public int IdPaciente { get; set; }

        public int IdDoctor { get; set; }

        public DateOnly Fecha { get; set; }

        public string Descripcion { get; set; } = null!;
    }
}
