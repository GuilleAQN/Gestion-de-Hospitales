using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.Estado
{
    public class EstadoInsertDTO
    {
        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(30, ErrorMessage = "Nombre no puede ser mayor a 30 carácteres")]
        public string Nombre { get; set; } = null!;
    }
}
