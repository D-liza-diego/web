using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Product
    {
        public Product()
        {
            Salesdetails = new HashSet<Salesdetail>();
        }

        public int Idproduct { get; set; }
        public string Nameproduct { get; set; }
        public double Precio { get; set; }
        public int Idcategoria { get; set; }
        public int? Cantidad { get; set; }
        public string Codigo { get; set; }

        public virtual Categoria IdcategoriaNavigation { get; set; }
        public virtual ICollection<Salesdetail> Salesdetails { get; set; }
    }
}
