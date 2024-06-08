using System;
using System.Collections.Generic;

namespace Primer_Parcial.Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public string Cedula { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Genero { get; set; } = null!;

    public string? Direccion { get; set; }

    public string Telefono { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();
}
