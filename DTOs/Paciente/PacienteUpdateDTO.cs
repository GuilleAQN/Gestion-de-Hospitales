using System.ComponentModel.DataAnnotations;

namespace Primer_Parcial.DTOs.Paciente
{
    public class PacienteUpdateDTO
    {
        [Required(ErrorMessage = "Id es requerido")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Cédula es requerido")]
        [MaxLength(11, ErrorMessage = "Cédula no puede ser mayor a 11 carácteres")]
        [MinLength(10, ErrorMessage = "Cédula no puede ser menor a 11 carácteres")]
        public string Cedula { get; set; } = null!;

        [Required(ErrorMessage = "Nombre Completo es requerido")]
        [MaxLength(150, ErrorMessage = "Nombre Completo no puede ser mayor a 150 carácteres")]
        [MinLength(10, ErrorMessage = "Nombre Completo no puede ser menor a 10 carácteres")]
        public string NombreCompleto { get; set; } = null!;

        [Required]
        [Range(typeof(DateOnly), "1970-01-01", "2023-12-31", ErrorMessage = "Fecha de Nacimiento debe estar entre el {1} y el {2}.")]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Género es requerido")]
        [MaxLength(200, ErrorMessage = "Género solo puede ser un 200 carácter (M ó F)")]
        public string Genero { get; set; } = null!;

        [Required(ErrorMessage = "Dirección es requerido")]
        [MaxLength(200, ErrorMessage = "Dirección no puede ser mayor a 200 carácteres")]
        [MinLength(10, ErrorMessage = "Dirección no puede ser menor a 10 carácteres")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "Teléfono es requerido")]
        [MaxLength(10, ErrorMessage = "Teléfono no puede ser mayor a 10 carácteres")]
        public string Telefono { get; set; } = null!;


        [Required]
        [EmailAddress(ErrorMessage = "Favor introducir un email válido")]
        [MaxLength(60, ErrorMessage = "Correo Electrónico no puede ser mayor a 60 carácteres")]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Código del Departamento es requerido")]
        public int IdDepartamento { get; set; }
    }
}
