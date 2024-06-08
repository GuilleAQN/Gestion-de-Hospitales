using System;
using System.Collections.Generic;

namespace Primer_Parcial.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Ubicación { get; set; }

    public string Telefono { get; set; } = null!;

    public virtual ICollection<Doctore> Doctores { get; set; } = new List<Doctore>();

    public virtual ICollection<Enfermera> Enfermeras { get; set; } = new List<Enfermera>();
}
