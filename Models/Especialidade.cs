using System;
using System.Collections.Generic;

namespace Primer_Parcial.Models;

public partial class Especialidade
{
    public int IdEspecialidad { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;
}
