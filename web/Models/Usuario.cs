using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Usuario
    {
        public int Iduser { get; set; }
        public string Name { get; set; } = null!;
        public int Dni { get; set; }
        public string Phone { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
