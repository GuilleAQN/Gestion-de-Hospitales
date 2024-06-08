using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.CategoriasCita
{
    public class CategoriaCitaInsertDTO
    {
        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(35, ErrorMessage = "Nombre no puede ser mayor a 35 carácteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Descripción es requerido")]
        [MaxLength(150, ErrorMessage = "Descripción no puede ser mayor a 150 carácteres")]
        [MinLength(10, ErrorMessage = "Descripción no puede ser menor a 10 carácteres")]
        public string Descripcion { get; set; } = null!;
    }
}
