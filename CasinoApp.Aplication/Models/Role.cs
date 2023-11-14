using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Rol { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
