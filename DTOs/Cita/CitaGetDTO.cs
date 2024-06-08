namespace Primer_Parcial.DTOs.Cita
{
    public class CitaGetDTO
    {
        public int IdCita { get; set; }

        public int IdPaciente { get; set; }

        public int IdDoctor { get; set; }

        public int IdEnfermera { get; set; }

        public DateTime Fecha { get; set; }

        public int IdCategoriaCita { get; set; }

        public string Descripción { get; set; } = null!;
    }
}
