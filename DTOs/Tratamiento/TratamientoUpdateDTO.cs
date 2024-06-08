using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.Tratamiento
{
    public class TratamientoUpdateDTO
    {
        [Required(ErrorMessage = "Id es requerido")]
        public int IdTratamiento { get; set; }
    }
}
