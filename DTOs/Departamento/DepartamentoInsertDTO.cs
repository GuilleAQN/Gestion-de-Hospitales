using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.Departamento
{
    public class DepartamentoInsertDTO
    {
        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(80, ErrorMessage = "Nombre no puede ser mayor a 80 carácteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Descripción es requerido")]
        [MaxLength(150, ErrorMessage = "Descripción no puede ser mayor a 150 carácteres")]
        [MinLength(10, ErrorMessage = "Descripción no puede ser menor a 10 carácteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Ubicación es requerido")]
        [MaxLength(50, ErrorMessage = "Ubicación no puede ser mayor a 150 carácteres")]
        public string? Ubicación { get; set; }

        [Required(ErrorMessage = "Teléfono es requerido")]
        [MaxLength(10, ErrorMessage = "Teléfono no puede ser mayor a 10 carácteres")]
        public string Telefono { get; set; } = null!;
    }
}
