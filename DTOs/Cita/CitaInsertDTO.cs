using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.Cita
{
    public class CitaInsertDTO
    {
        [Required(ErrorMessage = "Código del Paciente es requerido")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Código del Doctor es requerido")]
        public int IdDoctor { get; set; }

        [Required(ErrorMessage = "Código de la Enfermera es requerido")]
        public int IdEnfermera { get; set; }

        [Required]
        [Range(typeof(DateTime), "1990-01-01 00:00:00.00", "2050-12-31 23:59:59.99", ErrorMessage = "{0} debe estar entre el {1} y el {2}.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Código de la Categoría de la Cita es requerido")]
        public int IdCategoriaCita { get; set; }

        [Required(ErrorMessage = "Descripción es requerido")]
        [MaxLength(200, ErrorMessage = "Descripción no puede ser mayor a 200 carácteres")]
        [MinLength(10, ErrorMessage = "Descripción no puede ser menor a 10 carácteres")]
        public string Descripción { get; set; } = null!;
    }
}
