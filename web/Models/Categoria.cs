using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Products = new HashSet<Product>();
        }

        public int Idcategoria { get; set; }
        public string Catname { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
