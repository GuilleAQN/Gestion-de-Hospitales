using System;
using System.Collections.Generic;

namespace Primer_Parcial.Models;

public partial class Doctore
{
    public int IdDoctor { get; set; }

    public string Cedula { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public DateOnly FechaContratacion { get; set; }

    public string? Direccion { get; set; }

    public string Telefono { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public int IdEspecialidad { get; set; }

    public int IdDepartamento { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
