using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.Diagnostico
{
    public class DiagnosticoUpdateDTO
    {
        [Required(ErrorMessage = "Id es requerido")]
        public int IdDiagnostico { get; set; }

        [Required(ErrorMessage = "Código del Paciente es requerido")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Código del Doctor es requerido")]
        public int IdDoctor { get; set; }

        [Required]
        [Range(typeof(DateOnly), "1990-01-01", "2050-12-31", ErrorMessage = "{0} debe estar entre el {1} y el {2}.")]
        public DateOnly Fecha { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Descripción no puede ser mayor a 200 carácteres")]
        [MinLength(10, ErrorMessage = "Descripción no puede ser menor a 10 carácteres")]
        public string Descripcion { get; set; } = null!;
    }
}
