using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.Habitacion
{
    public class HabitacionUpdateDTO
    {
        [Required(ErrorMessage = "Id es requerido")]
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "El número de la habitación es requerido")]
        [MaxLength(5, ErrorMessage = "El número de la habitación no puede ser mayor a 5 carácteres")]
        public string Numero { get; set; } = null!;

        [Required(ErrorMessage = "El piso de la habitación es requerido")]
        [Range(1, 4, ErrorMessage = "El piso de la habitación debe ser entre {1} y {2}")]
        public int Piso { get; set; }

        [Required(ErrorMessage = "Tipo de Habitación es requerido")]
        [MaxLength(50, ErrorMessage = "El número de la habitación no puede ser mayor a 5 carácteres")]
        public string Tipo { get; set; } = null!;

        [Required(ErrorMessage = "Código del Estado es requerido")]
        public int IdEstado { get; set; }
    }
}
