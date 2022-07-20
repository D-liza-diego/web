using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Usuario
    {
        public int Iduser { get; set; }
        public string Name { get; set; }
        public int Dni { get; set; }
        public string Phone { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
    }
}
