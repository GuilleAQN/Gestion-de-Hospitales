using System;
using System.Collections.Generic;

namespace Primer_Parcial.Models;

public partial class Habitacione
{
    public int IdHabitacion { get; set; }

    public string Numero { get; set; } = null!;

    public int Piso { get; set; }

    public string Tipo { get; set; } = null!;

    public int IdEstado { get; set; }
}
