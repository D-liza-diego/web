using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public partial class Usuario
    {
        public int Iduser { get; set; }
        [Display(Name = "Nombre del usuario")]
        public string Name { get; set; } = null!;
        [Display(Name = "Documento del usuario")]
        public int Dni { get; set; }
        [Display(Name = "Telefono del usuario")]
        public string Phone { get; set; } = null!;
        [Display(Name = "Correo del usuario")]
        public string Correo { get; set; } = null!;
        [Display(Name = "Rol del usuario")]
        public string Rol { get; set; } = null!;
    }
}
