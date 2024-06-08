using System;
using System.Collections.Generic;

namespace Primer_Parcial.Models;

public partial class Tratamiento
{
    public int IdTratamiento { get; set; }

    public int IdDiagnostico { get; set; }

    public int IdDoctor { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public virtual Diagnostico IdDiagnosticoNavigation { get; set; } = null!;

    public virtual Doctore IdDoctorNavigation { get; set; } = null!;
}
