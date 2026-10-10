using System;
using System.Collections.Generic;

namespace MiPrimerApi.Models;

public partial class Cliente
{
    public int IdClientes { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string? Telefono { get; set; }
}
