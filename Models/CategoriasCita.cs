using System;
using System.Collections.Generic;

namespace Primer_Parcial.Models;

public partial class CategoriasCita
{
    public int IdCategoriaCita { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
