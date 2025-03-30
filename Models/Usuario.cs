using System;
using System.Collections.Generic;

namespace ApiLectorExcel.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? FechaActualiza { get; set; }
}
