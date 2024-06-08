using System;
using System.Collections.Generic;

namespace Primer_Parcial.Models;

public partial class Diagnostico
{
    public int IdDiagnostico { get; set; }

    public int IdPaciente { get; set; }

    public int IdDoctor { get; set; }

    public DateOnly Fecha { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual Doctore IdDoctorNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
