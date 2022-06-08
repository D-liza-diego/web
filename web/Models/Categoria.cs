using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Products = new HashSet<Product>();
        }

        public int Idcategoria { get; set; }
        [Display(Name = "Nombre de la categoria")]
        public string Catname { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
