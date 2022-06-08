using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public partial class Product
    {
        public Product()
        {
            Salesdetails = new HashSet<Salesdetail>();
        }
       
        public int Idproduct { get; set; }

        [Display(Name = "Nombre del producto")]
        public string Nameproduct { get; set; } = null!;

        [Display(Name = "Precio")]
        public double Precio { get; set; }

        [Display(Name = "Categoria del producto")]
        public int Idcategoria { get; set; }

        [Display(Name = "Cantidad")]
        public int? Cantidad { get; set; }

        public virtual Categoria IdcategoriaNavigation { get; set; } = null!;
        public virtual ICollection<Salesdetail> Salesdetails { get; set; }
    }
}
