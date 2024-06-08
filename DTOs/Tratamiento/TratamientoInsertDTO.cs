using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.Tratamiento
{
    public class TratamientoInsertDTO
    {
        [Required(ErrorMessage = "Código del Diagnóstico es requerido")]
        public int IdDiagnostico { get; set; }

        [Required(ErrorMessage = "Código del Doctor es requerido")]
        public int IdDoctor { get; set; }

        [Required(ErrorMessage = "Descripción es requerido")]
        [MaxLength(250, ErrorMessage = "Descripción no puede ser mayor a 250 carácteres")]
        public string Descripcion { get; set; } = null!;

        [Required]
        [Range(typeof(DateOnly), "1990-01-01", "2050-12-31", ErrorMessage = "Fecha de Contratación debe estar entre el {1} y el {2}.")]
        public DateOnly FechaInicio { get; set; }

        [Required]
        [Range(typeof(DateOnly), "1990-01-01", "2050-12-31", ErrorMessage = "Fecha de Finalización debe estar entre el {1} y el {2}.")]
        public DateOnly FechaFin { get; set; }
    }
}
