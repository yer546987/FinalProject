using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Usuario1 { get; set; }

    public string Contraseña { get; set; }

    public int IdRol { get; set; }

    public virtual Role IdRolNavigation { get; set; }
}
